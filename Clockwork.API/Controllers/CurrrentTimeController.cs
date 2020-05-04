using System;
using Microsoft.AspNetCore.Mvc;
using Clockwork.API.Models;
using System.Collections.Generic;
using Clockwork.API.Service;

namespace Clockwork.API.Controllers
{
    public class CurrentTimeController : Controller
    {
        [HttpGet("api/currenttime")]
        public IActionResult Get(string timezone)
        {
            // Parse timezone get timezone name
            var parsedTimeZoneInfo = ParseTimeZone(timezone);
            if (parsedTimeZoneInfo == null)
            {
                return NotFound();
            }

            // Create new entry
            var utcTime = DateTime.UtcNow;
            // convert UTC time to local time
            var time = TimeZoneInfo.ConvertTimeFromUtc(utcTime, parsedTimeZoneInfo);
            var newEntry = new CurrentTimeQuery
            {
                UTCTime = utcTime,
                Time = time,
                ClientIp = this.HttpContext.Connection.RemoteIpAddress.ToString(),
                TimeZone = parsedTimeZoneInfo.Id
            };

            // insert new entry to database
            ClockWorkRepository.InsertNewEntry(newEntry);

            var returnData = new Dictionary<string, object>
            {
                { "newEntry", newEntry },
                { "allEntries", ClockWorkRepository.FetchAll() }
            };

            // return new entry
            return Ok(returnData);
        }

        // pass timezone to method
        private TimeZoneInfo ParseTimeZone(string timezone)
        {
            try
            {   // get timezone by Id / location name 
                var timeZoneInfo = TimeZoneInfo.FindSystemTimeZoneById(timezone);
                Console.WriteLine("Time zone info: {0}", timezone);
                return timeZoneInfo;
            }
            catch (Exception e)
            {
                Console.WriteLine("Unable to parse timezone: {0}; Error: {1}", timezone, e);
                return null;
            }
        }

        [HttpGet("api/alltimes")]
        public IEnumerable<CurrentTimeQuery> GetAllTimes()
        {
            //get all time from Clockwork Repo
            var allTimes = ClockWorkRepository.FetchAll();

            return allTimes;
        }
    }
}