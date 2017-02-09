using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using EFRailWay.Abstract.Settings;
using Moq;
using System.Collections.Generic;
using System.Linq;
using EFRailWay.Entities.Settings;
using EFRailWay.Concrete.Settings;
using EFRailWay.Settings;

namespace Test_EFRailWay
{
    [TestClass]
    public class Test_Settings
    {
        Mock<IAppSettingsRepository> mock_apps = new Mock<IAppSettingsRepository>();
        Mock<ITypeValueRepository> mock_tv = new Mock<ITypeValueRepository>();
        Mock<IProjectRepository> mock_pr = new Mock<IProjectRepository>();

        [TestMethod]
        public void Read_Setting()
        {
            // Arrange - create the mock repository
            mock_tv.Setup(m => m.TypeValue).Returns(new TypeValue[] {
                new TypeValue { IDTypeValue=1, TypeValue1="INT"},
                new TypeValue { IDTypeValue=2, TypeValue1="STRING"},  
                new TypeValue { IDTypeValue=3, TypeValue1="BOOLEAN"}            
            }.AsQueryable());
            mock_apps.Setup(m => m.appSettings).Returns(new appSettings[] {
                new appSettings { Key="Seting1", Value="12", Description="DescriptionSeting1", IDProject=1, IDTypeValue=1 },
                new appSettings { Key="Seting2", Value="Value_Seting2", Description="DescriptionValue_Seting2", IDProject=2, IDTypeValue=2 },
                new appSettings { Key="Seting3", Value="458", Description="DescriptionValue_Seting3", IDProject=1,  IDTypeValue=1 },

            }.AsQueryable());
            mock_pr.Setup(m => m.Project).Returns(new Project[] {
                new Project { IDProject=1, Project1="Project#1", ProjectDescription="Description_Project1"},
                new Project { IDProject=2, Project1="Project#2", ProjectDescription="Description_Project2"},  
       
            }.AsQueryable());
            // Arrange - create the controller
            Settings target = new Settings(mock_apps.Object, mock_pr.Object ,mock_tv.Object);
            // Act
            // Проверка appSettings
            appSettings apps = target.Get_Setting("Seting1",1);
            int? cs1 = target.GetIntSetting("Seting1",1);
            string cs2 = target.GetStringSetting("Seting2",2);
            appSettings apps_null = target.Get_Setting("Seting1",2);
            
            // Проверка TypeValue
            Type type_int = target.GetTypeValue(1);
            Type type_string = target.GetTypeValue(2);
            Type type_Boolean = target.GetTypeValue(3);
            Type type_null = target.GetTypeValue(0);

            int max_id_TypeValue = target.GetMaxIDTypeValue();
            int? id_int = target.GetTypeValue("INT");
            int? id_string = target.GetTypeValue("STRING");
            int? id_boolean = target.GetTypeValue("BOOLEAN");
            int? id_null = target.GetTypeValue("Null"); 
            int? id_int_up = target.GetTypeValue("int");          
            // Проверка Project
            Project pr = target.Get_Project(1);
            Project pr_null = target.Get_Project(0);
            // списки
            List<TypeValue> list_TypeValue = target.Get_TypeValue().ToList();
            List<appSettings> list_Settings = target.Get_Setting().ToList();



            // Assert
            // appSettings
            Assert.AreEqual("12", apps.Value);
            Assert.AreEqual(1, apps.IDTypeValue);
            Assert.AreEqual("DescriptionSeting1", apps.Description);
            Assert.AreEqual(1, apps.IDProject);
            Assert.AreEqual(1, apps.IDTypeValue);
            Assert.AreEqual(null, apps_null);
            // TypeValue
            Assert.AreEqual(typeof(Int32), type_int);
            Assert.AreEqual(typeof(String), type_string);
            Assert.AreEqual(typeof(Boolean), type_Boolean);
            Assert.AreEqual(12, cs1);
            Assert.AreEqual("Value_Seting2", cs2);
            Assert.AreEqual(null, type_null);
            Assert.AreEqual(3, max_id_TypeValue);
            Assert.AreEqual(1, id_int);
            Assert.AreEqual(2, id_string);
            Assert.AreEqual(3, id_boolean);
            Assert.AreEqual(null, id_null);
            Assert.AreEqual(1, id_int_up);
            // Project
            Assert.AreEqual(1, pr.IDProject);
            Assert.AreEqual("Project#1", pr.Project1);
            Assert.AreEqual("Description_Project1", pr.ProjectDescription);
            Assert.AreEqual(null, pr_null);
            // списки
            Assert.AreEqual(3, list_TypeValue.Count());
            Assert.AreEqual(3, list_Settings.Count());


        }

        //[TestMethod]
        //public void Save_Setting()
        //{
        //    // Arrange - create the mock repository

        //    mock_tv.Setup(m => m.TypeValue).Returns(new TypeValue[] {
        //        new TypeValue { IDTypeValue=1, TypeValue1="INT"},
        //        new TypeValue { IDTypeValue=2, TypeValue1="STRING"}            
        //    }.AsQueryable());
        //    mock_apps.Setup(m => m.appSettings).Returns(new appSettings[] {
        //        new appSettings { Key="Seting1", Value="12", IDTypeValue=1 },
        //        new appSettings { Key="Seting2", Value="Value_Seting2", IDTypeValue=2 },
        //        new appSettings { Key="Seting3", Value="458", IDTypeValue=1 },

        //    }.AsQueryable());

        //    // Arrange - create the controller
        //    Settings target = new Settings(mock_tv.Object, mock_apps.Object);
        //    // Act
        //    List<TypeValue> list_TypeValue = target.Get_TypeValue().ToList();
        //    List<appSettings> list_Settings = target.Get_Setting().ToList();

        //    TypeValue new_TypeValue = new TypeValue() { IDTypeValue = 0, TypeValue1 = "Text" };
        //    int res_new_TypeValue = target.SaveTypeValue(new_TypeValue);
        //    mock_tv.Verify(m => m.SaveTypeValue(new_TypeValue));
        //    Assert.AreEqual(2, list_TypeValue.Count());
        //    Assert.AreEqual(3, list_Settings.Count());
        //}

    }
}
