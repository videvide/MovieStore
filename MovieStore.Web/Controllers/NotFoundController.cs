using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace MovieStore.Web.Controllers
{
    public class NotFoundController : Controller
    {
        // GET: NotFound
        public ActionResult Index(string aspxerrorpath)
        {
            ViewBag.NotFoundPath = aspxerrorpath;

            return View();
        }
    }
}