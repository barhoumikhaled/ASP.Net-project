using System.Web;
using System.Web.Optimization;

namespace FindIt {
    public class BundleConfig {
        // For more information on bundling, visit http://go.microsoft.com/fwlink/?LinkId=301862
        public static void RegisterBundles(BundleCollection bundles) {
            bundles.Add(new ScriptBundle("~/bundles/jquery").Include(
                        "~/Scripts/jquery-{version}.js"));

            bundles.Add(new ScriptBundle("~/bundles/jqueryval").Include(
                        "~/Scripts/jquery.validate*"));

            // Use the development version of Modernizr to develop with and learn from. Then, when you're
            // ready for production, use the build tool at http://modernizr.com to pick only the tests you need.
            bundles.Add(new ScriptBundle("~/bundles/modernizr").Include(
                        "~/Scripts/modernizr-*"));

            bundles.Add(new ScriptBundle("~/bundles/template").Include(
                "~/Content/plugins/rs-plugin/js/jquery.themepunch.tools.min.js",
                "~/Content/plugins/rs-plugin/js/jquery.themepunch.revolution.min.js",
                "~/Content/plugins/isotope/isotope.pkgd.min.js",
                "~/Content/plugins/magnific-popup/jquery.magnific-popup.min.js",
                "~/Content/plugins/waypoints/jquery.waypoints.min.js",
                "~/Content/plugins/jquery.countTo.js",
                "~/Content/plugins/jquery.parallax-1.1.3.js",          
                "~/Content/plugins/jquery.validate.js",
                "~/Content/plugins/vide/jquery.vide.js",
                "~/Content/plugins/owl-carousel/owl.carousel.js",
                "~/Content/plugins/jquery.browser.js",
                "~/Content/plugins/SmoothScroll.js",
                "~/Content/js/template.js"
                ));

            bundles.Add(new ScriptBundle("~/bundles/template").Include(
                "~/Content/plugins/rs-plugin/js/jquery.themepunch.tools.min.js",
                "~/Content/plugins/rs-plugin/js/jquery.themepunch.revolution.min.js",
                "~/Content/plugins/isotope/isotope.pkgd.min.js",
                "~/Content/plugins/magnific-popup/jquery.magnific-popup.min.js",
                "~/Content/plugins/waypoints/jquery.waypoints.min.js",
                "~/Content/plugins/jquery.countTo.js",
                "~/Content/plugins/jquery.parallax-1.1.3.js",          
                "~/Content/plugins/jquery.validate.js",
                "~/Content/plugins/vide/jquery.vide.js",
                "~/Content/plugins/owl-carousel/owl.carousel.js",
                "~/Content/plugins/jquery.browser.js",
                "~/Content/plugins/SmoothScroll.js",
                "~/Content/js/template.js"
                ));

            bundles.Add(new ScriptBundle("~/bundles/bootstrap").Include(
                      "~/Scripts/bootstrap.js",
                      "~/Scripts/respond.js"));

            bundles.Add(new StyleBundle("~/Content/css").Include(
                      "~/Content/css/bootstrap.css",
                      "~/Content/css/style.css",
                      "~/Content/css/skins/light_blue.css",
                      "~/Content/css/site.css"));
            
            bundles.Add(new StyleBundle("~/Content/fonts").Include(
                "~/Content/fonts/font-awesome/css/font-awesome.css",
                "~/Content/fonts/fontello/css/fontello.css"
                ));
            bundles.Add(new StyleBundle("~/Content/plugins").Include(
                "~/Content/plugins/magnific-popup/magnific-popup.css",
		        "~/Content/css/animations.css",
		        "~/Content/plugins/owl-carousel/owl.carousel.css",
		        "~/Content/plugins/owl-carousel/owl.transitions.css",
		        "~/Content/plugins/hover/hover-min.css",
		        "~/Content/plugins/rs-plugin/css/settings.css"
                ));
        }
    }
}
