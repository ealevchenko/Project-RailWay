namespace EFRailWay.Entities.KIS
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("RailWay.Oracle_RulesCopy")]
    public partial class Oracle_RulesCopy
    {
        [Key]
        public int IDRulesCopy { get; set; }

        public int TypeCopy { get; set; }

        public int IDStationOn { get; set; }

        public int IDStationFrom { get; set; }
    }
}
