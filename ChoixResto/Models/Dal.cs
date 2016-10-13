using System;
using System.Collections.Generic;
using System.Data.Entity;
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
        public Utilisateur ObtenirUtilisateur(string idStr)
        {
            switch (idStr)
            {
                case "Chrome":
                    return CreeOuRecupere("Chrome", "1234");
                case "IE":
                    return CreeOuRecupere("IE", "1234");
                case "Firefox":
                    return CreeOuRecupere("Firefox", "1234");
                default:
                    return CreeOuRecupere("Autre", "1234");
            }
        }

        private Utilisateur CreeOuRecupere(string nom, string motDePasse)
        {
            Utilisateur utilisateur = Authentifier(nom, motDePasse);
            if (utilisateur == null)
            {
                int id = AjouterUtilisateur(nom, motDePasse);
                return ObtenirUtilisateur(id);
            }
            return utilisateur;
        }

        public Utilisateur ObtenirUtilisateur(int id)
        {
            return bdd.Utilisateurs.FirstOrDefault(util => util.Id == id);
        }

        /*public Utilisateur ObtenirUtilisateur(string prenom)
        {
            //Si on passe un int entre quotes, on appel l'autre méthode
            int n;
            bool isNumeric = int.TryParse(prenom, out n);

            if (isNumeric)
                return ObtenirUtilisateur(n);
            else
                return bdd.Utilisateurs.FirstOrDefault(util => util.Prenom == prenom);
        }*/

        public int AjouterUtilisateur(string prenom, string motDePasse)
        {
            //Création de l'utilisateur
            Utilisateur utilisateur = new Utilisateur { Prenom = prenom, MotDePasse = motDePasse };
            bdd.Utilisateurs.Add(utilisateur);
            bdd.SaveChanges();

            //On retourne son id
            return utilisateur.Id;
        }

        public Utilisateur Authentifier(string prenom, string motDePasse)
        {
            return bdd.Utilisateurs.FirstOrDefault(util => util.Prenom == prenom && util.MotDePasse == motDePasse);
        }
        #endregion

        #region Vote
        public void AjouterVote(int idSondage, int idResto, int idUtilisateur)
        {
            //On récupère le sondage, le resto et l'utilisateur données en param
            Sondage sondageTrouve = bdd.Sondages.FirstOrDefault(sondage => sondage.Id == idSondage);
            Resto restoTrouve = bdd.Restos.FirstOrDefault(resto => resto.Id == idResto);
            Utilisateur utilisateurTrouve = ObtenirUtilisateur(idUtilisateur);

            if (sondageTrouve != null && restoTrouve != null && utilisateurTrouve != null)
            {
                //Création de l'objet vote
                Vote vote = new Vote { Resto = restoTrouve, Utilisateur = utilisateurTrouve };

                //Création du vote en base
                bdd.Votes.Add(vote);

                //Si c'est le 1er vote ajouté, instancier la liste
                if (sondageTrouve.Votes == null) sondageTrouve.Votes = new List<Vote>();

                //Rattachement du vote au sondage
                sondageTrouve.Votes.Add(vote);

                bdd.SaveChanges();
            }
        }
        /*
        public bool ADejaVote(int idSondage, string idUtilisateur)
        {
            bool _ADejaVote = false;

            //On récupère l'utilisateur donnée en param
            Utilisateur utilisateurTrouve = ObtenirUtilisateur(idUtilisateur);

            if (utilisateurTrouve != null)
            {
                //le sondage correspondant à l'id en param
                Sondage sondageTrouve = bdd.Sondages.FirstOrDefault(sondage => sondage.Id == idSondage);

                //Si le sondage existe, on regarde si un vote est fait par l'utilisateur en param
                if (sondageTrouve != null)
                {
                    //On récupère la liste des votes faits pour ce sondage
                    List<Vote> votes = sondageTrouve.Votes;

                    if (votes != null)
                    {
                        Vote voteTrouve = votes.FirstOrDefault(vote => vote.Utilisateur.Id == utilisateurTrouve.Id);

                        //Si on trouve un vote dans la liste, c'est que l'utilisateur a deja voté
                        if (voteTrouve != null) _ADejaVote = true;
                    }
                }
            }
            return _ADejaVote;
        }*/
        public bool ADejaVote(int idSondage, string idStr)
        {
            Utilisateur utilisateur = ObtenirUtilisateur(idStr);
            if (utilisateur != null)
            {
                Sondage sondage = bdd.Sondages.First(s => s.Id == idSondage);
                if (sondage.Votes == null)
                    return false;
                return sondage.Votes.Any(v => v.Utilisateur != null && v.Utilisateur.Id == utilisateur.Id);
            }
            return false;
        }
        #endregion

        #region Sondage
        public int CreerUnSondage()
        {
            DateTime datetimeSondage = DateTime.Now;

            //Création du sondage
            Sondage sondage = new Sondage { Date = datetimeSondage };
            bdd.Sondages.Add(sondage);
            bdd.SaveChanges();

            //On retourne son id
            return sondage.Id;
        }
        #endregion

        #region Resultats
        public List<Resultats> ObtenirLesResultats(int idSondage)
        {
            //liste des résultats à générer
            List<Resultats> lesResultats = new List<Resultats>();

            //On récupère le sondage concerné
            Sondage sondageTrouve = bdd.Sondages.FirstOrDefault(sondage => sondage.Id == idSondage);

            //On récupère les votes de ce sondage
            List<Vote> votesTrouves= sondageTrouve.Votes;


            //Analyse des votes de la liste
            foreach (Vote vote in votesTrouves)
            {
                //On récupère la ligne de résultat qui correspond au resto du vote analysé
                Resultats resultats = lesResultats.FirstOrDefault(resultat => resultat.IdResto == vote.Resto.Id);


                //Si le vote concerne un resto pas encore dans les résultats => création du résultat
                if (resultats == null)
                {
                    lesResultats.Add(new Resultats { IdResto = vote.Resto.Id, Nom = vote.Resto.Nom, Telephone = vote.Resto.Telephone, NombreDeVotes = 1});
                }

                //Si le vote concerne un resto deja dans les résultats => incrémente lnb de votes
                else
                {
                    resultats.NombreDeVotes++;
                }
            }

            return lesResultats;
        }
        #endregion
    }
}