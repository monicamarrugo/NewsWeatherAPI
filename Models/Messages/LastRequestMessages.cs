using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace NewsWeatherAPI.Models.Messages
{
    public class LastRequestMessages
    {
        [JsonProperty("IdCity")]
        public String iVch_IDCity { get; set; }

        [JsonProperty("IdCountry")]
        public String iVch_IDCountry { get; set; }

        [JsonProperty("CityName")]
        public String iVch_CityName { get; set; }

        [JsonProperty("CountryName")]
        public String iVch_CountryName { get; set; }

        [JsonProperty("UrlApi")]
        public String iVch_UrlApi { get; set; }

    }
}