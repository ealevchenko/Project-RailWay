namespace EFRailWay.Entities
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("RailWay.Code_State")]
    public partial class Code_State
    {
        public Code_State()
        {
            Code_InternalRailroad = new HashSet<Code_InternalRailroad>();
        }

        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int IDState { get; set; }

        [Required]
        [StringLength(50)]
        public string State { get; set; }

        [Required]
        [StringLength(250)]
        public string NameNetwork { get; set; }

        [Required]
        [StringLength(10)]
        public string ABB_RUS { get; set; }

        [Required]
        [StringLength(10)]
        public string ABB_ENG { get; set; }

        public virtual ICollection<Code_InternalRailroad> Code_InternalRailroad { get; set; }
    }
}
