using ChoixResto.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace ChoixResto.Controllers
{
    public class SondageController : Controller
    {
        private IDal dal;

        public SondageController()
            : this(new Dal())
        {
        }

        public SondageController(IDal dalIoc)
        {
            dal = dalIoc;
        }

        public ActionResult Index()
        {
            return View();
        }

        public ActionResult CreerSondage()
        {
            int idSondage = dal.CreerUnSondage();
            return RedirectToAction("Index","Vote",idSondage);
        }
    }
}