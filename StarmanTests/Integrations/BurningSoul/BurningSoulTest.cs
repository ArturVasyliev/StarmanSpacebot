using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using StarmanLibrary.Integrations.BurningSoul;
using BurningSoulClass = StarmanLibrary.Integrations.BurningSoul.BurningSoul;

namespace StarmanTests.Integrations.BurningSoul
{
    [TestClass]
    public class BurningSoulTest
    {
        [TestMethod]
        public void BurningSoul_GetMoonStatus_ReturnsValid()
        {
            var moonStatus = BurningSoulClass.GetCurrentMoonStatus();

            Assert.IsNotNull(moonStatus);
            Assert.IsFalse(CheckDefaultValues(moonStatus));
        }

        private bool CheckDefaultValues(MoonStatus moonStatus)
        {
            return moonStatus.Age == default(double)
                && moonStatus.Illumination == default(double)
                && moonStatus.Stage == default(string)
                && moonStatus.DistanceFromCoreOfEarth == default(double)
                && moonStatus.DistanceFromSun == default(double);
        }
    }
}
