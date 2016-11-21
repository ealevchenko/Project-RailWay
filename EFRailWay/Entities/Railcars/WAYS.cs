namespace EFRailWay.Entities.Railcars
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class WAYS
    {
        public WAYS()
        {
            VAGON_OPERATIONS = new HashSet<VAGON_OPERATIONS>();
        }

        [Key]
        public int id_way { get; set; }

        public int? id_stat { get; set; }

        public int? id_park { get; set; }

        [StringLength(20)]
        public string num { get; set; }

        [StringLength(250)]
        public string name { get; set; }

        public int? vag_capacity { get; set; }

        public int? order { get; set; }

        public int? bind_id_cond { get; set; }

        public int? for_rospusk { get; set; }

        public virtual VAG_CONDITIONS2 VAG_CONDITIONS2 { get; set; }

        public virtual ICollection<VAGON_OPERATIONS> VAGON_OPERATIONS { get; set; }
    }
}
