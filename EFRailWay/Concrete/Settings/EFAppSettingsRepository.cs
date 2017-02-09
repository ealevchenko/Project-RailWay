using EFRailWay.Abstract.Settings;
using EFRailWay.Entities.Settings;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EFRailWay.Concrete.Settings
{
    public class EFAppSettingsRepository : EFRepository, IAppSettingsRepository
    {
        public IQueryable<appSettings> appSettings
        {
            get { return context.appSettings; }
        }
        
        public int SaveSettings(appSettings appSettings)
        {
            appSettings dbEntry;
            if (context.appSettings.Where(s => s.Key == appSettings.Key && s.IDProject == appSettings.IDProject).FirstOrDefault() == null)
            {
                dbEntry = new appSettings()
                {
                    IDAppSettings = appSettings.IDAppSettings,
                    Key = appSettings.Key,
                    Value = appSettings.Value,
                    Description = appSettings.Description,
                    IDProject = appSettings.IDProject,
                    IDTypeValue = appSettings.IDTypeValue
                };
                context_edit.appSettings.Add(dbEntry);
            }
            else 
            {
                int id = context.appSettings.Where(s => s.Key == appSettings.Key && s.IDProject == appSettings.IDProject).FirstOrDefault().IDAppSettings;
                dbEntry = context_edit.appSettings.Find(id);
                if (dbEntry != null)
                {
                    dbEntry.Key = appSettings.Key;
                    dbEntry.Value = appSettings.Value;
                    dbEntry.Description = appSettings.Description;
                    dbEntry.IDTypeValue = appSettings.IDTypeValue;
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
            return dbEntry.IDAppSettings;
        }

        public appSettings DeleteSettings(string Key, int id_project)
        {
            appSettings setting = context.appSettings.Where(s => s.Key == Key && s.IDProject == id_project).FirstOrDefault();
            if (setting == null) return null;
            appSettings dbEntry = context_edit.appSettings.Find(setting.IDAppSettings);
            if (dbEntry != null)
            {
                context_edit.appSettings.Remove(dbEntry);
                context_edit.SaveChanges();
            }
            return dbEntry;
        }
    }
}
