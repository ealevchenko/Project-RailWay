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
    [Table("KOMETA.SOBSTV_FOR_NAKL")]
    public partial class KometaSobstvForNakl
    {
        [Key, Column(Order = 2)]      
        public string NPLAT { get; set; }
        [Key, Column(Order = 1)]  
        public int SOBSTV { get; set; }
        public string ABR { get; set; }
        public int? SOD_PLAT { get; set; }  
        public int? ID { get; set; }      
        public int? ID2 { get; set; }    
    }
}
