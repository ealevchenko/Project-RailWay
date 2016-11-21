namespace EFRailWay.Entities.Railcars
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class VAG_CONDITIONS
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int id_cond { get; set; }

        [StringLength(50)]
        public string name { get; set; }

        public int? id_ora { get; set; }

        public int? id_ora2 { get; set; }
    }
}
