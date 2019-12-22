using System.Web;
using System.Web.Optimization;

namespace BackEndSystem
{
    public class BundleConfig
    {
        // For more information on bundling, visit https://go.microsoft.com/fwlink/?LinkId=301862
        public static void RegisterBundles(BundleCollection bundles)
        {

            bundles.Add(new ScriptBundle("~/bundles/jquery").Include(
                        "~/Scripts/jquery-{version}.js"));

            bundles.Add(new ScriptBundle("~/bundles/mainjs").Include(
                 "~/Scripts/popper.min.js",
                 "~/Scripts/bootstrap.min.js",
                 "~/Scripts/jquery.matchHeight.min.js",
                 "~/Scripts/main.js"));

            bundles.Add(new ScriptBundle("~/bundles/datatable").Include(
            "~/Scripts/data-table/datatables.min.js",
                "~/Scripts/data-table/dataTables.bootstrap4.min.js",
                "~/Scripts/data-table/dataTables.buttons.min.js",
                "~/Scripts/data-table/buttons.bootstrap.min.js",
                "~/Scripts/data-table/jszip.min.js",
                "~/Scripts/data-table/vfs_fonts.js",
                "~/Scripts/data-table/buttons.html5.min.js",
                "~/Scripts/data-table/buttons.print.min.js",
                "~/Scripts/data-table/buttons.colVis.min.js",
                "~/Scripts/init/datatables-init.js"
                 ));


            bundles.Add(new ScriptBundle("~/bundles/jqueryval").Include(
              "~/Scripts/jquery.validate*"));

            // Use the development version of Modernizr to develop with and learn from. Then, when you're
            // ready for production, use the build tool at https://modernizr.com to pick only the tests you need.
            bundles.Add(new ScriptBundle("~/bundles/modernizr").Include(
                        "~/Scripts/modernizr-*"));

            //bundles.Add(new ScriptBundle("~/bundles/bootstrap").Include(
            //          "~/Scripts/bootstrap.min.js"));

            bundles.Add(new StyleBundle("~/Contents").Include(
                      "~/Content/bootstrap.css",
                        "~/Content/normalize.css",
                      "~/Content/all.css",
                      "~/Content/themify-icons.css",
                      "~/Content/cs-skin-elastic.css",
                      "~/Content/style.css"
                      ));
            BundleTable.EnableOptimizations = false;
        }
    }
}
