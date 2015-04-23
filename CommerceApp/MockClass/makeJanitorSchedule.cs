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
            var timeOff = db.TimeOffs.ToList();
            var i = 1;
            foreach (TimeOff element in timeOff)
            {
                list.Add
                (
                new JanitorSchedule
                {
                    ID = i,
                    Title = element.Employee.firstName ,
                    IsAllDayEvent = true,
                    Start = element.FirstDay,
                    End = element.LastDay,
                    color = String.Format("red")

                }
                );

                i++;
            }

            foreach (Shift element in shifts)
            {
                list.Add
            (
                new JanitorSchedule
                 {
                     ID = ++i,
                     Title = element.Employee.lastName,
                     IsAllDayEvent = true,
                     Start = element.ShiftDate,
                     End = element.ShiftDate.AddDays(6),
                     color = String.Format("blue")
                 }

            );

            }
            return list;
        }
    }
}
    
