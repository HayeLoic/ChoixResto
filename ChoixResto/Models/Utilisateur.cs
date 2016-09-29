using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace ChoixResto.Models
{
    public class Utilisateur
    {
        public int Id { get; set; }
        [Required, MaxLength(50)]
        public string Prenom { get; set; }
        public string MotDePasse { get; set; }
        [Range(18, 120)]
        public int Age { get; set; }
    }
}