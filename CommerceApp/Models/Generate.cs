using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using CommerceApp.MockClass;
using CommerceApp.Models;
using System.Data.Entity;
using System.Net;


namespace CommerceApp
{
    public class GenerateSchedule {

        private EmployeeDBContext db = new EmployeeDBContext();  
                                 //db.Database.ExecuteSqlCommand("TRUNCATE TABLE[SHIFTS]");
        int typeOfJob = 0;
        DateTime startDate = new DateTime(2015, 1, 7);
                                 // int leastShifts = 0;
        IList<JanitorSchedule> olb_schedule = new List<JanitorSchedule>();
        // bool prim_or_sec = true;
        
       
        public IList<JanitorSchedule> GenerateOLB()
        {           
            
            GenerateShifts allShifts = new GenerateShifts();
                                          // int OLB_p_or_s = 1; // primary olb = 1, secondary olb = 2  
            List<Employee> fullTeam = db.Employees.ToList();

            List<Employee> MGRTeam = new List<Employee>();
            List<Employee> OLBTeam = new List<Employee>();

            foreach (Employee element in fullTeam)
            {
                if (element.jobTitle == "OLB")
                    OLBTeam.Add(element);
                else if(element.jobTitle == "MGR")
                    MGRTeam.Add(element);
            }          
                if(!db.Shifts.Any()){
                     allShifts.Generate_OLB_Shifts(OLBTeam);
                             // here i have the olb team now i need to generate shifts to create schedule
                     // allShifts.Generate_MGR_Shifts(MGRTeam);
                }

             var shifts = db.Shifts.ToList();

            foreach (Shift element in shifts)
            {
                if (element.ShiftPrimary == true)
                 typeOfJob = 1;
                else
                  typeOfJob = 2;

                string status_string = Enums.GetName<ScheduleType>((ScheduleType)typeOfJob);
                string statusColor = Enums.GetEnumDescription<ScheduleType>(status_string);
                string jobDescription = statusColor.Substring(8, statusColor.Length - 8);
                string colorCode = statusColor.Substring(0, statusColor.IndexOf(":"));
              //  int shift_num = 1;
                olb_schedule.Add
                    (
                    new JanitorSchedule
                    {
                        ID = element.ShiftID, 
                        Title = jobDescription + " " + element.Employee.lastName,
                        IsAllDayEvent = true,
                        Start = element.ShiftDate,
                        End = element.ShiftDate.AddDays(6),
                        color = colorCode
                    }

                    );
            }


         return olb_schedule; // don't need this till the end
        

}

        }
       
       
} 


   
