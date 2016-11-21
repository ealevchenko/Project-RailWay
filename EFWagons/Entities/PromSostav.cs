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
        [Key]
        public int N_NATUR { get; set; }
        public int? D_DD { get; set; }
        public int? D_MM { get; set; }
        public int? D_YY { get; set; }
        public int? T_HH { get; set; }
        public int? T_MI { get; set; }
        public int? K_ST { get; set; }
        public int? N_PUT { get; set; }
        public int? NAPR { get; set; }
        public int? P_OT { get; set; }
        public int? V_P { get; set; }

    }
}
