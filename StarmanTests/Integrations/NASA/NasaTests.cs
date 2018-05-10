using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using StarmanLibrary.Integrations.NASA;

namespace StarmanTests.Integrations.NASA
{
    [TestClass]
    public class NasaTests
    {
        [TestMethod]
        public void Nasa_GetAstroPicOfTheDay_ReturnsValid()
        {
            AstroPicOfTheDay result = Nasa.GetAstroPicOfTheDay(new DateTime(2018, 05, 10));

            Assert.IsNotNull(result);
            Assert.IsFalse(CheckDefaultValues(result));
        }

        private bool CheckDefaultValues(AstroPicOfTheDay astroPicOfTheDay)
        {
            return astroPicOfTheDay.Title == default(string)
                && astroPicOfTheDay.Url == default(string)
                && astroPicOfTheDay.Description == default(string);
        }

        [TestMethod]
        public void Nasa_GetTheLatestPicOfTheEarth_ReturnsValid()
        {
            EarthPicture result = Nasa.GetTheLatestPicOfTheEarth();

            Assert.IsNotNull(result);
            Assert.IsNotNull(result.ImageUrl);
            Assert.AreNotEqual("", result.ImageUrl);
            Assert.IsFalse(CheckDefaultValues(result));
        }

        private bool CheckDefaultValues(EarthPicture earthPicture)
        {
            return earthPicture.ImageTitle == default(string)
                && earthPicture.Location == default(GeoLocation)
                && earthPicture.Date == default(DateTime)
                && earthPicture.Description == default(string);
        }
    }
}
