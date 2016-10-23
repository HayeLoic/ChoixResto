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
            /*Objet à retourner, contient les restos, l'id du sondage et l'éventuel message d'erreur
            //VoteRestoViewModel voteRestoViewModel = new VoteRestoViewModel();
            
            voteRestoViewModel.listVoteResto = dal.ObtientTousLesRestaurants();
            voteRestoViewModel.idSondage = idSondage;

            string utilisateur = Request.Browser.Browser;
            if (dal.ADejaVote(idSondage, utilisateur))
                voteRestoViewModel.erreur = "Vous avez déjà voté.";
            else
                voteRestoViewModel.erreur = string.Empty;

            return View(voteRestoViewModel);*/

            //Objet à retourner
            ListVoteRestoViewModel listVoteResto = new ListVoteRestoViewModel();

            //Liste des restos décochés par défaut
            foreach(Resto resto in dal.ObtientTousLesRestaurants())
            {
                listVoteResto.listVoteResto.Add(new VoteRestoViewModel(resto,false));
            }

            //Id du sondage
            listVoteResto.idSondage = idSondage;

            string utilisateur = Request.Browser.Browser;
            if (dal.ADejaVote(idSondage, utilisateur))
                listVoteResto.MessageErreur = "Vous avez déjà voté.";
            else
                listVoteResto.MessageErreur = string.Empty;

            return View(listVoteResto);
        }

        [HttpPost]
        public ActionResult Index(ListVoteRestoViewModel listVoteRestoViewModel)
        {
            Utilisateur utilisateur = dal.ObtenirUtilisateur(Request.Browser.Browser);

            listVoteRestoViewModel.MessageErreur = string.Empty;
            listVoteRestoViewModel.MessageSucces = string.Empty;

            if (dal.ADejaVote(listVoteRestoViewModel.idSondage, utilisateur.Id.ToString()))
                listVoteRestoViewModel.MessageErreur = "Vous avez déjà voté.";
            else if (listVoteRestoViewModel.listVoteResto == null)
            {
                listVoteRestoViewModel.MessageErreur = "Vous devez choisir au moins un restaurant.";
                //listVoteRestoViewModel.listVoteResto = dal.ObtientTousLesRestaurants();
            }
            else
            {
                foreach (VoteRestoViewModel resto in listVoteRestoViewModel.listVoteResto)
                {
                    if(resto.Checked)
                        dal.AjouterVote(listVoteRestoViewModel.idSondage, resto.Resto.Id, utilisateur.Id);
                }
                listVoteRestoViewModel.MessageSucces = "Vote pris en compte.";
            }

            return View(listVoteRestoViewModel);

            /*Utilisateur utilisateur = dal.ObtenirUtilisateur(Request.Browser.Browser);

            //Restos cochés


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
            */

        }
    }
}
 