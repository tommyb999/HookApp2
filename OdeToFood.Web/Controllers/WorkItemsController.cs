using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Hook.Data.Services;

namespace OdeToFood.Web.Controllers
{
    public class WorkItemsController : Controller
    {
        IData db;

        public WorkItemsController()
        {
            db = new InMemoryWorkItemData();

        }

        public ActionResult Index()
        {
            var model = db.GetAll();

            return View(model);
        }
    }
}
