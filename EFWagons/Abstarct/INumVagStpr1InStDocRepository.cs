using EFWagons.Abstract;
using EFWagons.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EFWagons.Abstarct
{
    public interface INumVagStpr1InStDocRepository: IDBRepository
    {
        IQueryable<NumVagStpr1InStDoc> NumVagStpr1InStDoc { get; }
    }
}
