using EFRailCars.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EFRailCars.Abstract
{
    public interface IVagonsOperationsRepository: IDBRepository
    {
        IQueryable<VAGON_OPERATIONS> VAGON_OPERATIONS { get; }
        int SaveVAGONOPERATIONS(VAGON_OPERATIONS VAGONOPERATIONS);
        VAGON_OPERATIONS DeleteVAGONOPERATIONS(int id_oper);
    }
}
