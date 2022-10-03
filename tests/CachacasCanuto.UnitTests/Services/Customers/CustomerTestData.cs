namespace CachacasCanuto.UnitTests.Services.Customers
{
    public class CustomerTestData
    {
        public static IEnumerable<object[]> GetAllValidData
        {
            get
            {
                yield return new object[] { "LEWIS HAMILTON", new DateTime(1983, 01, 07), new DateTime(1986, 01, 07) };
                yield return new object[] { "juan", new DateTime(1700, 10, 20), new DateTime(1819, 10, 27) };
                yield return new object[] { "cAsI", new DateTime(1993, 10, 20), new DateTime(1993, 10, 27) };
            }
        }

        public static IEnumerable<object[]> GetAllInvalidData
        {
            get
            {
                yield return new object[] { "LEWIS HAMILTON", new DateTime(1973, 01, 07), new DateTime(1976, 01, 07) };
                yield return new object[] { "juan", new DateTime(1600, 10, 20), new DateTime(1619, 10, 27) };
                yield return new object[] { "cAsI", new DateTime(1991, 10, 20), new DateTime(1992, 10, 27) };
            }
        }
    }
}
