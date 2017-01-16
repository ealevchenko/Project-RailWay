using EFRailWay.Entities;
using EFRailWay.Entities.KIS;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EFRailWay.Abstract.KIS
{
    public interface IOracleInputSostavRepository 
    {
        IQueryable<Oracle_InputSostav> Oracle_InputSostav { get; }
        int SaveOracle_InputSostav(Oracle_InputSostav Oracle_InputSostav);
        Oracle_InputSostav DeleteOracle_InputSostav(int ID);
    }
}
