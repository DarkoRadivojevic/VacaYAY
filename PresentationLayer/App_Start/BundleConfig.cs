using System.Web;
using System.Web.Optimization;

namespace VacaYAY
{
    public class BundleConfig
    {
        // For more information on bundling, visit https://go.microsoft.com/fwlink/?LinkId=301862
        public static void RegisterBundles(BundleCollection bundles)
        {
            bundles.Add(new ScriptBundle("~/bundles/jquery").Include(
                        "~/Scripts/jquery-{version}.js",
                        "~/Scripts/core/jquery.min.js",
                        "~/Scripts/core/popper.min.js",
                        "~/Scripts/plugins/perfect-scrollbar.jquery.min.js"));

            bundles.Add(new ScriptBundle("~/bundles/jqueryval").Include(
                        "~/Scripts/jquery.validate*"));


            // Use the development version of Modernizr to develop with and learn from. Then, when you're
            // ready for production, use the build tool at https://modernizr.com to pick only the tests you need.
            bundles.Add(new ScriptBundle("~/bundles/modernizr").Include(
                        "~/Scripts/modernizr-*"));

            bundles.Add(new ScriptBundle("~/bundles/bootstrap").Include(
                      "~/Scripts/bootstrap.js",
                       "~/Scripts/bootstrap.min.js",
                      "~/Scripts/paper-dashboard.js",
                      "~/Scripts/pikaday.js"));

            bundles.Add(new StyleBundle("~/Content/css").Include(
                      "~/Content/bootstrap.min.css",
                      "~/Content/paper-dashboard.css",
                      "~/Content/site.css",
                      "~/Content/pikaday.css",
                      "~/Content/customHelpers.css"));

            bundles.Add(new ScriptBundle("~/bundles/appscripts").Include(
                "~/Scripts/AdminView/admin.scripts.js",
                "~/Scripts/Pickaday/pikaday.scripts.js",
                "~/Scripts/shared.scripts.js"
                ));
        }
    }
}

//obradjivanje greske u ajax i u contorleru
//show employess edit,delete 
