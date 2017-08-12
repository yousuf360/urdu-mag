using System.Web;
using System.Web.Optimization;

namespace Urdu_Magazine
{
    public class BundleConfig
    {
        // For more information on bundling, visit http://go.microsoft.com/fwlink/?LinkId=301862
        public static void RegisterBundles(BundleCollection bundles)
        {
            bundles.Add(new ScriptBundle("~/bundles/jquery").Include(
                        "~/Scripts/jquery-{version}.js"));

            bundles.Add(new ScriptBundle("~/bundles/jqueryval").Include(
                        "~/Scripts/jquery.validate*"));
            bundles.Add(new ScriptBundle("~/bundles/jqueryval").Include(
                       "~/Scripts/jquery.validate*"));
            bundles.Add(new ScriptBundle("~/bundles/malihu-custom-scrollbar-plugin").Include(
                      "~/Content/malihu-custom-scrollbar-plugin/jquery.mCustomScrollbar.concat.min.js"));
            // Use the development version of Modernizr to develop with and learn from. Then, when you're
            // ready for production, use the build tool at http://modernizr.com to pick only the tests you need.
            bundles.Add(new ScriptBundle("~/bundles/modernizr").Include(
                        "~/Scripts/modernizr-*"));

            bundles.Add(new ScriptBundle("~/bundles/bootstrap").Include(
                      "~/Scripts/bootstrap.js",
                      "~/Scripts/respond.js"));
            bundles.Add(new ScriptBundle("~/bundles/bootstrap-rtl").Include(
                   "~/Scripts/rtl/bootstrap-rtl.js",
                   "~/Scripts/respond.js"));
            bundles.Add(new ScriptBundle("~/bundles/Custom-rtl").Include(
          "~/Scripts/rtl/custom.js"));
            bundles.Add(new StyleBundle("~/Content/css").Include(
                      "~/Content/bootstrap.css",
                      "~/Content/site.css"));
            bundles.Add(new StyleBundle("~/Content/css-rtl").Include(
                  "~/Content/Admin-ui/rtl/bootstrap-rtl.css",
                  "~/Content/Admin-ui/rtl/Site.css"));
            bundles.Add(new StyleBundle("~/Content/font-awesome").Include(
                 "~/Content/font-awesome/css/font-awesome.min.css", new CssRewriteUrlTransform()));



            bundles.Add(new StyleBundle("~/Content/malihu-custom-scrollbar-plugin").Include(
                "~/Content/malihu-custom-scrollbar-plugin/jquery.mCustomScrollbar.min.css"));
        }
    }
}
