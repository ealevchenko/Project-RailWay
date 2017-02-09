using EFRailWay.Entities.Reference;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EFRailWay.Abstract.Reference
{
    public interface ICodeCountryRepository
    {
        IQueryable<Code_Country> Code_Country { get; }
        int SaveCode_Country(Code_Country Code_Country);
        Code_Country DeleteCode_Country(int ID);
    }
            

}
