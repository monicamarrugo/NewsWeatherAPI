using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace NewsWeatherAPI.Models
{
    


    public class CityWeatherNews
    {
        public Article[] articles { get; set; }
        public Current_Weather current_weather { get; set; }
    }

    public class Current_Weather
    {
        public Weather[] weather { get; set; }
        public Main main { get; set; }
        public Sys sys { get; set; }
        public string name { get; set; }
        public int cod { get; set; }
    }

}