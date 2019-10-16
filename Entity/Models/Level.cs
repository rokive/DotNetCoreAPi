using System;
using System.Collections.Generic;
using System.Text;

namespace Entity.Models
{
    public class Level:Base
    {
        public Level()
        {
            TradeDetails = new List<TradeDetails>();
        }
        public string LevelName { get; set; }

        public long TradeId { get; set; }
        public Trade Trade { get; set; }

        public virtual List<TradeDetails> TradeDetails { get; set; }

    }
}
