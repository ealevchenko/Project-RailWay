using EFRailWay.Abstract;
using EFRailWay.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EFRailWay.Concrete
{
    public class EFProjectRepository : EFRepository, IProjectRepository
    {
        /// <summary>
        /// 
        /// </summary>
        public IQueryable<Project> Project
        {
            get { return context.Project; }
        }
        /// <summary>
        /// Добавить или править
        /// </summary>
        /// <param name="TypeValue"></param>
        /// <returns></returns>
        public int SaveProject(Project Project)
        {
            Project dbEntry;
            if (Project.IDProject == 0)
            {
                dbEntry = new Project() {
                     IDProject = Project.IDProject,
                     Project1 = Project.Project1,
                     ProjectDescription = Project.ProjectDescription
                };
                context_edit.Project.Add(dbEntry);
            }
            else
            {
                dbEntry = context_edit.Project.Find(Project.IDProject);
                if (dbEntry != null)
                {
                    dbEntry.Project1 = Project.Project1;
                    dbEntry.ProjectDescription = Project.ProjectDescription;
                }
            }
            try
            {
                context_edit.SaveChanges();
            }
            catch (Exception e)
            {
                return -1;
            }
            return dbEntry.IDProject;
        }
        /// <summary>
        /// Удалить
        /// </summary>
        /// <param name="IDTypeValue"></param>
        /// <returns></returns>
        public Project DeleteProject(int IDProject)
        {
            Project dbEntry = context_edit.Project.Find(IDProject);
            if (dbEntry != null)
            {
                context_edit.Project.Remove(dbEntry);
                context_edit.SaveChanges();
            }
            return dbEntry;
        }
    }
}
