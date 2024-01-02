using ModernWeatherApplication.Service;

namespace TestForWeather
{
    [TestClass]
    public class ApiUnitTest
    {
        [TestMethod]
        
        public void TestFetchWeatherDataSevenDay()
        {
            ApiService service = new ApiService(null);
            var resultTask =  service.FetchWeatherDataSevenDay(101210106);
            var result = resultTask.GetAwaiter().GetResult();
            Console.WriteLine(result);
            Console.WriteLine(result.Count);
        }

        [TestMethod]

        public void TestFetchWeatherDataPerHour()
        {
            ApiService service = new ApiService(null);
            var resultTask = service.FetchWeatherDataPerHour(101210106);
            var result = resultTask.GetAwaiter().GetResult();
            Console.WriteLine(result);
            Console.WriteLine(result.Count);
        }

        [TestMethod]

        public void TestFetchAirPollutionNow()
        {
            ApiService service = new ApiService(null);
            var resultTask = service.FetchAirPollutionNow(101210106);
            var result = resultTask.GetAwaiter().GetResult();
            Console.WriteLine(result);
            Console.WriteLine(result.pubTime);
        }
    }
}