using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

namespace NorthwindPresentation.Controllers
{
    [Route("api/[controller]")]
    public class CustomerController : Controller
    {
        

//        [HttpGet("[action]")]
//        public IEnumerable<WeatherForecast> WeatherForecasts()
//        {
//            var rng = new Random();
//            return Enumerable.Range(1, 5).Select(index => new WeatherForecast
//            {
//                DateFormatted = DateTime.Now.AddDays(index).ToString("d"),
//                TemperatureC = rng.Next(-20, 55),
//                Summary = Summaries[rng.Next(Summaries.Length)]
//            });
//        }
//
//        public class WeatherForecast
//        {
//            public string DateFormatted { get; set; }
//            public int TemperatureC { get; set; }
//            public string Summary { get; set; }
//
//            public int TemperatureF
//            {
//                get { return 32 + (int) (TemperatureC / 0.5556); }
//            }
//        }
    }
}