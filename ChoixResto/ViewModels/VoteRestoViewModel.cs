using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ChoixResto.ViewModels
{
    public class VoteRestoViewModel
    {
        public List<Models.Resto> listResto { get; set; }
        public int idSondage { get; set; }
        public string erreur { get; set; }
    }
}