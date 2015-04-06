using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using CommerceApp.Models;

namespace CommerceApp.MockClass
{
    public class makeJanitorSchedule
    {
        private EmployeeDBContext db = new EmployeeDBContext();

        public IList<JanitorSchedule> GetJanitorSchedule()
        {
            var list = new List<JanitorSchedule>();
            var shifts = db.Shifts.ToList();
            var i = 1;
            foreach (Shift element in shifts){
                list.Add
                (
                new JanitorSchedule
                {
                    ID=i,
                    Title=element.Employee.firstName,
                    IsAllDayEvent = true,
                    Start = element.ShiftDate,
                    End = element.ShiftDate.AddDays(6),
                    color = String.Format("blue")
                }
    /*                new JanitorSchedule
                    {
                        ID = 1,
                        Title = String.Format("Toilet Duty"),
                        IsAllDayEvent = true,
                        Start = DateTime.Now.AddDays(0).AddHours(0),
                        End = DateTime.Now.AddDays(6).AddHours(0),
                        color = String.Format("blue")
                    }
    */
                    );
            }
            return list;
        }
    }
}