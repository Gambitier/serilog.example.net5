using Destructurama.Attributed;
using System;

namespace WebApplication3
{
    public class WeatherForecast
    {
        public DateTime Date { get; set; }

        [NotLogged]
        public int TemperatureC { get; set; }

        public int TemperatureF => 32 + (int)(TemperatureC / 0.5556);

        public string Summary { get; set; }
    }
}
