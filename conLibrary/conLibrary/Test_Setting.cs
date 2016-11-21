using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace conLibrary
{
    class Test_Setting
    {
    }
    //**************** ТЕСТ Settings ***********************************************************
    //Settings target = new Settings();

    // Тест TypeValue
    //TypeValue new_TypeValue = new TypeValue() { IDTypeValue = 0, TypeValue1 = "TestType" };
    //TypeValue change_TypeValue = new TypeValue() { IDTypeValue = 3, TypeValue1 = "TestTypeC" };
    //int res1_new_TypeValue = target.SaveTypeValue(new_TypeValue);
    //int res2_new_TypeValue = target.SaveTypeValue(new_TypeValue);
    //int res3_new_TypeValue = target.SaveTypeValue(change_TypeValue);
    //target.InsertTypeValue();
    //{ }
    //TypeValue del_TypeValue = target.DeleteTypeValue(res1_new_TypeValue);
    //{ }
    // тест project
    //Project pr1 = new Project() { IDProject = 0, Project1 = "project1", ProjectDescription = null };
    //Project pr2 = new Project() { IDProject = 0, Project1 = "project2", ProjectDescription = "desc_pr2" };

    //int res1_new_pr = target.SaveProject(pr1);
    //int res2_new_pr = target.SaveProject(pr2);
    //Project pr1_change = new Project() { IDProject = res1_new_pr, Project1 = "project1", ProjectDescription = "desc_pr1" };
    //int res3_new_pr = target.SaveProject(pr1_change);
    //Project del_pr1 = target.DeleteProject(res1_new_pr);
    //Project del_pr2 = target.DeleteProject(res2_new_pr);
    //{ }
    // тест appsettings
    //appSettings s1 = new appSettings() { IDAppSettings = 1, Key = "key1_pr1", Value = "123", Description = "des_key1_pr1", IDProject = 7, IDTypeValue = 1 };
    //int res1_new_s = target.SaveSetting(s1);
    //appSettings s2 = new appSettings() { IDAppSettings = 1, Key = "key1_pr1", Value = "string", Description = "des_key1_pr1", IDProject = 7, IDTypeValue = 2 };
    //int res2_new_s = target.SaveSetting(s2);
    //appSettings del_s1 = target.DeleteSettings("key1_pr1",6);
    //appSettings del_s2 = target.DeleteSettings("key1_pr1", 7);
    //{ }
    // тест авто создания project и setting
    //Project pr_cr = target.Get_Project("Test", "dec_test", true);
    //if (pr_cr != null)
    //{
    //    int? port = target.GetIntSettingConfigurationManager("Port", pr_cr.IDProject, true);
    //    string PSW = target.GetStringSettingConfigurationManager("PSW", pr_cr.IDProject, true);
    //    bool? DeleteFile = target.GetBoolSettingConfigurationManager("DeleteFile", pr_cr.IDProject, true);
    //}
    //{ }
    //
    //**************** END ТЕСТ Settings***********************************************************
}
