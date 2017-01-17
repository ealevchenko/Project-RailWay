
using EFWagons.Entities;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EFWagons.Concrete
{
    public class EFDbORCContext : DbContext
    {
        public EFDbORCContext()
            : base("name=OracleDbContext")
        {
        }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            //
            modelBuilder.HasDefaultSchema("KOMETA");
            //modelBuilder.Entity<Strana>()
            //                    .Property(e => e.KOD_STRAN)
            //                    .HasPrecision(4, 0);

            //modelBuilder.Entity<Strana>()
            //    .Property(e => e.ABREV_STRAN)
            //    .IsFixedLength();

            //modelBuilder.Entity<Strana>()
            //    .Property(e => e.NAME)
            //    .IsFixedLength();
        }

        public DbSet<KometaStrana> KometaStrana { get; set; }
        // Информация по станции Промышленная
        public DbSet<PromSostav> PromSostav { get; set; }
        public DbSet<PromNatHist> PromNatHist { get; set;}
        public DbSet<PromVagon> PromVagon { get; set; }
        public DbSet<PromGruzSP> PromGruzSP { get; set; } // грузы на приходе
        public DbSet<PromCex> PromCex { get; set; } // перечень цехов

        // Информация по вагонам
        public DbSet<NumVagStan> NumVagStan { get; set; }
        public DbSet<NumVagStpr1Gr> NumVagStpr1Gr { get; set; } // груз на станции прибытия?
        public DbSet<KometaVagonSob> KometaVagonSob { get; set; } // аренда вагонов
        public DbSet<KometaSobstvForNakl> KometaSobstvForNakl { get; set; } // собственник по накладным       
        public DbSet<NumVagStanStpr1InStDoc> NumVagStanStpr1InStDoc { get; set; } // Составы по внутреним станциям по прибытию  
        public DbSet<NumVagStanStpr1InStVag> NumVagStanStpr1InStVag { get; set; } // Вагоны по внутреним станциям по прибытию 
    }
}
