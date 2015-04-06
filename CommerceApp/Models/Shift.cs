using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CommerceApp.Models
{
    public class Shift
    {
        public int ShiftID { get; set; }
        public int EmployeeID { get; set; }
        public virtual Employee Employee { get; set; }
        public DateTime ShiftDate { get; set; }
        public bool ShiftPrimary { get; set; }

    }
}