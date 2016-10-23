using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ChoixResto.ViewModels
{
    public class ListVoteRestoViewModel
    {
        public int idSondage { get; set; }
        public string MessageErreur { get; set; }
        public string MessageSucces { get; set; }
        public List<VoteRestoViewModel> listVoteResto { get; set; }

        public ListVoteRestoViewModel()
        {
            listVoteResto = new List<VoteRestoViewModel>();
        }
    }
}