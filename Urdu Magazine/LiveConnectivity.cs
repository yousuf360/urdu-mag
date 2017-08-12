using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Microsoft.AspNet.SignalR;
using System.Collections.Concurrent;
using System.Threading.Tasks;

namespace Urdu_Magazine
{
    public class LiveConnectivity : Hub
    {
        static ConcurrentDictionary<int, HashSet<string>> Userdic = new ConcurrentDictionary<int,HashSet<string>>();
        static ConcurrentDictionary<string, int> UserdicForRemoving = new ConcurrentDictionary<string, int>();

        //public void addCommentInWebView(int issueId, string from,string profilePic,string commentText, string date)
        //{
        //    if (profilePic == null) {
        //        profilePic = "default";
        //    }
        //    Clients.Group("web-view-" + issueId).addComment(from,profilePic,commentText,date);
        //}
        public void joinUserDic(int userId)
        {
            Groups.Add(Context.ConnectionId, "user-id-" + userId);
            //if (Userdic.ContainsKey(userId)) {
            //    Userdic[userId].Add(connectionId);
            //    UserdicForRemoving.TryAdd(connectionId, userId);
            //}
            //else{
            //    HashSet<string> item;
            //    item = new HashSet<string>();
            //    item.Add(connectionId);
            //    Userdic.TryAdd(userId, item);
            //    UserdicForRemoving.TryAdd(connectionId, userId);
            //}
        }
        //public override Task OnDisconnected(bool stopCalled)
        //{
        //    int userId = 0;
        //    UserdicForRemoving.TryRemove(Context.ConnectionId, out userId);
        //    if (userId != 0) {

        //        Userdic[userId].Remove(Context.ConnectionId);
        //        if (Userdic[userId].Count == 0) {
        //            HashSet<string> item;
        //            Userdic.TryRemove(userId,out item);
        //        }
        //    }
        //    return base.OnDisconnected(stopCalled);
        //}
        public void addMeToWebViewRoom(int issueId) {
            Groups.Add(Context.ConnectionId, "web-view-" + issueId);
        }


        
    }
}