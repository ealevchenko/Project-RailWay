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
    [Table("PROM.GRUZ_SP")]
    public partial class PromGruzSP
    {
        [Key]
        public  int K_GRUZ { get; set; }
        public  string NAME_GR { get; set; }
        public  string ABREV_GR { get; set; }
        public  int GRUP_P { get; set; }
        public  string NGRUP_P { get; set; }
        public  int GRUP_O { get; set; }
        public  int GROUP_OSV { get; set; }
        public  string NGRUP_O { get; set; }
        public  int? TAR_GR { get; set; }
        public  int KOD1 { get; set; }
        public  int KOD2 { get; set; }
        public  int? K_GR { get; set; }
    }
}
