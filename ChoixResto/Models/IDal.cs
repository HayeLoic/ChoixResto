using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChoixResto.Models
{
    public interface IDal : IDisposable
    {
        //Méthodes resto
        void CreerRestaurant(string nom, string telephone);
        void ModifierRestaurant(int id, string nom, string telephone);
        bool RestaurantExiste(string nom);
        List<Resto> ObtientTousLesRestaurants();

        //Méthodes utilisateur
        int AjouterUtilisateur(string prenom, string mdp);
        Utilisateur ObtenirUtilisateur(int id);
        Utilisateur ObtenirUtilisateur(string prenom);
        Utilisateur Authentifier(string prenom, string mdp);

        //Méthodes vote
        bool ADejaVote(int idSondage, string idUtilisateur);

        //Méthodes sondage
        int CreerUnSondage();
    }
}
