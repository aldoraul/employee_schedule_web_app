using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.ComponentModel;

namespace CommerceApp.Models
{
    public class TimeOff
    {
        [DisplayName("Time Off I.D.")]

        public int TimeOffID { get; set; }

        public int EmployeeID { get; set; }
        public virtual Employee Employee { get; set; }


        [DisplayFormat(DataFormatString = "{0:MMM dd yyyy}", ApplyFormatInEditMode = true)]
        [DisplayName("First Day")]

        public DateTime FirstDay { get; set; }

        [DisplayFormat(DataFormatString = "{0:MMM dd yyyy}", ApplyFormatInEditMode = true)]
        [DisplayName("Last Day")]

        public DateTime LastDay { get; set; }
        
        [DisplayName("Time Off Type")]

        public string timeOffType { get; set; }

    }
}