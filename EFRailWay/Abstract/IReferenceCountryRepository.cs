using EFRailWay.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EFRailWay.Abstract
{
    public interface IReferenceCountryRepository
    {
        IQueryable<ReferenceCountry> ReferenceCountry { get; }
        int SaveReferenceCountry(ReferenceCountry ReferenceCountry);
        ReferenceCountry DeleteReferenceCountry(int IDCountry);
    }
}
