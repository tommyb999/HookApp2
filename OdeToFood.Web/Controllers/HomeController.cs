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

        public HomeController()
        {
            db = new InMemoryWorkItemData();

        }

        public ActionResult Index()
        {
            var model = db.GetAll();


            //Create a file to write to
            string path = @"/Users/MartynBale/dev/HookApp/json_output.json";
            using (StreamWriter file = new StreamWriter(path))
            {
                // Adding the json output
                foreach (var item in model)
                {
                    file.WriteLine(JsonConvert.SerializeObject(item));
                    Console.WriteLine(JsonConvert.SerializeObject(item));
                }
            }
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