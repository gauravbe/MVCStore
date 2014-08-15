using System.Web;
using System.Web.Optimization;

namespace MVCStore.Admin.App_Start
{
    public class BundleConfig
    {  
        // For more information on Bundling, visit http://go.microsoft.com/fwlink/?LinkId=254725
        public static void RegisterBundles(BundleCollection bundles)
        {
            bundles.Add(new ScriptBundle("~/bundles/jquery").Include(
                        "~/content/scripts/jquery-{version}.js"));

            bundles.Add(new ScriptBundle("~/bundles/jqueryui").Include(
                        "~/content/scripts/jquery-ui-{version}.js"));

            bundles.Add(new ScriptBundle("~/bundles/jqueryval").Include(
                        "~/content/scripts/jquery.unobtrusive*",
                        "~/content/scripts/jquery.validate*"));

            // Use the development version of Modernizr to develop with and learn from. Then, when you're
            // ready for production, use the build tool at http://modernizr.com to pick only the tests you need.
            bundles.Add(new ScriptBundle("~/bundles/modernizr").Include(
                        "~/content/scripts/modernizr-*"));

            bundles.Add(new ScriptBundle("~/bundles/bootstrap").Include(
                    "~/content/scripts/bootstrap.js"             
                    ));

            bundles.Add(new StyleBundle("~/content/css/bootstrap").Include(
                "~/content/css/bootstrap/bootstrap.css",
                "~/content/css/bootstrap/bootstrap-theme.css"
                ));          

        }
    }
}