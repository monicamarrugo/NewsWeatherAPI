using Insight.Database;
using NewsWeatherAPI.Models.Messages;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NewsWeatherAPI.Models.Repositories
{
    public interface INewsRepository
    {
        [Sql("History.sp_InsertLastRequest")]
        void SaveRequest(LastRequestMessages lastRequest);

        [Sql("History.sp_SelecttLastRequest")]
        List<LastRequest> GetLastRequests();

    }
}
