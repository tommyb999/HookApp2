using Hook.Data.Services;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Text;
using System.IO;

namespace Hook.Web.Controllers
{
    public class HomeController : Controller
    {
        IData db;

        public HomeController(IData db)
        {
            this.db = db;

        }

        public ActionResult Index()
        {
            var model = db.GetAll();

            return View(model);
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