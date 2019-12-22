using System.Web;
using System.Web.Optimization;

namespace DotrA_001
{
    public class BundleConfig
    {
        // 如需統合的詳細資訊，請瀏覽 https://go.microsoft.com/fwlink/?LinkId=301862
        public static void RegisterBundles(BundleCollection bundles)
        {
            bundles.Add(new ScriptBundle("~/bundles/jquery").Include(
                        "~/Scripts/jquery-{version}.js"));

            bundles.Add(new ScriptBundle("~/bundles/jqueryval").Include(
                        "~/Scripts/jquery.validate*"));

            // 使用開發版本的 Modernizr 進行開發並學習。然後，當您
            // 準備好可進行生產時，請使用 https://modernizr.com 的建置工具，只挑選您需要的測試。
            bundles.Add(new ScriptBundle("~/bundles/modernizr").Include(
                        "~/Scripts/modernizr-*"));

            bundles.Add(new ScriptBundle("~/bundles/bootstrap").Include(
                      "~/Scripts/bootstrap.js"));

            bundles.Add(new StyleBundle("~/Content/css").Include(
                      "~/Content/bootstrap.css",
                      "~/Content/site.css"));
            //Assets/css
            bundles.Add(new StyleBundle("~/Asset/css").Include(
                "~/Assets/css/animate.css",
                "~/Assets/css/aos.css",
                "~/Assets/css/bootstrap-datepicker.css",
                "~/Assets/css/bootstrap.min.css",
                "~/Assets/css/flaticon.css",
                "~/Assets/css/icomoon.css",
                "~/Assets/css/ionicons.min.css",
                "~/Assets/css/jquery.timepicker.css",
                "~/Assets/css/magnific-popup.css",
                "~/Assets/css/open-iconic-bootstrap.min.css",
                "~/Assets/css/owl.carousel.min.css",
                "~/Assets/css/owl.theme.default.min.css",
                "~/Assets/css/style.css"
                ));
            //Assect/jqery
            bundles.Add(new ScriptBundle("~/Assets/jquery").Include(
                "~/Assets/js/jquery.min.js"
                ));

            //Assets/js
            bundles.Add(new ScriptBundle("~/Asset/js").Include(
                "~/Assets/js/jquery-migrate-3.0.1.min.js",
                "~/Assets/js/popper.min.js",
                //"~/Assets/js/bootstrap.min.js",
                "~/Assets/js/jquery.easing.1.3.js",
                "~/Assets/js/jquery.waypoints.min.js",
                "~/Assets/js/jquery.stellar.min.js",
                "~/Assets/js/owl.carousel.min.js",
                "~/Assets/js/jquery.magnific-popup.min.js",
                "~/Assets/js/aos.js",
                "~/Assets/js/jquery.animateNumber.min.js",
                "~/Assets/js/bootstrap-datepicker.js",
                "~/Assets/js/scrollax.min.js",
                "~/Assets/js/main.js"
           ));
            bundles.Add(new ScriptBundle("~/bundles/datatable").Include(
           "~/assets/js/data-table/datatables.min.js",
               "~/assets/js/data-table/dataTables.bootstrap4.min.js",
               "~/assets/js/data-table/dataTables.buttons.min.js",
               "~/assets/js/data-table/buttons.bootstrap.min.js",
               "~/assets/js/data-table/jszip.min.js",
               "~/assets/js/data-table/vfs_fonts.js",
               "~/assets/js/data-table/buttons.html5.min.js",
               "~/assets/js/data-table/buttons.print.min.js",
               "~/assets/js/data-table/buttons.colVis.min.js",
               "~/assets/js/init/datatables-init.js"
                ));
            BundleTable.EnableOptimizations = false;
        }
    }
}
