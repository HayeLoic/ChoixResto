using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ChoixResto.ViewModels
{
    public class VoteRestoViewModel
    {
        /*public List<Models.Resto> listResto { get; set; }
        public int idSondage { get; set; }
        public string erreur { get; set; }*/
        public Models.Resto Resto { get; set; }
        public bool Checked { get; set; }

        public VoteRestoViewModel(Models.Resto _Resto, bool _Checked)
        {
            Resto = _Resto;
            Checked = _Checked;
        }

    }
}
