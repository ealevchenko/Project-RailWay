namespace EFRailWay.Entities.Railcars
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class VAG_CONDITIONS2
    {
        public VAG_CONDITIONS2()
        {
            VAG_CONDITIONS21 = new HashSet<VAG_CONDITIONS2>();
            VAGON_OPERATIONS = new HashSet<VAGON_OPERATIONS>();
            WAYS = new HashSet<WAYS>();
        }

        [Key]
        public int id_cond { get; set; }

        [StringLength(50)]
        public string name { get; set; }

        public int? id_cond_after { get; set; }

        public int? order { get; set; }

        public virtual ICollection<VAG_CONDITIONS2> VAG_CONDITIONS21 { get; set; }

        public virtual VAG_CONDITIONS2 VAG_CONDITIONS22 { get; set; }

        public virtual ICollection<VAGON_OPERATIONS> VAGON_OPERATIONS { get; set; }

        public virtual ICollection<WAYS> WAYS { get; set; }
    }
}
