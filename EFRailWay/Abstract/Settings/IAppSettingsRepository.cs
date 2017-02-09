using EFRailWay.Entities.Settings;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EFRailWay.Abstract.Settings
{
    public interface IAppSettingsRepository
    {
        IQueryable<appSettings> appSettings { get; }
        int SaveSettings(appSettings appSettings);
        appSettings DeleteSettings(string Key, int id_project);
    }
}
