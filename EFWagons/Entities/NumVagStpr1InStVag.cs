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
    [Table("NUM_VAG.STPR1_IN_ST_VAG")]
    public partial class NumVagStpr1InStVag
    {
        [Key, Column(Order = 1)]
        public  int ID_DOC { get; set; }        
        [Key, Column(Order = 2)]
        public  int N_IN_ST { get; set; }
        [Key, Column(Order = 3)]
        public  int N_VAG { get; set; }
        public  int? STRAN_SOBSTV { get; set; }
        public  int? GODN_IN_ST { get; set; }
        public  int GR_IN_ST { get; set; }
        public  int SOBSTV { get; set; }
        public  string REM_IN_ST { get; set; }
        public  int ID_VAG { get; set; }
        public  int? ST_NAZN_OUT_ST { get; set; }
        public  int? STRAN_OUT_ST { get; set; }
        public  int? SOBSTV_OLD { get; set; }
    }
}
