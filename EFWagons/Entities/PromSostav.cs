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
    [Table("PROM.SOSTAV")]
    public partial class PromSostav
    {
        [Key, Column(Order = 1)]
        public int N_NATUR { get; set; }
        [Key, Column(Order = 2)]
        public int? D_DD { get; set; }
        [Key, Column(Order = 3)]
        public int? D_MM { get; set; }
        [Key, Column(Order = 4)]
        public int? D_YY { get; set; }
        [Key, Column(Order = 5)]
        public int? T_HH { get; set; }
        [Key, Column(Order = 6)]
        public int? T_MI { get; set; }
        public int? K_ST { get; set; }
        public int? N_PUT { get; set; }
        public int? NAPR { get; set; }
        public int? P_OT { get; set; }
        public int? V_P { get; set; }
        public int? K_ST_OTPR { get; set; }
        public int? K_ST_PR { get; set; }
        public int? N_VED_PR { get; set; }
        public int? N_SOST_OT { get; set; }
        public int? N_SOST_PR { get; set; }
        public DateTime? DAT_VVOD { get; set; }
    }
}
