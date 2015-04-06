using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CommerceApp.MockClass
{
    public class JanitorSchedule
    {
        public int ID { get; set; }
        public string Title { get; set; }
        public bool IsAllDayEvent { get; set; }
        public DateTime Start { get; set; }
        public DateTime End { get; set; }
        public string color { get; set; }
    }
}