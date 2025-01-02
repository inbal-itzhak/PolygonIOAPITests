using Allure.NUnit.Attributes;
using RestSharp;
using System.Text.Json;


namespace PolygonTests.APITests.PolygonAPI
{

    public class PolygonSteps : BaseTestAPI
    {
      //  string polygonApiKey = config["ApiKeys:PolygonIoApiKey"];
        private PolygonIORequests _polygonIORequests;
        public RestClient _client;

        public PolygonSteps(RestClient client)
        {
            _client = client ?? throw new ArgumentNullException(nameof(client));
           
            _polygonIORequests = new PolygonIORequests(_client, config["ApiKeys:PolygonIoApiKey"]);
        }

        [AllureStep("Send Get Open Close Data Reuest for PolygonIO in specific date")]
        public async Task <PolygonOpenCloseData> GetStockOpenCloseDataByDatePolygonIO(string ticker, string date)
        {
            var polygonResponse = await _polygonIORequests.GetOpenCloseData(ticker,date);
            Assert.IsNotNull(polygonResponse);
            Assert.That((int)polygonResponse.StatusCode, Is.EqualTo(200), $"Response status code is {polygonResponse.StatusCode}, should be 200");
            var polygonResponseData = JsonSerializer.Deserialize<PolygonOpenCloseData>(polygonResponse.Content);
            return polygonResponseData;
        }

        public DateTime GetPreviousBusinessDay()
        {
            DateTime previousDay = DateTime.Today.AddDays(-1);
            if (previousDay.DayOfWeek == DayOfWeek.Saturday)
            {
                return previousDay.AddDays(-1);
            }
            if (previousDay.DayOfWeek == DayOfWeek.Sunday)
            {
                return previousDay.AddDays(-2);
            }
            List<DateTime> marketClosedDays = MarketClosedDates();
            foreach (DateTime holiday in marketClosedDays)
            {
                if (previousDay == holiday)
                {
                    previousDay = holiday.AddDays(-1);
                    if (previousDay.DayOfWeek == DayOfWeek.Saturday)
                    {
                        return previousDay.AddDays(-1);
                    }
                    if (previousDay.DayOfWeek == DayOfWeek.Sunday)
                    {
                        return previousDay.AddDays(-2);
                    }
                }
            }
            return previousDay;

        }

        public DateTime GetLastTradeDateTime()
        {
            TimeSpan openTime = new TimeSpan(9, 30, 0);
            TimeSpan closeingTime = new TimeSpan(16, 00, 00);
            List<DateTime> marketClosedDays = MarketClosedDates();
            TimeZoneInfo et = TimeZoneInfo.FindSystemTimeZoneById("Eastern Standard Time");
            DateTime now = DateTime.Now;
            now = now.Kind == DateTimeKind.Unspecified ? now : DateTime.Now;
           // DateTime utcNow = TimeZoneInfo.ConvertTimeToUtc(now, TimeZoneInfo.Local);
            DateTime easternTime = TimeZoneInfo.ConvertTime(now,TimeZoneInfo.Local, et);
          //  DateTime now = TimeZoneInfo.ConvertTimeFromUtc(DateTime.UtcNow, et);
            DateTime today = TimeZoneInfo.ConvertTime(DateTime.Today, TimeZoneInfo.Local, et);

            if (easternTime.DayOfWeek != DayOfWeek.Saturday && now.DayOfWeek != DayOfWeek.Sunday)
            {
               if(easternTime.TimeOfDay > openTime && easternTime.TimeOfDay < closeingTime)
                {
                    return now;
                    //returnDateTime.UtcNow;
                }
               if(easternTime.TimeOfDay < openTime && easternTime.DayOfWeek == DayOfWeek.Monday)
                {
                    return TimeZoneInfo.ConvertTimeFromUtc(today.AddDays(-3).AddHours(16), et);
                   // return DateTime.Today.AddDays(-3).AddHours(16);
                }
               if(easternTime.TimeOfDay > closeingTime || easternTime.TimeOfDay < openTime)
                {
                    return TimeZoneInfo.ConvertTimeFromUtc(today.AddHours(16), et);
                    //return DateTime.Today.AddHours(16);
                }
            }
           else if(easternTime.DayOfWeek == DayOfWeek.Saturday)
            {
                return TimeZoneInfo.ConvertTimeFromUtc(today.AddDays(-1).AddHours(16), et);
               // return DateTime.Today.AddDays(-1).AddHours(16);
            }
            else if (easternTime.DayOfWeek == DayOfWeek.Sunday)
            {
                return TimeZoneInfo.ConvertTimeFromUtc(today.AddDays(-2).AddHours(16), et);
               // return DateTime.Today.AddDays(-2).AddHours(16);
            }
            foreach(DateTime holiday in marketClosedDays) 
            {
                if(easternTime.Date == holiday)
                {
                    return TimeZoneInfo.ConvertTimeFromUtc(GetPreviousBusinessDay().AddHours(16), et);
                   // GetPreviousBusinessDay().AddHours(16);
                }
            }
            return easternTime;
            //returnDateTime.UtcNow;
        }

        private List<DateTime> MarketClosedDates()
        {
            List<DateTime> holidayDates = new List<DateTime>();
            string christmas = "2024-12-25";
            string newYears = "2025-01-01";
            string mlkDay = "2025-01-20";
            string presidents = "2025-02-17";
            string goodFriday = "2025-04-18";
            string memorialDay = "2025-05-26";
            string juneteenth = "2025-06-19";
            string independenceDay = "2025-07-04";
            string laborDay = "2025-09-01";
            string thanksgiving = "2025-11-27";
            string nextChristmas = "2025-12-25";
            List<string> holidays = new List<string>() { christmas, newYears, mlkDay, presidents, goodFriday, memorialDay, juneteenth, independenceDay, thanksgiving, nextChristmas };
            foreach (string holiday in holidays)
            {
                holidayDates.Add(DateTime.Parse(holiday));
            }
            return holidayDates;





        }
    }
}
