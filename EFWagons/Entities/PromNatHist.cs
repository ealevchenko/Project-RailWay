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
    [Table("PROM.NAT_HIST")]
    public partial class PromNatHist
    {
        [Key]
        public  int N_VAG { get; set; }
        public  int NPP { get; set; }
        public  int? GODN { get; set; }
        public  int K_ST { get; set; }
        public  int N_NATUR { get; set; }
        // дата
        public  int? D_PR_DD { get; set; }
        public  int? D_PR_MM { get; set; }
        public  int? D_PR_YY { get; set; }
        public  int? T_PR_HH { get; set; }
        public  int? T_PR_MI { get; set; }
    }
}
