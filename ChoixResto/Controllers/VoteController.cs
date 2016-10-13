using ChoixResto.Models;
using ChoixResto.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace ChoixResto.Controllers
{
    public class VoteController : Controller
    {
        private IDal dal;

        public VoteController()
            : this(new Dal())
        {
        }

        public VoteController(IDal dalIoc)
        {
            dal = dalIoc;
        }

        public ActionResult Index(int idSondage)
        {
            //Objet à retourner, contient les restos, l'id du sondage et l'éventuel message d'erreur
            VoteRestoViewModel voteRestoViewModel = new VoteRestoViewModel();

            voteRestoViewModel.listResto = dal.ObtientTousLesRestaurants();
            voteRestoViewModel.idSondage = idSondage;
            voteRestoViewModel.erreur = string.Empty;

            return View(voteRestoViewModel);
        }
    }
}