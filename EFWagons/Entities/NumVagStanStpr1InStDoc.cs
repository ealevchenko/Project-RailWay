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
    [Table("NUM_VAG.STPR1_IN_ST_DOC")]
    public partial class NumVagStanStpr1InStDoc
    {
        [Key, Column(Order = 1)]
        public  int ID_DOC { get; set; }        
        [Key, Column(Order = 2)]
        public  DateTime DATE_IN_ST { get; set; }
        public  int? ST_IN_ST { get; set; }
        public  int? N_PUT_IN_ST { get; set; }
        public  int? NAPR_IN_ST { get; set; }
        public  int? FIO_IN_ST { get; set; }
        public  int? CEX { get; set; }
        public  int? N_POST { get; set; }
        public  int? K_STAN { get; set; }
        public  int? OLD_N_NATUR { get; set; }        	
    }
}
