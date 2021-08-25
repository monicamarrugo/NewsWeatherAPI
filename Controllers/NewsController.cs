using NewsWeatherAPI.Models;
using NewsWeatherAPI.Models.Messages;
using NewsWeatherAPI.Models.Repositories;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.Common;
using System.Data.SqlClient;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Web.Http;
using Insight.Database;

namespace NewsWeatherAPI.Controllers
{
    public class NewsController : ApiController
    {
        private readonly String _urlApiNews;
        private readonly String _urlApiWeather;
        private readonly String _urlApiKeyNews;
        private readonly String _urlApiKeyWeather;
        private readonly String _connectionStrings;
        public NewsController()
        {
             _urlApiNews = ConfigurationManager.AppSettings["UrlWebApiNews"].ToString();
            _urlApiWeather = ConfigurationManager.AppSettings["UrlWebApiWeather"].ToString();
            _urlApiKeyNews = ConfigurationManager.AppSettings["ApiKeyNews"].ToString();
            _urlApiKeyWeather = ConfigurationManager.AppSettings["ApiKeyWeather"].ToString();
            this._connectionStrings = ConfigurationManager.ConnectionStrings["SystemDatabase"].ConnectionString;
        }

        [HttpGet]
        public HttpResponseMessage CurrentNews([FromUri] String country, [FromUri] String city)
        {
            string urlApiNewsParams = String.Format("country={0}&apiKey={1}", country, _urlApiKeyNews);
            string urlApiWeatherParams = String.Format("id={0}&appid={1}", city, _urlApiKeyWeather);

            string urlnews = _urlApiNews + urlApiNewsParams;
            string urlweather = _urlApiWeather + urlApiWeatherParams;

            string jsonNews = new WebClient().DownloadString(urlnews);
            string jsonWeather = new WebClient().DownloadString(urlweather);

            News oNews = JsonConvert.DeserializeObject<News>(jsonNews);
            ClassWeather oWeather = JsonConvert.DeserializeObject<ClassWeather>(jsonWeather);

            CityWeatherNews weatherNews = new CityWeatherNews()
            {
                articles = oNews.articles,
                current_weather = new Current_Weather()
                {
                    weather = oWeather.weather,
                    main = oWeather.main,
                    sys = oWeather.sys,
                    name = oWeather.name,
                    cod = oWeather.cod
                }
            };

            LastRequestMessages lastrequest = new LastRequestMessages()
            {
                iVch_IDCity = city,
                iVch_IDCountry = country,
                iVch_CityName = oWeather.name,
                iVch_CountryName = oWeather.sys.country,
                iVch_UrlApi = urlnews
            };

            this.SaveRequest(lastrequest);

            var jsData = JsonConvert.SerializeObject(weatherNews, Newtonsoft.Json.Formatting.Indented);

            return new HttpResponseMessage { Content = new StringContent(jsData, Encoding.UTF8, "application/json") };

}

        private void SaveRequest(LastRequestMessages lastrequest)
        {
            
            using (DbConnection c = new SqlConnection(_connectionStrings))
            {
                try
                {
                    INewsRepository newsRepository = c.As<INewsRepository>();
                    newsRepository.SaveRequest(lastrequest);
                }
                catch (Exception e)
                {
                  throw new Exception(e.Message);
                }
            }
        }

        [HttpGet]
        public HttpResponseMessage GetLastRequest()
        {
            List<LastRequest> dataList = new List<LastRequest>();
            using (DbConnection c = new SqlConnection(_connectionStrings))
            {
                try
                {
                    INewsRepository newsRepository = c.As<INewsRepository>();
                    dataList = newsRepository.GetLastRequests();
                }
                catch (Exception e)
                {
              
                    throw new Exception(e.Message);
                }
            }

            var jsData = JsonConvert.SerializeObject(dataList, Newtonsoft.Json.Formatting.Indented);

            return new HttpResponseMessage { Content = new StringContent(jsData, Encoding.UTF8, "application/json") };
        }

    }

    
}
