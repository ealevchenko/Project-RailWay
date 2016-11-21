using EFRailWay.Entities;
using EFRailWay.Entities.KIS;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EFRailWay.Abstract.KIS
{
    public interface IOracleArrivalSostavRepository 
    {
        IQueryable<Oracle_ArrivalSostav> Oracle_ArrivalSostav { get; }
        int SaveOracle_ArrivalSostav(Oracle_ArrivalSostav Oracle_ArrivalSostav);
        Oracle_ArrivalSostav DeleteOracle_ArrivalSostav(int IDOrcSostav);
    }
}
