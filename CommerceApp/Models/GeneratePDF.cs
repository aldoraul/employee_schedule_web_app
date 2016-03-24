using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel;

namespace CommerceApp.Models
{
    public class GeneratePDF
    {
        [DisplayName("Number of Months")]
        public NumOfMonths numberOfMonths { get; set; }
        [DisplayName("Start Date")]
        [DisplayFormat(DataFormatString = "{0:MMM dd yyyy}", ApplyFormatInEditMode = true)]
        public DateTime StartDate { get; set; }
    }
    public enum NumOfMonths
    {
        Month=1,
        Quarter=4,
        Year=12
    }
}