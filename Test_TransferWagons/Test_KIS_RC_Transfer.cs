using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using EFRailWay.Entities.KIS;

namespace Test_TransferWagons
{
    [TestClass]
    public class Test_KIS_RC_Transfer
    {
        [TestMethod]
        public void SetListWagon()
        {
            Mock<Oracle_ArrivalSostav> mock_oas = new Mock<Oracle_ArrivalSostav>();
            mock_oas.Setup(m => m).Returns(new Oracle_ArrivalSostav()
            {
                IDOrcSostav = 542,
                DateTime = DateTime.Now,
                NaturNum = 3827,
                IDOrcStation = 1,
                WayNum = 7,
                Napr = 1,
                CountWagons = null,
                CountNatHIist = null,
                CountSetWagons = null,
                CountSetNatHIist = null,
                Close = null,
                Status = 0,
                ListWagons = null,
                ListNoSetWagons = null,
                ListNoUpdateWagons = null,
            });
        }
    }
}
