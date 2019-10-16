using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace Entity.Models
{
    public class TradeDetails :Base
    {
        public long TradeId { get; set; }

        public long LevelId { get; set; }
        public Level Level { get; set; }

        [Required]
        public string Language { get; set; }

        [Required]
        public string SyllabusName { get; set; }

        [Required]
        public string SyllabusFile { get; set; }

        [Required]
        public string PlanFile { get; set; }

        [Required]
        public string DevelopmentOfficer { get; set; }

        [Required]
        public string Manager { get; set; }

        public DateTime ActiveDate
        {
            get
            {
                return this.activeDate.HasValue
                   ? this.activeDate.Value
                   : DateTime.Now;
            }

            set { this.activeDate = value; }
        }

        private DateTime? activeDate = null;

        [NotMapped]
        public virtual List<Language> LanguageList { get; set; }

        [NotMapped]
        public virtual List<Level> LevelList { get; set; }

        [NotMapped]
        public virtual List<Trade> TradeList { get; set; }
    }
}
