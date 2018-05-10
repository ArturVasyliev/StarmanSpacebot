using System;
using System.Net;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using StarmanLibrary.Integrations.MAAS2;

namespace StarmanTests.Integrations.MAAS2
{
    [TestClass]
    public class Maas2Test
    {
        [TestMethod]
        public void Maas2_GetMarsStatusBySol_ReturnsValid()
        {
            MarsStatus result = Maas2.GetMarsStatusBySol(0);

            Assert.IsNotNull(result);
            Assert.IsFalse(CheckDefaultValues(result));
        }

        private bool CheckDefaultValues(MarsStatus marsStatus)
        {
            return marsStatus.HttpStatus == default(HttpStatusCode)
                && marsStatus.Sol == default(int)
                && marsStatus.Season == default(string)
                && marsStatus.MinTemp == default(int)
                && marsStatus.MaxTemp == default(int)
                && marsStatus.Opacity == default(string)
                && marsStatus.SunriseTime == default(string)
                && marsStatus.SunsetTime == default(string)
                && marsStatus.TempUnitOfMeasure == default(string);
        }
    }
}
