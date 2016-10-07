using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace ChoixResto.Controllers
{
    public class SondageController : Controller
    {
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult CreerSondage()
        {
            return View();
        }
    }
}