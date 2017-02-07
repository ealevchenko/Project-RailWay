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
    [Table("NUM_VAG.STPR1_TUPIK")]
    public partial class NumVagStpr1Tupik
    {
        [Key, Column(Order = 1)]
        public int ID_CEX { get; set; }
        [Key, Column(Order = 2)]
        public int ID_CEX_TUPIK { get; set; }
        public string NAMETUPIK { get; set; }
    }
}
