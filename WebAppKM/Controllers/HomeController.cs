using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace WebAppKM.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            return View();
        }

        [CustomAuthorize(Roles ="User")]
        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        [CustomAuthorize(Roles = "Administrator")]
        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }
        public ActionResult km_01()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }
        public ActionResult km_02()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }
        public ActionResult km_03()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }
        public ActionResult km_04()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }
        public ActionResult km_05()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }
        public ActionResult km_06()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }
        public ActionResult km_07()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }
        public ActionResult Km_doc()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }
        public ActionResult Km_raws()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }
    }
}