using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EFWagons.Entities
{
    [Table("PROM.CEX")]
    public partial class PromCex
    {
        [Key]
        public  int K_PODR { get; set; }
        public  string NAME_P { get; set; }
        public  string ABREV_P { get; set; }
    }
}
