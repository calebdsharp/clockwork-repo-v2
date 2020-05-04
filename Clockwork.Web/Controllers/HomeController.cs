using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Web.Mvc;
using System.Web.Script.Serialization;
using Clockwork.Web.Models;

namespace Clockwork.Web.Controllers
{
    public class HomeController : Controller
    {
        private static HttpClient client;

        public ActionResult Index()
        {
            List<SelectListItem> items = new List<SelectListItem>();
            IReadOnlyCollection<TimeZoneInfo> timeZoneInfos = TimeZoneInfo.GetSystemTimeZones();
            foreach (TimeZoneInfo info in timeZoneInfos)
            {
                SelectListItem item = new SelectListItem() { Text = info.Id, Value = info.Id };
                items.Add(item);
            }

            var model = new AllTimesViewModel
            {
                Timezones = items
            };

            return View(model);
        }

        [HttpPost]
        public ActionResult Index(string SelectedTimezoneId)
        {
            client = new HttpClient();
            client.BaseAddress = new Uri("http://localhost:5000/");

            client.DefaultRequestHeaders.Accept.Clear();
            client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
            string response = client.GetStringAsync("/api/currenttime?timezone=" + SelectedTimezoneId).Result;

            var currentTime = new JavaScriptSerializer().DeserializeObject(response);

            return Json(currentTime, JsonRequestBehavior.AllowGet);
        }

        // GET: lit of requested times
        public ActionResult GetRequestedTimes()
        {
            client = new HttpClient();
            client.BaseAddress = new Uri("http://localhost:5000/");

            client.DefaultRequestHeaders.Accept.Clear();
            client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
            string response = client.GetStringAsync("api/alltimes").Result;

            // deserialize JSON
            var alltimes = new JavaScriptSerializer().DeserializeObject(response);

            return Json(alltimes, JsonRequestBehavior.AllowGet);
        }
    }
}

