namespace EFRailWay.Entities.Railcars
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class PARKS
    {
        [Key]
        public int id_park { get; set; }

        [StringLength(250)]
        public string name { get; set; }

        public int? id_stat { get; set; }
    }
}
