namespace EFRailWay.Entities
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("RailWay.LogErrors")]
    public partial class LogErrors
    {
        [Key]
        public int IDLogError { get; set; }

        public DateTime DateTime { get; set; }

        [Required]
        [StringLength(50)]
        public string UserEnterprise { get; set; }

        [Required]
        [StringLength(50)]
        public string MachineName { get; set; }

        [Required]
        [StringLength(1024)]
        public string url { get; set; }

        public int TypeError { get; set; }

        [Required]
        [StringLength(516)]
        public string Source { get; set; }

        [StringLength(516)]
        public string TargetSite { get; set; }

        [StringLength(516)]
        public string TargetSite_DeclaringType { get; set; }

        [StringLength(516)]
        public string TargetSite_MemberType { get; set; }

        public int? CodeError { get; set; }

        [Required]
        [StringLength(1024)]
        public string MessageError { get; set; }

        [StringLength(2048)]
        public string StackTrace { get; set; }
    }
}
