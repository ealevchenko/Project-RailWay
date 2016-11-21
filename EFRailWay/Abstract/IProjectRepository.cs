using EFRailWay.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EFRailWay.Abstract
{
    public interface IProjectRepository 
    {
        IQueryable<Project> Project { get; }
        int SaveProject(Project Project);
        Project DeleteProject(int IDProject);
    }
}
