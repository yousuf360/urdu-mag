using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System;
using System.Collections;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Data.Entity;
using System.IO;
using System.Linq;
using System.Net;
using System.Reflection;
using System.Web;
namespace Urdu_Magazine.UserDefinedFunctions
{
    public class FileOperations
    {
       
        public Dictionary<bool, string> uploadDocuments(Dictionary<PropertyInfo, HttpPostedFileBase> files, string path)
        {
           
            foreach (var key in files.Keys)
            {
                HttpPostedFileBase file;
                files.TryGetValue(key, out file);
                string fileURL = upload(file, path + "/", key.Name);

                if (fileURL.Equals("error"))
                {
                    DeleteDirectory(path);
                    Dictionary<bool, string> error = new Dictionary<bool, string>();
                    error.Add(false, key.Name);
                    return error;
                }

            }

            Dictionary<bool, string> success = new Dictionary<bool, string>();
            success.Add(true, path);
            return success;

        }
        public void DeleteDirectory(string target_dir)
        {
            string[] files = Directory.GetFiles(target_dir);
            string[] dirs = Directory.GetDirectories(target_dir);

            foreach (string file in files)
            {
                System.IO.File.SetAttributes(file, FileAttributes.Normal);
                System.IO.File.Delete(file);
            }

            foreach (string dir in dirs)
            {
                DeleteDirectory(dir);
            }

            Directory.Delete(target_dir, false);
        }
        public bool? createDirectory(string path)
        {
            try
            {
                // Determine whether the directory exists.
                if (Directory.Exists(path))
                {
                    //Console.WriteLine("That path exists already.");
                    return false;
                }

                // Try to create the directory.
                // DirectoryInfo di = Directory.CreateDirectory(path);
                Directory.CreateDirectory(path);
                //  Console.WriteLine("The directory was created successfully at {0}.", Directory.GetCreationTime(path));

                // Delete the directory.
                // di.Delete();
                //  Console.WriteLine("The directory was deleted successfully.");
                return true;
            }
            catch (Exception e)
            {
                Console.WriteLine("The process failed: {0}", e.ToString());
                return null;
            }
        }
        // GET: Admin
        public string upload(HttpPostedFileBase file, string path, string name)
        {
            try
            {
                string extension = Path.GetExtension(file.FileName);
                string nameWithFullPath = Path.Combine(path, name + extension);
                file.SaveAs(nameWithFullPath);

                return nameWithFullPath;
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error occurred. Error details: " + ex.Message);
                return "error";
            }
        }
    }
}