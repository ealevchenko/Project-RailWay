using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using System.Linq;
using System.Collections.Generic;
using EFRailWay.Abstract;
using EFRailWay.Entities;
using EFRailWay.MT;

namespace Test_EFRailWay
{
    [TestClass]
    public class Test_MT
    {
        Mock<IMTRepository> mock_mt = new Mock<IMTRepository>();

        [TestMethod]
        public void Read_Sostav()
        {
            // Arrange - create the mock repository
            mock_mt.Setup(m => m.MTSostav).Returns(new MTSostav[] {
                new MTSostav() { 
                    IDMTSostav=1, 
                    FileName="regl_8701-058-4670_2016082212254.xml",
                    CompositionIndex="8701-058-4670",
                    DateTime=DateTime.Parse("2016-08-22 12:25:00.000"),
                    Operation = 1,
                    Create= DateTime.Parse("2016-09-07 15:07:45.000"),
                    Close= DateTime.Parse("2016-09-07 15:07:45.697"),
                    ParentID=null
                },
                new MTSostav() { 
                    IDMTSostav=2, 
                    FileName="regl_8701-058-4670_2016082212353.xml",
                    CompositionIndex="8701-058-4670",
                    DateTime=DateTime.Parse("2016-08-22 12:35:00.000"),
                    Operation = 2,
                    Create= DateTime.Parse("2016-09-07 15:07:45.000"),
                    Close= null,
                    ParentID=1
                },
                new MTSostav() { 
                    IDMTSostav=3, 
                    FileName="regl_4670-731-4672_2016082214000.xml",
                    CompositionIndex="4670-731-4672",
                    DateTime=DateTime.Parse("2016-08-22 14:00:00.000"),
                    Operation = 1,
                    Create= DateTime.Parse("2016-09-07 15:07:46.000"),
                    Close= null,
                    ParentID=null
                },
            }.AsQueryable());

            // Arrange - create the controller
            MTContent target = new MTContent(mock_mt.Object);
             
            // Act
            // Проверка
            MTSostav mts = target.Get_MTSostav(1);
            MTSostav mts_null = target.Get_MTSostav(0);
            List<MTSostav> list_ci = target.Get_MTSostav("8701-058-4670").ToList();
            List<MTSostav> list_all = target.Get_MTSostav().ToList();

            bool sostav_exist = target.IsExistSostav("regl_8701-058-4670_2016082212353.xml");
            bool sostav_not_exist = target.IsExistSostav("1regl_8701-058-4670_2016082212353.xml");

            MTSostav mts_notClose = target.Get_NoCloseMTSostav("8701-058-4670", DateTime.Parse("2016-08-23 12:35:00.000"));
            MTSostav mts_notClose_null = target.Get_NoCloseMTSostav("8701-058-4670", DateTime.Parse("2016-08-21 12:35:00.000"));
            // Assert
            // 
            Assert.AreEqual("regl_8701-058-4670_2016082212254.xml", mts.FileName);
            Assert.AreEqual("8701-058-4670", mts.CompositionIndex);
            Assert.AreEqual("2016-08-22 12:25:00.000", mts.DateTime.ToString("yyyy-MM-dd HH:mm:ss.000"));
            Assert.AreEqual(1, mts.Operation);
            Assert.AreEqual("2016-09-07 15:07:45.000", mts.Create.ToString("yyyy-MM-dd HH:mm:ss.000"));
            Assert.AreEqual("2016-09-07 15:07:45.697", ((DateTime)mts.Close).ToString("yyyy-MM-dd HH:mm:ss.697"));
            Assert.AreEqual(null, mts.ParentID);
            Assert.AreEqual(null, mts_null);

            Assert.AreEqual(2, list_ci.Count());
            Assert.AreEqual(1, list_ci[0].IDMTSostav);
            Assert.AreEqual(2, list_ci[1].IDMTSostav);
            Assert.AreEqual(3, list_all.Count());
            Assert.AreEqual(true, sostav_exist);
            Assert.AreEqual(false, sostav_not_exist);

            Assert.AreEqual(2, mts_notClose.IDMTSostav);
            Assert.AreEqual(null, mts_notClose_null);

        }

        [TestMethod]
        public void Read_List()
        {
            // Arrange - create the mock repository
            mock_mt.Setup(m => m.MTList).Returns(new MTList[] {
                new MTList() { 
                     IDMTList=1,
                     IDMTSostav=1,
                     Position=1,
                     CarriageNumber=60490943,
                     CountryCode=221,
                     Weight=57,
                     IDCargo=17101,
                     Cargo="sss",
                     IDStation=46600,
                     Station="НИКОПОЛЬ",
                     Consignee=3925,
                     Operation="ТСП",
                     CompositionIndex="3540-097-4670",
                     DateOperation=DateTime.Parse("2016-08-22 11:40:00.000"),
                     TrainNumber=1402,
                     NaturList=null
                },
                new MTList() { 
                     IDMTList=2,
                     IDMTSostav=1,
                     Position=2,
                     CarriageNumber=60490944,
                     CountryCode=221,
                     Weight=50,
                     IDCargo=17101,
                     Cargo="sss",
                     IDStation=46600,
                     Station="НИКОПОЛЬ",
                     Consignee=3925,
                     Operation="ТСП",
                     CompositionIndex="3540-097-4670",
                     DateOperation=DateTime.Parse("2016-08-22 11:40:00.000"),
                     TrainNumber=1402,
                     NaturList=null
                },   
                new MTList() { 
                     IDMTList=3,
                     IDMTSostav=2,
                     Position=1,
                     CarriageNumber=33490944,
                     CountryCode=222,
                     Weight=60,
                     IDCargo=17100,
                     Cargo="sss",
                     IDStation=46600,
                     Station="Кривой Рог",
                     Consignee=3925,
                     Operation="ПРИБ",
                     CompositionIndex="3540-055-4670",
                     DateOperation=DateTime.Parse("2016-08-22 11:40:00.000"),
                     TrainNumber=1402,
                     NaturList=null
                },
            }.AsQueryable());

											


            // Arrange - create the controller
            MTContent target = new MTContent(mock_mt.Object);
             
            // Act
            // Проверка
            MTList mtl = target.Get_MTList(1);
            MTList mtl_null = target.Get_MTList(0);
            List<MTList> list_ws = target.Get_MTListToSostav(1).ToList();
            List<MTList> list_all = target.Get_MTList().ToList();
            int? tn = target.GetTrainNumberToSostav(1);
            int? tn_null = target.GetTrainNumberToSostav(0);
            bool exlist = target.IsMTListToMTSostsv(1);
            bool exlist_null = target.IsMTListToMTSostsv(0);
            //bool sostav_exist = target.IsExistSostav("regl_8701-058-4670_2016082212353.xml");
            //bool sostav_not_exist = target.IsExistSostav("1regl_8701-058-4670_2016082212353.xml");

            //MTSostav mts_notClose = target.Get_NoCloseMTSostav("8701-058-4670", DateTime.Parse("2016-08-23 12:35:00.000"));
            //MTSostav mts_notClose_null = target.Get_NoCloseMTSostav("8701-058-4670", DateTime.Parse("2016-08-21 12:35:00.000"));
            // Assert
            // 

            Assert.AreEqual(null, mtl_null);
            Assert.AreEqual(60490943, mtl.CarriageNumber);
            Assert.AreEqual(DateTime.Parse("2016-08-22 11:40:00.000"), mtl.DateOperation);
            Assert.AreEqual(1, mtl.Position);

            Assert.AreEqual(2, list_ws.Count());
            Assert.AreEqual(1, list_ws[0].IDMTList);
            Assert.AreEqual(2, list_ws[1].IDMTList);
            Assert.AreEqual(3, list_all.Count());
            Assert.AreEqual(1402, tn);
            Assert.AreEqual(null, tn_null);
            Assert.AreEqual(true, exlist);
            Assert.AreEqual(false, exlist_null);
            //Assert.AreEqual(true, sostav_exist);
            //Assert.AreEqual(false, sostav_not_exist);

            //Assert.AreEqual(2, mts_notClose.IDMTSostav);
            //Assert.AreEqual(null, mts_notClose_null);

        }
    }
}
