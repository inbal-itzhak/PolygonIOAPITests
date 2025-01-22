using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PolygonAPITests
{
    public class DataProvider
    {
        public static object[] TickerTocompanyName =
        {
            new object[] {"IBM", "International Business Machines Corporation" },
            new object[] {"AMD", "Advanced Micro Devices Inc" },
            new object[] {"ADBE","Adobe Inc." },
            new object[] {"NFLX", "Netflix Inc"},
            new object[] {"META", "Meta Platforms Inc" }
        };

        public static object[] DatesToTest =
        {
            new object[]{2024,11,29},
            new object[]{2023,12,31},
            new object[]{DateTime.Now.Year,DateTime.Now.Month,DateTime.Now.Day}
        };

        public static object[] TickersToTest =
        {
            new object[] {"BKNG"},
            new object[] {"MSFT"},
            new object[] {"TSLA"},
            new object[] {"WDAY"},
            new object[] {"AMZN"},

        };




    }
}
