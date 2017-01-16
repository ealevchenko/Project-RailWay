using EFWagons.Entities;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EFWagons.Abstract
{
    public interface IDBRepository
    {
       Database db {get;}
    }
}
