using Xunit;

namespace KartingTrackStatistic.Tests
{
    public class DriverTests
    {
        [Fact]
        public void GetStatisticsForNewDriverReturnsStatistics()
        {
            // arrange

            var driver = new DriverSaved("John", "Smith");
            driver.AddLapTime(102.0);
            driver.AddLapTime(101.0);
            driver.AddLapTime(103);
            driver.AddLapTime(104);
            driver.AddLapTime(105.0);

            // act
            var result = driver.GetStatistics();

            // assert
            Assert.Equal(103, result.Average, 1);
            Assert.Equal(105, result.High, 1);
            Assert.Equal(101, result.Low, 1);
        }
    }
}