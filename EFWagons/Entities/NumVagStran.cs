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
    [Table("NUM_VAG.STRAN")]
    public partial class NumVagStran
    {
        [Key]
        public  int NPP { get; set; }
        public  string NAME { get; set; }
        public  string ABREV_STRAN { get; set; }
        public  int KOD_EUROP { get; set; }
        public  int KOD_STRAN { get; set; }
        public  int KOD_OLD { get; set; }

    }
}
