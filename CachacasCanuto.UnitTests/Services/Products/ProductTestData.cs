namespace CachacasCanuto.UnitTests.Services.Products
{
    public class ProductTestData
    {
        public static IEnumerable<object[]> GetAllValidData
        {
            get
            {
                yield return new object[] { "hacinha", 25m, 30m };
                yield return new object[] { "CANA DA BOA", 6m, 11m };
                yield return new object[] { "SinGL", 80m, 99m };
            }
        }

        public static IEnumerable<object[]> GetAllInvalidData
        {
            get
            {
                yield return new object[] { "Cachacinha", 30m, 45m };
                yield return new object[] { "Cana da boa", 50m, 66m };
                yield return new object[] { "Singleton", 60m, 78m };
            }
        }

    }
}
