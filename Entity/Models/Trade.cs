using System;
using System.Collections.Generic;
using System.Text;

namespace Entity.Models
{
    public class Trade : Base
    {
        public Trade()
        {
            Level = new List<Level>();
        }
        public string TradeName { get; set; }

        public virtual List<Level> Level { get; set; }
        
    }
}
