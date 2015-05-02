using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.Entity;//BA
using System.ComponentModel.DataAnnotations; //BA For setting the key to create the DBContext and controller
using System.ComponentModel.DataAnnotations.Schema;//BA
using System.ComponentModel;
namespace CommerceApp.Models
{
    public class Shift
    {
        public int ShiftID { get; set; }
        public int EmployeeID { get; set; }
        public virtual Employee Employee { get; set; }
        [DisplayFormat(DataFormatString = "{0:MMM dd yyyy}", ApplyFormatInEditMode = true)]
        public DateTime ShiftDate { get; set; }
        public bool ShiftPrimary { get; set; }

    }
}