using EFRailWay.Abstract;
using EFRailWay.Entities;
using EFRailWay.Entities.KIS;
using EFRailWay.Entities.Railcars;
using EFRailWay.Entities.SAP;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EFRailWay.Concrete
{
    
    public class EFDbContext : DbContext
    {
        public EFDbContext()
            : base("name=Railcars")
        {
        }

        // Справочники ЖД
        public virtual DbSet<Code_Cargo> Code_Cargo { get; set; }
        public virtual DbSet<Code_InternalRailroad> Code_InternalRailroad { get; set; }
        public virtual DbSet<Code_State> Code_State { get; set; }
        public virtual DbSet<Code_Station> Code_Station { get; set; }
        public virtual DbSet<Code_Country> Code_Country { get; set; }

        // Справочники МеталлургТранс
        public virtual DbSet<MTList> MTList { get; set; }
        public virtual DbSet<MTSostav> MTSostav { get; set; }
        public virtual DbSet<MTConsignee> MTConsignee { get; set; }
        // Настройки Settings
        public virtual DbSet<appSettings> appSettings { get; set; }
        public virtual DbSet<TypeValue> TypeValue { get; set; }
        public virtual DbSet<Project> Project { get; set; }
        // Системные
        public virtual DbSet<LogErrors> LogErrors { get; set; }
        // Временные данные пока работаем с КИС
        public virtual DbSet<Oracle_ArrivalSostav> Oracle_ArrivalSostav { get; set; }
        public virtual DbSet<Oracle_RulesCopy> Oracle_RulesCopy { get; set; }
        public virtual DbSet<Oracle_InputSostav> Oracle_InputSostav { get; set; }

        // Данные старой системы Railcars
        public virtual DbSet<GDSTAIT> GDSTAIT { get; set; }
        public virtual DbSet<GRUZ_FRONTS> GRUZ_FRONTS { get; set; }
        public virtual DbSet<GRUZS> GRUZS { get; set; }
        public virtual DbSet<NAZN_COUNTRIES> NAZN_COUNTRIES { get; set; }
        public virtual DbSet<OWNERS> OWNERS { get; set; }
        public virtual DbSet<OWNERS_COUNTRIES> OWNERS_COUNTRIES { get; set; }
        public virtual DbSet<SHOPS> SHOPS { get; set; }
        public virtual DbSet<STATIONS> STATIONS { get; set; }
        public virtual DbSet<TUPIKI> TUPIKI { get; set; }
        public virtual DbSet<VAG_CONDITIONS> VAG_CONDITIONS { get; set; }
        public virtual DbSet<VAG_CONDITIONS2> VAG_CONDITIONS2 { get; set; }
        public virtual DbSet<VAGONS> VAGONS { get; set; }
        public virtual DbSet<WAYS> WAYS { get; set; }
        public virtual DbSet<PARKS> PARKS { get; set; }
        public virtual DbSet<VAGON_OPERATIONS> VAGON_OPERATIONS { get; set; }
        //SAP
        public virtual DbSet<SAPIncSupply> SAPIncSupply { get; set; }
        // Справочники системы Railway
        public virtual DbSet<ReferenceCargo> ReferenceCargo { get; set; }
        public virtual DbSet<ReferenceCountry> ReferenceCountry { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Code_State>()
                .HasMany(e => e.Code_InternalRailroad)
                .WithRequired(e => e.Code_State)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<MTSostav>()
                .HasMany(e => e.MTList)
                .WithRequired(e => e.MTSostav)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<Project>()
                .HasMany(e => e.appSettings)
                .WithRequired(e => e.Project)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<TypeValue>()
                .HasMany(e => e.appSettings)
                .WithRequired(e => e.TypeValue)
                .WillCascadeOnDelete(false);

            // Данные старой системы Railcars
            modelBuilder.Entity<GRUZS>()
                .HasMany(e => e.VAGON_OPERATIONS)
                .WithOptional(e => e.GRUZS)
                .HasForeignKey(e => e.id_gruz_amkr);

            modelBuilder.Entity<NAZN_COUNTRIES>()
                .Property(e => e.id_ora)
                .IsFixedLength();

            modelBuilder.Entity<NAZN_COUNTRIES>()
                .HasMany(e => e.VAGON_OPERATIONS)
                .WithOptional(e => e.NAZN_COUNTRIES)
                .HasForeignKey(e => e.id_nazn_country);

            modelBuilder.Entity<OWNERS_COUNTRIES>()
                .Property(e => e.name)
                .IsFixedLength();

            modelBuilder.Entity<OWNERS_COUNTRIES>()
                .HasMany(e => e.OWNERS)
                .WithOptional(e => e.OWNERS_COUNTRIES)
                .HasForeignKey(e => e.id_country);

            modelBuilder.Entity<SHOPS>()
                .HasMany(e => e.VAGON_OPERATIONS)
                .WithOptional(e => e.SHOPS)
                .HasForeignKey(e => e.id_shop_gruz_for);

            modelBuilder.Entity<VAG_CONDITIONS2>()
                .HasMany(e => e.VAG_CONDITIONS21)
                .WithOptional(e => e.VAG_CONDITIONS22)
                .HasForeignKey(e => e.id_cond_after);

            modelBuilder.Entity<VAG_CONDITIONS2>()
                .HasMany(e => e.VAGON_OPERATIONS)
                .WithOptional(e => e.VAG_CONDITIONS2)
                .HasForeignKey(e => e.id_cond2);

            modelBuilder.Entity<VAG_CONDITIONS2>()
                .HasMany(e => e.WAYS)
                .WithOptional(e => e.VAG_CONDITIONS2)
                .HasForeignKey(e => e.bind_id_cond);

            modelBuilder.Entity<VAGONS>()
                .HasMany(e => e.VAGON_OPERATIONS)
                .WithOptional(e => e.VAGONS)
                .HasForeignKey(e => e.id_vagon);

            modelBuilder.Entity<VAGON_OPERATIONS>()
                .Property(e => e.weight_gruz)
                .HasPrecision(18, 3);

            //SAP
            modelBuilder.Entity<SAPIncSupply>()
                .Property(e => e.WeightDoc)
                .HasPrecision(18, 3);

            modelBuilder.Entity<SAPIncSupply>()
                .Property(e => e.WeightReweighing)
                .HasPrecision(18, 3);
        }
    }

}
