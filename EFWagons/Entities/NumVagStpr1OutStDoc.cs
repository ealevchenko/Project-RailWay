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
    [Table("NUM_VAG.STPR1_OUT_ST_DOC")]
    public partial class NumVagStpr1OutStDoc
    {
        [Key, Column(Order = 1)]
        public  int ID_DOC { get; set; }        
        [Key, Column(Order = 2)]
        public  DateTime DATE_OUT_ST { get; set; }
        public  int? ST_OUT_ST { get; set; }
        public  int? N_PUT_OUT_ST { get; set; }
        public  int? NAPR_OUT_ST { get; set; }
        public  int? FIO_IN_ST { get; set; }
        public  int? CEX { get; set; }
        public  int? K_STAN { get; set; }
        public  DateTime? DATE_ST { get; set; }
        public  int? STATUS { get; set; }
        public  string NAME_ST { get; set; }     	
    }
}
