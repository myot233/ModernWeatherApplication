using ModernWeatherApplication.Service;
using static SkiaSharp.HarfBuzz.SKShaper;

namespace TestForWeather
{
    [TestClass]
    public class UnitTest1
    {
        [TestMethod]
        
        public void TestMethod1()
        {
            ApiService service = new ApiService();
            var result_task =  service.FetchWeatherData(101210106);
            var result = result_task.GetAwaiter().GetResult();
            Console.WriteLine(result);
            Console.WriteLine(result.Count);
        }
    }
}