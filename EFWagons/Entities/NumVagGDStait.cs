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
    [Table("NUM_VAG.GDSTAIT")]
    public partial class NumVagGDStait
    {
        [Key]
        public  int CODGD { get; set; }
        public  string NAMEGD { get; set; }
        public  int N_GD { get; set; }
        public  int EXP { get; set; }
        public  int ST_PEREXOD { get; set; }
        public  int SPC11 { get; set; }
        public  int SPC3 { get; set; }
        public  int PROM { get; set; }
        public  int VOST { get; set; }        
        public  int CPMP { get; set; }
        public  DateTime? DAT_INSERT { get; set; }
    }
}
