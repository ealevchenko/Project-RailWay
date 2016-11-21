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
    [Table("KOMETA.VAGON_SOB")]
    public partial class KometaVagonSob
    {
        [Key, Column(Order = 1)]
        public  int N_VAGON { get; set; }
        [Key, Column(Order = 2)]
        public  int SOB { get; set; }
        [Key, Column(Order = 3)]
        public  DateTime DATE_AR { get; set; }
        public  DateTime? DATE_END { get; set; }
        public  string ROD { get; set; }
        public  DateTime? DATE_REM { get; set; }
        public  string PRIM { get; set; }
        public  int? CODE { get; set; }
    }
}
