using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using EFRailWay.Abstract;
using EFRailWay.References;
using System.Collections.Generic;
using System.Linq;
using EFRailWay.Entities;

namespace Test_EFRailWay
{
    [TestClass]
    public class Test_ReferenceRailway
    {
        [TestMethod]
        public void Read_Code_Station()
        {
            // Arrange - create the mock repository
            Mock<IReferenceRailwayRepository> mock_rr = new Mock<IReferenceRailwayRepository>();
            mock_rr.Setup(m => m.Code_Station).Returns(new Code_Station[] {
                new Code_Station { IDStation = 1, Station="Station1", Code=40000, CodeCS=400000, IDInternalRailroad=1 },
                new Code_Station { IDStation = 2, Station="Station2", Code=50000, CodeCS=500000, IDInternalRailroad=1 },
                new Code_Station { IDStation = 3, Station="Station3", Code=50001, CodeCS=500010, IDInternalRailroad=2 }
            }.AsQueryable());

            // Arrange - create the controller
            ReferenceRailway target = new ReferenceRailway(mock_rr.Object);
            // Act
            Code_Station cs1 = target.GetStations(1);
            Code_Station cs3 = target.GetStations(3);
            Code_Station cs4 = target.GetStations(4);
            List<Code_Station> list = target.GetStations().ToList();
            // Assert
            Assert.AreEqual(1, cs1.IDStation);
            Assert.AreEqual(3, cs3.IDStation);
            Assert.AreEqual(3, list.Count());
            Assert.AreEqual(null, cs4);
        }
    }
}
