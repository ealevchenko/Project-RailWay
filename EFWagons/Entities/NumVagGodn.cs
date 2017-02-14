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
    [Table("NUM_VAG.GODN")]
    public partial class NumVagGodn
    {
        [Key]
        public int CODE { get; set; }
        public string NAME { get; set; }
        public string BIG_NAME { get; set; }
        public DateTime? ANNUL { get; set; }

    }
}
