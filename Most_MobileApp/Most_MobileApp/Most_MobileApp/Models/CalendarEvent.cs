using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace Most_MobileApp.Models
{
    public class CalendarEvent
    {
        [JsonProperty("items")]
        public List<Item> Items { get; set; }

        public CalendarEvent()
        {
            //constructor
        }

    }
    public class Item
    {
        [JsonProperty("summary")]
        public string Summary { get; set; }
        [JsonProperty("start")]
        public StartDate StartDate { get; set; }
        [JsonProperty("end")]
        public EndDate EndDate { get; set; }
    }
    public class StartDate
    {
        [JsonProperty("dateTime")]
        public DateTime StartDataTime { get; set; }
    }
    public class EndDate
    {
        [JsonProperty("dateTime")]
        public DateTime EndDataTime { get; set; }
    }
}
