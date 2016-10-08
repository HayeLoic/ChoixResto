using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ChoixResto.ViewModels
{
    public class VoteRestoViewModel
    {
        public Models.Resto Resto { get; set; }
        public bool RestoChecked { get; set; }
    }
}