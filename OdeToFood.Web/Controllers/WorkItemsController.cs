using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Hook.Data.Models;
using Hook.Data.Services;

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

            db.Add(workitem);
            return View();
        }

    }
}
