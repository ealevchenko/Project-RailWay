using EFRailWay.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EFRailWay.Abstract
{
    public interface IAppSettingsRepository
    {
        IQueryable<appSettings> appSettings { get; }
        int SaveSettings(appSettings appSettings);
        appSettings DeleteSettings(string Key, int id_project);
    }
}
