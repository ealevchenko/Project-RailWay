using EFISA95.Entities;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EFISA95.Concrete
{
    public class EFDbContext : DbContext
    {
        public EFDbContext() : base("name=ISA95")
        {
        }

        public virtual DbSet<Equipment> Equipment { get; set; }
        public virtual DbSet<EquipmentCapabilityTestSpecification> EquipmentCapabilityTestSpecification { get; set; }
        public virtual DbSet<EquipmentClass> EquipmentClass { get; set; }
        public virtual DbSet<EquipmentClassProperty> EquipmentClassProperty { get; set; }
        public virtual DbSet<EquipmentProperty> EquipmentProperty { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Equipment>()
                .HasMany(e => e.Equipment11)
                .WithOptional(e => e.Equipment2)
                .HasForeignKey(e => e.Equipment1);

            modelBuilder.Entity<Equipment>()
                .HasMany(e => e.EquipmentProperty)
                .WithOptional(e => e.Equipment)
                .WillCascadeOnDelete();

            modelBuilder.Entity<EquipmentCapabilityTestSpecification>()
                .HasMany(e => e.EquipmentClassProperty)
                .WithOptional(e => e.EquipmentCapabilityTestSpecification1)
                .HasForeignKey(e => e.EquipmentCapabilityTestSpecification);

            modelBuilder.Entity<EquipmentCapabilityTestSpecification>()
                .HasMany(e => e.EquipmentProperty)
                .WithOptional(e => e.EquipmentCapabilityTestSpecification1)
                .HasForeignKey(e => e.EquipmentCapabilityTestSpecification);

            modelBuilder.Entity<EquipmentClass>()
                .HasMany(e => e.EquipmentClass1)
                .WithOptional(e => e.EquipmentClass2)
                .HasForeignKey(e => e.ParentID);

            modelBuilder.Entity<EquipmentClassProperty>()
                .HasMany(e => e.EquipmentClassProperty11)
                .WithOptional(e => e.EquipmentClassProperty2)
                .HasForeignKey(e => e.EquipmentClassProperty1);

            modelBuilder.Entity<EquipmentClassProperty>()
                .HasMany(e => e.EquipmentProperty)
                .WithOptional(e => e.EquipmentClassProperty)
                .HasForeignKey(e => e.ClassPropertyID);

            modelBuilder.Entity<EquipmentProperty>()
                .HasMany(e => e.EquipmentProperty11)
                .WithOptional(e => e.EquipmentProperty2)
                .HasForeignKey(e => e.EquipmentProperty1);
        }
    }
}
