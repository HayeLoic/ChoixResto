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

        public void AjouterUtilisateur(string prenom, string motDePasse)
        {
            bdd.Utilisateurs.Add(new Utilisateur { Prenom = prenom, MotDePasse = motDePasse });
            bdd.SaveChanges();
        }

        public Utilisateur Authentifier(string prenom, string motDePasse)
        {
            return bdd.Utilisateurs.FirstOrDefault(util => util.Prenom == prenom && util.MotDePasse == motDePasse);
        }
        #endregion
    }
}