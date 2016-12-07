namespace EFRailWay.Entities.Railcars
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class VAGON_OPERATIONS
    {
        [Key]
        public int id_oper { get; set; }

        [Column(TypeName = "datetime2")]
        public DateTime? dt_amkr { get; set; }

        public int? n_natur { get; set; }

        public int? id_vagon { get; set; }

        public int? id_stat { get; set; }

        [Column(TypeName = "datetime2")]
        public DateTime? dt_from_stat { get; set; }

        [Column(TypeName = "datetime2")]
        public DateTime? dt_on_stat { get; set; }

        public int? id_way { get; set; }

        [Column(TypeName = "datetime2")]
        public DateTime? dt_from_way { get; set; }

        [Column(TypeName = "datetime2")]
        public DateTime? dt_on_way { get; set; }

        public int? num_vag_on_way { get; set; }

        public int? is_present { get; set; }

        public int? id_locom { get; set; }

        public int? id_locom2 { get; set; }

        public int? id_cond2 { get; set; }

        public int? id_gruz { get; set; }

        public int? id_gruz_amkr { get; set; }

        public int? id_shop_gruz_for { get; set; }

        [Column(TypeName = "numeric")]
        public decimal? weight_gruz { get; set; }

        public int? id_tupik { get; set; }

        public int? id_nazn_country { get; set; }

        public int? id_gdstait { get; set; }

        public int? id_cond { get; set; }

        [StringLength(500)]
        public string note { get; set; }

        public int? is_hist { get; set; }

        public int? id_oracle { get; set; }

        public int? lock_id_way { get; set; }

        public int? lock_order { get; set; }

        public int? lock_side { get; set; }

        public int? lock_id_locom { get; set; }

        public int? st_lock_id_stat { get; set; }

        public int? st_lock_order { get; set; }

        public int? st_lock_train { get; set; }

        public int? st_lock_side { get; set; }

        public int? st_gruz_front { get; set; }

        public int? st_shop { get; set; }

        public int? oracle_k_st { get; set; }

        public int? st_lock_locom1 { get; set; }

        public int? st_lock_locom2 { get; set; }

        public int? id_oper_parent { get; set; }

        [StringLength(50)]
        public string grvu_SAP { get; set; }

        [StringLength(80)]
        public string ngru_SAP { get; set; }

        public int? id_ora_23_temp { get; set; }

        [StringLength(100)]
        public string edit_user { get; set; }

        public DateTime? edit_dt { get; set; }

        public int? IDSostav { get; set; }

        public int? num_vagon { get; set; }

        [Column(TypeName = "datetime")]
        public DateTime? dt_uz { get; set; }

        [Column(TypeName = "datetime")]
        public DateTime? dt_out_amkr { get; set; }

        public virtual GDSTAIT GDSTAIT { get; set; }

        public virtual GRUZS GRUZS { get; set; }

        public virtual NAZN_COUNTRIES NAZN_COUNTRIES { get; set; }

        public virtual SHOPS SHOPS { get; set; }

        public virtual STATIONS STATIONS { get; set; }

        public virtual TUPIKI TUPIKI { get; set; }

        public virtual VAG_CONDITIONS2 VAG_CONDITIONS2 { get; set; }

        public virtual VAGONS VAGONS { get; set; }

        public virtual WAYS WAYS { get; set; }
    }
}
