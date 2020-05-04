using System;
using System.Linq;
using Clockwork.API.Models;

namespace Clockwork.API.Service
{
    internal struct ClockWorkRepository
    {
        public static void InsertNewEntry(CurrentTimeQuery newEntry)
        {
            // insert new entry to database
            var db = new ClockworkContext();
            db.CurrentTimeQueries.Add(newEntry);
            Console.WriteLine("{0} records(s) saved to database", db.SaveChanges());
        }

        public static IOrderedQueryable<CurrentTimeQuery> FetchAll()
        {
            // get all entries
            var db = new ClockworkContext();
            IOrderedQueryable<CurrentTimeQuery> timeQueries =
                from query in db.CurrentTimeQueries
                orderby query.CurrentTimeQueryId
                select query;

            return timeQueries;
        }
    }
}
