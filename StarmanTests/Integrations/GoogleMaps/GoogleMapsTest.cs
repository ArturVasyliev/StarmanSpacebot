using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using StarmanLibrary.Integrations.GoogleMaps;
using GoogleMapsClass = StarmanLibrary.Integrations.GoogleMaps.GoogleMaps;

namespace StarmanTests.Integrations.GoogleMaps
{
    [TestClass]
    public class GoogleMapsTest
    {
        [TestMethod]
        public void GoogleMaps_GetAddressByLocation_ReturnsValid()
        {
            double latitude = 2;
            double longitude = 3;
            LocationResult result = GoogleMapsClass.GetAddressByLocation(latitude, longitude);

            Assert.IsNotNull(result);
            Assert.AreEqual(latitude, result.Latitude);
            Assert.AreEqual(longitude, result.Longitude);
            Assert.IsFalse(CheckDefaultValues(result));
        }

        private bool CheckDefaultValues(LocationResult locationResult)
        {
            return locationResult.Addresses == default(Address[])
                && locationResult.Status == default(string);
        }
    }
}
