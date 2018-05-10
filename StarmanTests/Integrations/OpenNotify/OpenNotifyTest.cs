using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using StarmanLibrary.Integrations.OpenNotify;
using OpenNotifyClass = StarmanLibrary.Integrations.OpenNotify.OpenNotify;

namespace StarmanTests.Integrations.OpenNotify
{
    [TestClass]
    public class OpenNotifyTest
    {
        [TestMethod]
        public void OpenNotify_GetHumansInSpace_ReturnsValid()
        {
            HumansInSpace result = OpenNotifyClass.GetHumansInSpace();

            Assert.IsNotNull(result);
            Assert.IsFalse(CheckDefaultValues(result));
        }

        private bool CheckDefaultValues(HumansInSpace humansInSpace)
        {
            return humansInSpace.People == default(HumanData[])
                && humansInSpace.Number == default(int)
                && humansInSpace.Message == default(string);
        }

        [TestMethod]
        public void OpenNotify_GetIssData_ReturnsValid()
        {
            ISSData result = OpenNotifyClass.GetIssData();

            Assert.IsNotNull(result);
            Assert.IsFalse(CheckDefaultValues(result));
        }

        private bool CheckDefaultValues(ISSData issData)
        {
            return issData.Position == default(ISSPosition)
                && issData.TimeStamp == default(int)
                && issData.Message == default(string);
        }
    }
}
