using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

[assembly: log4net.Config.XmlConfigurator(Watch = true)]

namespace Yahtzee.Controllers
{
    public class HomeController : Controller
    {
        private static readonly log4net.ILog logger = log4net.LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);
        public ActionResult Index()
        {
            ViewBag.Title = "Home Page";
            logger.Debug("Index page accessed");
            return View();
        }

    }
}
