using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Newtonsoft.Json;
using StarmanLibrary.Integrations.SpaceX;
using SpaceXClass = StarmanLibrary.Integrations.SpaceX.SpaceX;

namespace StarmanTests.Integrations.SpaceX
{
    [TestClass]
    public class SpaceXTest
    {
        [TestMethod]
        public void SpaceX_GetCompanyInfo_ReturnsValid()
        {
            Company result = SpaceXClass.GetCompanyInfo();

            Assert.IsNotNull(result);
            Assert.IsFalse(CheckDefaultValues(result));
        }

        private bool CheckDefaultValues(Company company)
        {
            return company.Name == default(string)
                && company.Founder == default(string)
                && company.CreationYear == default(string)
                && company.EmployeesAmount == default(int)
                && company.CEO == default(string)
                && company.CTO == default(string)
                && company.COO == default(string)
                && company.COOPropulsion == default(string)
                && company.Valuation == default(long)
                && company.Headquarters == default(Headquarters)
                && company.Summary == default(string);
        }

        [TestMethod]
        [ExpectedException(typeof(JsonReaderException))]
        public void SpaceX_GetRocketById_ThrowsJsonReaderException()
        {
            Rocket result = SpaceXClass.GetRocketById("");
        }

        private bool CheckDefaultValues(Rocket rocket)
        {
            return rocket.Id == default(string)
                && rocket.Name == default(string)
                && rocket.Active == default(bool)
                && rocket.StagesAmount == default(int)
                && rocket.CostPerLaunch == default(int)
                && rocket.SuccessRatePercent == default(int)
                && rocket.FirstFlightDate == default(DateTime)
                && rocket.Height == default(Height)
                && rocket.Diameter == default(Diameter)
                && rocket.Mass == default(Mass)
                && rocket.Description == default(string);
        }

        [TestMethod]
        public void SpaceX_GetRockets_ReturnsValid()
        {
            Rocket[] result = SpaceXClass.GetRockets();

            Assert.IsNotNull(result);
        }

        [TestMethod]
        public void SpaceX_GetLatestLaunch_ReturnsValid()
        {
            Launch result = SpaceXClass.GetLatestLaunch();

            Assert.IsNotNull(result);
            Assert.IsFalse(CheckDefaultValues(result));
        }

        private bool CheckDefaultValues(Launch launch)
        {
            return launch.FlightNumber == default(int)
                && launch.LaunchDate == default(DateTime)
                && launch.Rocket == default(LaunchedRocket)
                && launch.VideoLink == default(string)
                && launch.ArticleLink == default(string)
                && launch.Description == default(string);
        }

        [TestMethod]
        public void SpaceX_GetAllPastLaunches_ReturnsValid()
        {
            Launch[] result = SpaceXClass.GetAllPastLaunches();

            Assert.IsNotNull(result);
        }

        [TestMethod]
        public void SpaceX_GetAllUpcomingLaunches_ReturnsValid()
        {
            Launch[] result = SpaceXClass.GetAllUpcomingLaunches();

            Assert.IsNotNull(result);
        }
    }
}
