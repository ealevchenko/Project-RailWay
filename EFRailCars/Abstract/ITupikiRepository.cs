using EFRailCars.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EFRailCars.Abstract
{
    public interface ITupikiRepository: IDBRepository
    {
        IQueryable<TUPIKI> TUPIKI { get; }
        int SaveTUPIKI(TUPIKI tupiki);
        TUPIKI DeleteTUPIKI(int id_tupik);
    }
}
