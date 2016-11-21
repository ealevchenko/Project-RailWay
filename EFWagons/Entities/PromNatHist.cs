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
        [Key, Column(Order = 2)]
        public  int N_VAG { get; set; }
        public  int NPP { get; set; }
        public  int? GODN { get; set; }
        public  int K_ST { get; set; }
        [Key, Column(Order = 1)]
        public  int N_NATUR { get; set; }

        // дата
        public  int? D_PR_DD { get; set; }
        public  int? D_PR_MM { get; set; }
        public  int? D_PR_YY { get; set; }
        public  int? T_PR_HH { get; set; }
        public  int? T_PR_MI { get; set; }

        
        public int? K_POL_GR { get; set; }
        public int? K_GR { get; set; }        
        public int? N_VED_PR { get; set; }
        public int? N_NAK_MPS { get; set; }
        public int? N_NAK_KMK { get; set; }
        public decimal? WES_GR { get; set; }
        public int? K_OP { get; set; }
        public int? K_FRONT { get; set; }
        public int? KOD_STRAN { get; set; }

        public DateTime? DAT_VVOD { get; set; }
    }
}
