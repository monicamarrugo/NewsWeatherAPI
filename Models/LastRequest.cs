using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace NewsWeatherAPI.Models
{
    public class LastRequest
    {
        public string IDRequest {get; set;}
        public string IDCity { get; set;}
        public string IDCountry { get; set;}
        public string CityName { get; set;}
        public string CountryName { get; set;}
        public string UrlApi { get; set; }

    }
}