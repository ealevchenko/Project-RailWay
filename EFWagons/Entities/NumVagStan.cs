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
    [Table("NUM_VAG.STAN")]
    public partial class NumVagStan
    {
        [Key]
        public  int K_STAN { get; set; }
        public  string NAME { get; set; }
        public  string ABREV { get; set; }
        public  string LITERA { get; set; }
        public  int? PHONE { get; set; }
        public  bool? P_AVTOM { get; set; }
        public  string MACHINE { get; set; }
        public  int? UCH { get; set; }
        public  bool? MPS { get; set; }
        public  int? KIND_STAN { get; set; }
    }
}
