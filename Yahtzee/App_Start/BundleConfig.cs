using System.Web;
using System.Web.Optimization;

namespace Yahtzee
{
    public class BundleConfig
    {
        // For more information on bundling, visit http://go.microsoft.com/fwlink/?LinkId=301862
        public static void RegisterBundles(BundleCollection bundles)
        {
            bundles.Add(new ScriptBundle("~/bundles/angular").Include(
                        "~/Scripts/angular.js",
                        "~/Scripts/angular-route.js",
                        "~/signalr/js",
                        "~/Scripts/bootstrap.js",
                        "~/Scripts/respond.js",
                        "~/Angular/Yahtzee.js",
                        "~/Angular/Components/panel/TabPanelController.js",
                        "~/Angular/Components/home/HomePageController.js",
                        "~/Angular/Components/login/LoginController.js",
                        "~/Angular/Components/register/RegisterController.js",
                        "~/Angular/Components/profile/ProfileController.js",
                        "~/Angular/Components/profile/games/GamesSectionController.js",
                        "~/Angular/Components/profile/play/PlaySectionController.js",
                        "~/Angular/Components/profile/user/UserSectionController.js",
                        "~/Angular/Components/profile/load/LoadedSectionController.js",
                        "~/Angular/Services/loginService.js",
                        "~/Angular/Services/userService.js",
                        "~/Angular/Services/gameService.js",
                        "~/Angular/Services/chatService.js")
                  );

            // Use the development version of Modernizr to develop with and learn from. Then, when you're
            // ready for production, use the build tool at http://modernizr.com to pick only the tests you need.
            bundles.Add(new ScriptBundle("~/bundles/modernizr").Include(
                        "~/Scripts/modernizr-*"));

        

            bundles.Add(new StyleBundle("~/Content/css").Include(
                      "~/Content/bootstrap.css",
                      "~/Content/materialdesignicons.min.css",
                      "~/Content/site.css"));

            BundleTable.EnableOptimizations = false;

        }
    }
}
