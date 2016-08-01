using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ChoixResto.Models
{
    public class Resultats
    {
        public int IdResto { get; set; }
        public string Nom { get; set; }
        public string Telephone { get; set; }
        public int NombreDeVotes { get; set; }
    }
}