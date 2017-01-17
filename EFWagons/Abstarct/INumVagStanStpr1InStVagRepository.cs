using EFWagons.Abstract;
using EFWagons.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EFWagons.Abstarct
{
    public interface INumVagStanStpr1InStVagRepository: IDBRepository
    {
        IQueryable<NumVagStanStpr1InStVag> NumVagStanStpr1InStVag { get; }
    }
}
