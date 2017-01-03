using System.Web;
using System.Web.Optimization;

namespace BeautyAndCare
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

            // Use the development version of Modernizr to develop with and learn from. Then, when you're
            // ready for production, use the build tool at http://modernizr.com to pick only the tests you need.
            bundles.Add(new ScriptBundle("~/bundles/modernizr").Include(
                        "~/Scripts/modernizr-*"));

            bundles.Add(new ScriptBundle("~/bundles/bootstrap").Include(
                      "~/bower_components/bootstrap/dist/js/bootstrap.min.js",
                      "~/Scripts/respond.js"));

            bundles.Add(new StyleBundle("~/Content/css").Include(
                      "~/bower_components/bootstrap/dist/css/bootstrap.min.css",
                      "~/bower_components/metisMenu/dist/metisMenu.min.css",
                      "~/Content/timeline.css",
                      "~/Content/sb-admin-2.css"));


            bundles.Add(new ScriptBundle("~/bundles/jqueryWeb").Include(
"~/Template/js/jquery-2.1.1.min.js",
"~/Template/js/bootstrap.min.js",
"~/Template/js/owl.carousel.min.js",
"~/Template/js/superfish.min.js",
"~/Template/js/jquery.rd-navbar.min.js",
"~/Template/js/bootstrap-tabcollapse.js",
"~/Template/js/jquery.cookies.js",
"~/Template/js/style_switcher_demo.js",

"~/Template/js/jquery.bxslider.js",
"~/Template/js/jquery.elevatezoom.js",
"~/Template/js/CusWeb.js"

));
            bundles.Add(new ScriptBundle("~/bundles/jqueryWebPlug").Include(

"~/Template/js/device.min.js",
"~/Template/js/livesearch.min.js", "~/Template/js/common.js", "~/Template/js/script.js"
));
            bundles.Add(new StyleBundle("~/Content/cssWeb").Include(

                   "~/Template/css/stylesheet.css"));
        }
    }
}
