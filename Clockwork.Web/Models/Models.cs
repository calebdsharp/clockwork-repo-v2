using System;
using System.Collections.Generic;
using System.Web.Mvc;

namespace Clockwork.Web.Models
{
    public class RequestedTimesModel
    {
        public int CurrentTimeQueryId { get; set; }
        public DateTime Time { get; set; }
        public string ClientIp { get; set; }
        public DateTime UTCTime { get; set; }
        public string TimeZone { get; set; }
    }

    public class CurrentTimeRequestModel
    {
        public int CurrentTimeQueryId { get; set; }
        public DateTime Time { get; set; }
        public string ClientIp { get; set; }
        public DateTime UTCTime { get; set; }
        public string TimeZone { get; set; }
    }

    public class AllTimesViewModel
    {
        public CurrentTimeRequestModel CurrentTimeRequest { get; set; }
        public List<RequestedTimesModel> RequestedTimes { get; set; }
        public string SelectedTimezoneId { get; set; }
        public IEnumerable<SelectListItem> Timezones { get; set; }
    }
}


