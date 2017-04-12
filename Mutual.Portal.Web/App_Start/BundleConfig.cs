using System.Web;
using System.Web.Optimization;

namespace Mutual.Portal.Web
{
    public class BundleConfig
    {
        // For more information on bundling, visit http://go.microsoft.com/fwlink/?LinkId=301862
        public static void RegisterBundles(BundleCollection bundles)
        {
            bundles.Add(new ScriptBundle("~/bundles/jquery").Include(
                        "~/Scripts/jquery-{version}.js"));

            // Use the development version of Modernizr to develop with and learn from. Then, when you're
            // ready for production, use the build tool at http://modernizr.com to pick only the tests you need.
            bundles.Add(new ScriptBundle("~/bundles/modernizr").Include(
                        "~/Scripts/modernizr-*"));

            bundles.Add(new ScriptBundle("~/bundles/bootstrap").Include(
                      "~/Scripts/bootstrap.js",
                      "~/Scripts/respond.js"));

            bundles.Add(new StyleBundle("~/Content/css").Include(
                      "~/Content/bootstrap.css",
                      "~/Content/site.css",
                      "~/Content/callout.css",
                      "~/Content/bootstrap-social.css",
                      "~/Content/font-awesome.min.css",
                      "~/Content/custom.css"));

            bundles.Add(new StyleBundle("~/app/plugins/css").Include(
                      "~/app/lib/plugins/toastr/angular-toastr.css",
                      "~/app/lib/plugins/sweet-alert/sweetalert2.min.css"));

            bundles.Add(new ScriptBundle("~/app/plugins/js").Include(
                "~/app/lib/plugins/toastr/angular-toastr.tpls.js",
                "~/app/lib/plugins/sweet-alert/sweetalert2.min.js"
                ));

            bundles.Add(new ScriptBundle("~/app/lib").Include(
                      "~/app/lib/angular.min.js",
                      "~/app/lib/angular-cookies.min.js",
                      "~/app/lib/angular-local-storage.min.js",
                      "~/app/lib/angular-touch.min.js",
                      "~/app/lib/validator.js"
                      ));

            bundles.Add(new ScriptBundle("~/app/business").Include(
                      "~/app/app.js",
                      "~/app/utility/*.js",
                      "~/app/controllers/*.js",
                      "~/app/services/*.js"
                      ));
        }
    }
}
