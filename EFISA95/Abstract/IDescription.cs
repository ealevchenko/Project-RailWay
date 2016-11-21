using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EFISA95.Abstract
{
    public interface IDescription
    {
        int ID { get; set; }
        string Description { get; set; }
        int? ParentID { get; set; }
    }
}
