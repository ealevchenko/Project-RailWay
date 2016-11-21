using EFRailWay.Abstract;
using EFRailWay.Concrete;
using EFRailWay.Entities;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EFRailWay.Settings
{
    public enum SettingsType : int { INT = 1, STRING = 2, BOOLEAN = 3}

    public class SettingValue 
    {
        public string Key { get; set; }
        public SettingsType TypeValue { get; set; }
    }

    public class Settings
    {
        private ITypeValueRepository rep_TypeValue;
        private IAppSettingsRepository rep_AppSettings;
        private IProjectRepository rep_Project;

        #region Конструкторы
        public Settings()
        {
            this.rep_TypeValue = new EFTypeValueRepository();
            this.rep_AppSettings = new EFAppSettingsRepository();
            this.rep_Project = new EFProjectRepository();
        }

        public Settings(IAppSettingsRepository rep_AppSettings, IProjectRepository rep_Project,ITypeValueRepository rep_TypeValue)
        {
            this.rep_TypeValue = rep_TypeValue;
            this.rep_AppSettings = rep_AppSettings;
            this.rep_Project = rep_Project;
        }
        #endregion

        #region TypeValue
        /// <summary>
        /// Получить тип TypeValue
        /// </summary>
        /// <param name="type_value"></param>
        /// <returns></returns>
        public TypeValue Get_TypeValue(int type_value)
        {
            return rep_TypeValue.TypeValue.Where(s => s.IDTypeValue == type_value).SingleOrDefault();
        }
        /// <summary>
        /// Список типов
        /// </summary>
        /// <returns></returns>
        public IQueryable<TypeValue> Get_TypeValue()
        {
            return rep_TypeValue.TypeValue.OrderBy(t=>t.IDTypeValue);
        }
        /// <summary>
        /// Вернуть максимальный ID
        /// </summary>
        /// <returns></returns>
        public int GetMaxIDTypeValue() 
        {
            return rep_TypeValue.TypeValue.Max(t => t.IDTypeValue);
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="typevalue"></param>
        /// <returns></returns>
        public int? GetTypeValue(string typevalue) 
        {
            TypeValue tv = rep_TypeValue.TypeValue.Where(t => t.TypeValue1.ToUpper() == typevalue.ToUpper()).SingleOrDefault();
            if (tv!=null) {return tv.IDTypeValue; }
            return null;
        }
        /// <summary>
        /// Получить тип переменной
        /// </summary>
        /// <param name="type_value"></param>
        /// <returns></returns>
        public Type GetTypeValue(int type_value) 
        {
            TypeValue type = Get_TypeValue(type_value);
            if (type == null) return null;
            switch (type.TypeValue1.ToUpper())
            {
                case "INT": return typeof(Int32);
                case "STRING": return typeof(string);
                case "BOOLEAN": return typeof(Boolean);
                default: return null;
            }
        }
        /// <summary>
        /// Сохранить тип 
        /// </summary>
        /// <param name="tv"></param>
        /// <returns></returns>
        public int SaveTypeValue(TypeValue tv)
        {
            return rep_TypeValue.SaveTypeValue(tv);
        }
        /// <summary>
        /// Удалить TypeValue
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public TypeValue DeleteTypeValue(int id)
        {
            return rep_TypeValue.DeleteTypeValue(id);
        }
        /// <summary>
        /// Занести в базу TypeValues
        /// </summary>
        public void InsertTypeValue() 
        {
            foreach (SettingsType sv in Enum.GetValues(typeof(SettingsType))) 
            {
                SaveTypeValue(new TypeValue() { IDTypeValue = (int)sv, TypeValue1 = sv.ToString() });
            }
        }
        #endregion

        #region Project
        /// <summary>
        /// Получить тип Project по id
        /// </summary>
        /// <param name="type_value"></param>
        /// <returns></returns>
        public Project Get_Project(int id_project)
        {
            return rep_Project.Project.Where(p => p.IDProject == id_project).SingleOrDefault();
        }
        /// <summary>
        /// Получить тип Project по названию проекта
        /// </summary>
        /// <param name="project"></param>
        /// <returns></returns>
        public Project Get_Project(string project)
        {
            return rep_Project.Project.Where(p => p.Project1 == project).SingleOrDefault();
        }
        /// <summary>
        /// Получить тип Project по названию проекта если проекта нет и указан create создать проект в бд
        /// </summary>
        /// <param name="project"></param>
        /// <param name="description"></param>
        /// <param name="create"></param>
        /// <returns></returns>
        public Project Get_Project(string project, string description, bool create)
        {
            Project prj = Get_Project(project);
            if (prj == null & create) 
            {
                prj = new Project() { IDProject = 0, Project1 = project, ProjectDescription = description };
                int result = SaveProject(prj);
                if (result <= 0) { return null; }
                prj.IDProject = result;
            }
            return prj;
        }

        /// <summary>
        /// Список Projects
        /// </summary>
        /// <returns></returns>
        public IQueryable<Project> Get_Project()
        {
            return rep_Project.Project.OrderBy(p => p.IDProject);
        }
        /// <summary>
        /// Вернуть максимальный ID
        /// </summary>
        /// <returns></returns>
        public int GetMaxIDProject()
        {
            return rep_Project.Project.Max(p => p.IDProject);
        }
        /// <summary>
        /// Сохранить Project 
        /// </summary>
        /// <param name="tv"></param>
        /// <returns></returns>
        public int SaveProject(Project project)
        {
            return rep_Project.SaveProject(project);
        }
        /// <summary>
        /// Удалить Project
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public Project DeleteProject(int id)
        {
            return rep_Project.DeleteProject(id);
        }
        #endregion

        #region appSettings
        /// <summary>
        /// Вернуть запрашиваемый ключ
        /// </summary>
        /// <param name="Key"></param>
        /// <returns></returns>
        public appSettings Get_Setting(string Key, int id_project)
        {
            return rep_AppSettings.appSettings.Where(s => s.Key == Key & s.IDProject==id_project).SingleOrDefault();
        }
        /// <summary>
        /// Список appSettings
        /// </summary>
        /// <returns></returns>
        public IQueryable<appSettings> Get_Setting()
        {
            return rep_AppSettings.appSettings.OrderBy(a=>a.Key);
        }
        /// <summary>
        /// вернуть переменную типа int
        /// </summary>
        /// <param name="Key"></param>
        /// <returns></returns>
        public int? GetIntSetting(string Key, int id_project) 
        { 
            appSettings apps = Get_Setting(Key,id_project);
            if (apps == null) return null;
            if (GetTypeValue(apps.IDTypeValue) == typeof(Int32)) 
            {
                return int.Parse(apps.Value);
            }
            return null;
        }
        /// <summary>
        /// вернуть переменную типа string
        /// </summary>
        /// <param name="Key"></param>
        /// <returns></returns>
        public string GetStringSetting(string Key, int id_project) 
        { 
            appSettings apps = Get_Setting(Key, id_project);
            if (apps == null) return null;
            if (GetTypeValue(apps.IDTypeValue) == typeof(String)) 
            {
                return apps.Value;
            }
            return null;
        }
        /// <summary>
        /// вернуть переменную типа bool
        /// </summary>
        /// <param name="Key"></param>
        /// <param name="id_project"></param>
        /// <returns></returns>
        public bool? GetBoolSetting(string Key, int id_project) 
        { 
            appSettings apps = Get_Setting(Key,id_project);
            if (apps == null) return null;
            if (GetTypeValue(apps.IDTypeValue) == typeof(Boolean)) 
            {
                return Boolean.Parse(apps.Value);
            }
            return null;
        }
        /// <summary>
        /// Получить значение Setting(int) по указаному Key. Если в базе нет значения считать из файлв app.config и если указан create создать Setting в базе данных
        /// </summary>
        /// <param name="Key"></param>
        /// <param name="create"></param>
        /// <param name="id_project"></param>
        /// <returns></returns>
        public int? GetIntSettingConfigurationManager(string Key, int id_project, bool create)
        {
            int? result = GetIntSetting(Key, id_project);
            if (result == null)
            {
                result = GetIntConfigurationManager(Key);
                if (result != null & create)
                {
                    appSettings setting = new appSettings()
                    {
                        IDAppSettings = 0,
                        Key = Key,
                        Value = result.ToString(),
                        Description = "",
                        IDProject = id_project,
                        IDTypeValue = (int)GetTypeValue(SettingsType.INT.ToString())
                    };
                    SaveSetting(setting);
                }
            }
            return result;

        }
        /// <summary>
        /// Получить значение Setting(string) по указаному Key. Если в базе нет значения считать из файлв app.config и если указан create создать Setting в базе данных
        /// </summary>
        /// <param name="Key"></param>
        /// <param name="id_project"></param>
        /// <param name="create"></param>
        /// <returns></returns>
        public string GetStringSettingConfigurationManager(string Key, int id_project, bool create)
        {
            string result = GetStringSetting(Key, id_project);
            if (String.IsNullOrWhiteSpace(result))
            {
                result = GetStringConfigurationManager(Key);
                if (result != null & create)
                {
                    appSettings setting = new appSettings()
                    {
                        IDAppSettings = 0,
                        Key = Key,
                        Value = result,
                        Description = "",
                        IDProject = id_project,
                        IDTypeValue = (int)GetTypeValue(SettingsType.STRING.ToString())
                    };
                    SaveSetting(setting);
                }
            }
            return result;

        }
        /// <summary>
        /// Получить значение Setting(bool) по указаному Key. Если в базе нет значения считать из файлв app.config и если указан create создать Setting в базе данных
        /// </summary>
        /// <param name="Key"></param>
        /// <param name="id_project"></param>
        /// <param name="create"></param>
        /// <returns></returns>
        public bool? GetBoolSettingConfigurationManager(string Key, int id_project, bool create)
        {
            bool? result = GetBoolSetting(Key, id_project);
            if (result==null)
            {
                result = GetBoolConfigurationManager(Key);
                if (result != null & create)
                {
                    appSettings setting = new appSettings()
                    {
                        IDAppSettings = 0,
                        Key = Key,
                        Value = result.ToString(),
                        Description = "",
                        IDProject = id_project,
                        IDTypeValue = (int)GetTypeValue(SettingsType.BOOLEAN.ToString())
                    };
                    SaveSetting(setting);
                }
            }
            return result;

        }
        /// <summary>
        /// Получить значение Setting(int) по указаному Key. Если в базе нет значения считать из файлв app.config и если указан create создать Setting в базе данных
        /// </summary>
        /// <param name="Key"></param>
        /// <param name="project"></param>
        /// <param name="create"></param>
        /// <returns></returns>
        public int? GetIntSettingConfigurationManager(string Key, string project, bool create) 
        { 
            Project prj = Get_Project(project);
            if (prj!=null){
                return GetIntSettingConfigurationManager(Key, prj.IDProject, create);
            }
            return null;
        }
        /// <summary>
        /// Получить значение Setting(string) по указаному Key. Если в базе нет значения считать из файлв app.config и если указан create создать Setting в базе данных
        /// </summary>
        /// <param name="Key"></param>
        /// <param name="project"></param>
        /// <param name="create"></param>
        /// <returns></returns>
        public string GetStringSettingConfigurationManager(string Key, string project, bool create) 
        { 
            Project prj = Get_Project(project);
            if (prj!=null){
                return GetStringSettingConfigurationManager(Key, prj.IDProject, create);
            }
            return null;        
        }
        /// <summary>
        /// Получить значение Setting(bool) по указаному Key. Если в базе нет значения считать из файлв app.config и если указан create создать Setting в базе данных
        /// </summary>
        /// <param name="Key"></param>
        /// <param name="project"></param>
        /// <param name="create"></param>
        /// <returns></returns>
        public bool? GetBoolSettingConfigurationManager(string Key, string project, bool create) 
        { 
            Project prj = Get_Project(project);
            if (prj!=null){
                return GetBoolSettingConfigurationManager(Key, prj.IDProject, create);
            }
            return null;        
        }
        /// <summary>
        /// Сохранить Project 
        /// </summary>
        /// <param name="tv"></param>
        /// <returns></returns>
        public int SaveSetting(appSettings setting)
        {
            return rep_AppSettings.SaveSettings(setting);
        }
        /// <summary>
        /// Удалить Project
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public appSettings DeleteSettings(string key, int id_project)
        {
            return rep_AppSettings.DeleteSettings(key, id_project);
        }
        #endregion

        #region ConfigurationManager
        /// <summary>
        /// Вернуть string
        /// </summary>
        /// <param name="key"></param>
        /// <returns></returns>
        public string GetStringConfigurationManager(string key) 
        {
            try
            {
                return ConfigurationManager.AppSettings[key].ToString();
            }
            catch (Exception e) 
            {
                return null;
            }
        }
        /// <summary>
        /// Вернуть int
        /// </summary>
        /// <param name="key"></param>
        /// <returns></returns>
        public int? GetIntConfigurationManager(string key) 
        {
            try
            {
                string result = GetStringConfigurationManager(key);
                if (result != null)
                {
                    return int.Parse(result);
                }
            } 
            catch (Exception e) 
            {
                return null;
            }
            return null;
        }
        /// <summary>
        /// Вернуть bool
        /// </summary>
        /// <param name="key"></param>
        /// <returns></returns>
        public bool? GetBoolConfigurationManager(string key) 
        {
            try
            {
                string result = GetStringConfigurationManager(key);
                if (result != null)
                {
                    return bool.Parse(result);
                }
            } 
            catch (Exception e) 
            {
                return null;
            }
            return null;
        }
        #endregion
    }
}
