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
    [Table("NUM_VAG.STPR1_GR")]
    public partial class NumVagStpr1Gr
    {
        [Key]
        public  int KOD_GR { get; set; }
        public  string GR { get; set; }
        public  int? OLD { get; set; }
    }
}
