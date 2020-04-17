using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Hook.Data.Models;
using Hook.Data.Services;
using Newtonsoft.Json;

namespace Hook.Web.Controllers
{
    public class WorkItemsController : Controller
    {
        private readonly IData db;

        public WorkItemsController(IData db)
        {
            this.db = db;

        }

        public ActionResult Index()
        {
            var model = db.GetAll();

            //Create a file to write to
            string path = @"/Users/MartynBale/dev/HookApp2/json_output.json";

            System.IO.File.WriteAllText(path, JsonConvert.SerializeObject(model));

            return View(model);
        }

        public ActionResult Detail(int id)
        {
            var model = db.Get(id);

            if(model == null)
            {
                return View("NotFound");
            }

            return View(model);
        }


        [HttpGet]
        public ActionResult Edit(int id)
        {
            //Implement the edit function
            var model = db.Get(id);

            if (model == null)
            {
                return View("NotFound");
            }

            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(WorkItem workitem)
        {
            //Implement the create function

            if (ModelState.IsValid)
            {
                db.Update(workitem);
                return RedirectToAction("Index");
            }

            return View(workitem);
        }


        [HttpGet]
        public ActionResult Delete(int id)
        {
            //Implement the delete function
            var model = db.Get(id);

            if (model == null)
            {
                return View("NotFound");
            }

            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(int id, FormCollection form)
        {
            //Implement the create function

            if (ModelState.IsValid)
            {
                db.Delete(id);
                Console.WriteLine("inside");
                return RedirectToAction("Index");
            }

         
            return View();
        }


        [HttpGet]
        public ActionResult Create()
        {

            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(WorkItem workitem)
        {
            //Implement the create function

            if(ModelState.IsValid)
            {
                db.Add(workitem);
                return RedirectToAction("Index");
            }

            return View();
        }

    }
}
