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

            string utilisateur = Request.Browser.Browser;
            if (dal.ADejaVote(idSondage, utilisateur))
                voteRestoViewModel.erreur = "Vous avez déjà voté.";
            else
                voteRestoViewModel.erreur = string.Empty;

            return View(voteRestoViewModel);
        }

        [HttpPost]
        public ActionResult Index(VoteRestoViewModel voteRestoViewModel)
        {
            Utilisateur utilisateur = dal.ObtenirUtilisateur(Request.Browser.Browser);
            if (dal.ADejaVote(voteRestoViewModel.idSondage, utilisateur.Id.ToString()))
                voteRestoViewModel.erreur = "Vous avez déjà voté.";
            else if (voteRestoViewModel.listResto == null)
            {
                voteRestoViewModel.erreur = "Vous devez choisir au moins un restaurant.";
                voteRestoViewModel.listResto = dal.ObtientTousLesRestaurants();
            }
            else
            {
                foreach (Resto resto in voteRestoViewModel.listResto)
                {
                    dal.AjouterVote(voteRestoViewModel.idSondage, resto.Id, utilisateur.Id);
                }
                voteRestoViewModel.erreur = "Vote pris en compte.";
            }

            return View(voteRestoViewModel);
        }
    }
}