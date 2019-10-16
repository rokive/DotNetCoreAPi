using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace Entity.Models
{
    public class Language : Base
    {
        public string LanguageName { get; set; }

        public string ShortName { get; set; }

        [NotMapped]
        public bool isCheck { get; set; }
    }
}
