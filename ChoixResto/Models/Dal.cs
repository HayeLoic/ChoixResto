using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ChoixResto.Models
{
    public class Dal : IDal
    {
        private BddContext bdd;

        public Dal()
        {
            bdd = new BddContext();
        }

        public void Dispose()
        {
            bdd.Dispose();
        }

        #region Resto
        public List<Resto> ObtientTousLesRestaurants()
        {
            return bdd.Restos.ToList();
        }

        public void CreerRestaurant(string nom, string telephone)
        {
            bdd.Restos.Add(new Resto { Nom = nom, Telephone = telephone });
            bdd.SaveChanges();
        }

        public void ModifierRestaurant(int id, string nom, string telephone)
        {
            Resto restoTrouve = bdd.Restos.FirstOrDefault(resto => resto.Id == id);
            if (restoTrouve != null)
            {
                restoTrouve.Nom = nom;
                restoTrouve.Telephone = telephone;
                bdd.SaveChanges();
            }
        }

        public bool RestaurantExiste(string nom)
        {
            Resto restoTrouve = bdd.Restos.FirstOrDefault(resto => resto.Nom == nom);
            return (restoTrouve == null ? false : true);
        }
        #endregion

        #region Utilisateur
        public Utilisateur ObtenirUtilisateur(int id)
        {
            return bdd.Utilisateurs.FirstOrDefault(util => util.Id == id);
        }

        public Utilisateur ObtenirUtilisateur(string prenom)
        {
            //Si on passe un int entre quotes, on appel l'autre méthode
            int n;
            bool isNumeric = int.TryParse(prenom, out n);

            if (isNumeric)
                return ObtenirUtilisateur(n);
            else
                return bdd.Utilisateurs.FirstOrDefault(util => util.Prenom == prenom);
        }

        public int AjouterUtilisateur(string prenom, string motDePasse)
        {
            bdd.Utilisateurs.Add(new Utilisateur { Prenom = prenom, MotDePasse = motDePasse });
            bdd.SaveChanges();
            return bdd.Utilisateurs.FirstOrDefault(util => util.Prenom == prenom).Id;
        }

        public Utilisateur Authentifier(string prenom, string motDePasse)
        {
            return bdd.Utilisateurs.FirstOrDefault(util => util.Prenom == prenom && util.MotDePasse == motDePasse);
        }
        #endregion

        #region Vote
        public bool ADejaVote(int idSondage, string idUtilisateur)
        {
            Utilisateur utilisateurTrouve = ObtenirUtilisateur(idUtilisateur);
            if (utilisateurTrouve == null)
                return false;
            else
            {
                Vote voteTrouve = bdd.Votes.FirstOrDefault(vote => vote.Sondage.Id == idSondage && vote.Utilisateur.Id == utilisateurTrouve.Id);
                return (voteTrouve == null ? false : true);
            }
        }
        #endregion

        #region Sondage
        public int CreerUnSondage()
        {
            DateTime datetimeSondage = DateTime.Now;
            bdd.Sondages.Add(new Sondage { Date = datetimeSondage });
            bdd.SaveChanges();
            return bdd.Sondages.FirstOrDefault(sondage => sondage.Date == datetimeSondage).Id;
        }
        #endregion
    }
}