using EFRailWay.Abstract;
using EFRailWay.Entities;
using EFRailWay.Entities.KIS;
using EFRailWay.Entities.MT;
using EFRailWay.Entities.Reference;
using EFRailWay.Entities.SAP;
using EFRailWay.Entities.Settings;
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
        public virtual DbSet<Oracle_OutputSostav> Oracle_OutputSostav { get; set; }

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
