using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace ProjektNET.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult SetTlo(string kolor)
        {
            Session["Tlo"] = kolor;
            return View("Index");
        }

        public ContentResult GetTlo()
        {
            string style;
            if (Session["Tlo"] != null)
            {
                style = String.Format("background-color: {0};", Session["Tlo"]);
            }
            else
            {
                style = "background-color: #FFFFFF;";
            }
            return Content(style);
        }

        public ActionResult Index()
        {
            return View();
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }
    }
}