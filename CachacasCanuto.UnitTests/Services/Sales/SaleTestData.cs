namespace CachacasCanuto.UnitTests.Services.Sales
{
    public class SaleTestData
    {
        public static IEnumerable<object[]> GetAllValidData
        {
            get
            {
                yield return new object[] { new DateTime(2000, 01, 01), new DateTime(2022, 12, 25), "Hamilton", new List<string> { "IdHamilton" }, "Cachacinha", new List<string> { "IdCachacinha" }, 10, 1};
                yield return new object[] { new DateTime(1999, 01, 01), new DateTime(1999, 12, 25), "Casimiro", new List<string> { "IdCasimiro" }, "Singleton", new List<string> { "IdSingleton" }, 10, 1 };
                yield return new object[] { new DateTime(1689, 01, 01), new DateTime(1880, 12, 25), "Don Juan", new List<string> { "IdDonJuan" }, "Cana Braba", new List<string> { "IdCanaBraba" }, 10, 1 };
            }
        }

        public static IEnumerable<object[]> GetAllInvalidData
        {
            get
            {
                yield return new object[] { new DateTime(2000, 01, 01), new DateTime(2022, 12, 25), "Chuvisco", "Cachacinha", 10, 1 };
            }
        }
    }
}
