using EFRailWay.Entities;
using EFRailWay.Entities.Railcars;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EFRailWay.Abstract.Railcars
{
    public interface IVagonsOperationsRepository: IDBRepository
    {
        IQueryable<VAGON_OPERATIONS> VAGON_OPERATIONS { get; }
        int SaveVAGONOPERATIONS(VAGON_OPERATIONS VAGONOPERATIONS);
        VAGON_OPERATIONS DeleteVAGONOPERATIONS(int id_oper);
    }
}
