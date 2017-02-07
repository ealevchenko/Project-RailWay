using EFRailWay.Entities;
using EFRailWay.Entities.KIS;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EFRailWay.Abstract.KIS
{
    public interface IOracleOutputSostavRepository 
    {
        IQueryable<Oracle_OutputSostav> Oracle_OutputSostav { get; }
        int SaveOracle_OutputSostav(Oracle_OutputSostav Oracle_OutputSostav);
        Oracle_OutputSostav DeleteOracle_OutputSostav(int ID);
    }
}
