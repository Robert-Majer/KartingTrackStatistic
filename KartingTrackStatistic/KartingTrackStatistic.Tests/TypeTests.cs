using Xunit;

namespace KartingTrackStatistic.Tests
{
    public class TypeTests
    {
        [Fact]
        public void GetStudentReturnsDifferentObjects()
        {
            var driver1 = GetStudent("Michael", "Jordan");
            var driver2 = GetStudent("Mike", "Tyson");

            Assert.NotSame(driver1, driver2);
            Assert.False(driver1.Equals(driver2));
            Assert.False(Object.ReferenceEquals(driver1, driver2));
        }

        [Fact]
        public void TwoVarsCanReferenceSameObject()
        {
            var driver1 = GetStudent("Clint", "Eastwood");
            var driver2 = driver1;

            Assert.Same(driver1, driver2);
            Assert.True(driver1.Equals(driver2));
            Assert.True(Object.ReferenceEquals(driver1, driver2));
        }

        private DriverSaved GetStudent(string firstName, string secondName)
        {
            return new DriverSaved(firstName, secondName);
        }
    }
}