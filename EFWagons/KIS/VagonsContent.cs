using EFWagons.Abstarct;
using EFWagons.Concrete;
using EFWagons.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EFWagons.KIS
{
    public class VagonsContent
    {
        INumVagStanRepository rep_nvs;
        INumVagStpr1GrRepository rep_nvstpr;

        public VagonsContent() 
        {
            this.rep_nvs = new EFNumVagStanRepository();
            this.rep_nvstpr = new EFNumVagStpr1GrRepository();

        }

        public VagonsContent(INumVagStanRepository rep_nvs, INumVagStpr1GrRepository rep_nvstpr) 
        {
            this.rep_nvs = rep_nvs;
            this.rep_nvstpr = rep_nvstpr;
        }

        /// <summary>
        /// Получить все станции КИС
        /// </summary>
        /// <returns></returns>
        public IQueryable<NumVagStan> GetStations() 
        {
            return rep_nvs.NumVagStan;
        }
        /// <summary>
        /// Получить информацию по станции по  id
        /// </summary>
        /// <param name="id_stan"></param>
        /// <returns></returns>
        public NumVagStan GetStations(int id_stan) 
        {
            return GetStations().Where(s => s.K_STAN == id_stan).FirstOrDefault();
        }
        /// <summary>
        /// список грузов
        /// </summary>
        /// <returns></returns>
        public IQueryable<NumVagStpr1Gr> GetSTPR1GR() 
        {
            return rep_nvstpr.NumVagStpr1Gr;
        }
        /// <summary>
        /// Получить груз по kod_gr
        /// </summary>
        /// <param name="kod_gr"></param>
        /// <returns></returns>
        public NumVagStpr1Gr GetSTPR1GR(int kod_gr) 
        {
            return GetSTPR1GR().Where(g=>g.KOD_GR==kod_gr).FirstOrDefault();
        }
    }
}
