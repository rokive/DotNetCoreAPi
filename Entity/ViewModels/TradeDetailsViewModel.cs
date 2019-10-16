using Entity.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace Entity.ViewModels
{
    public class TradeDetailsViewModel
    {
        public TradeDetailsViewModel()
        {
            TradeDetailsList = new List<TradeDetails>();
        }
        public int TotalPageNo { get; set; }
        public int TotalItem { get; set; }
        public List<TradeDetails> TradeDetailsList { get; set; }
    }
}
