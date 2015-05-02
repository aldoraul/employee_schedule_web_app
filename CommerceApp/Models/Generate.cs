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
    public class GenerateSchedule
    {

        private EmployeeDBContext db = new EmployeeDBContext();
        int typeOfJob = 0;
        DateTime startDate = new DateTime(2015, 1, 7);
        // int leastShifts = 0;
        IList<JanitorSchedule> all_schedule = new List<JanitorSchedule>();
        List<Employee> MGRTeam = new List<Employee>();

        List<Employee> HSFTeam = new List<Employee>();
        List<Employee> OLBTeam = new List<Employee>();

        public IList<JanitorSchedule> GenerateOLB()
        {


                GenerateShifts allShifts = new GenerateShifts();
                // int OLB_p_or_s = 1; // primary olb = 1, secondary olb = 2  
                List<Employee> fullTeam = db.Employees.ToList();
               

                foreach (Employee element in fullTeam)
                {
                    if (element.jobTitle == "OLB")
                        OLBTeam.Add(element);
                    if (element.jobTitle == "MGR")
                        MGRTeam.Add(element);
                    if (element.jobTitle == "HSF")
                        HSFTeam.Add(element);
                }
                if (!db.Shifts.Any())
                {
                    allShifts.Generate_OLB_Shifts(OLBTeam);
                    allShifts.Generate_HSF_Shifts(HSFTeam);
                    allShifts.Generate_MGR_Shifts(MGRTeam);

                    // here i have the olb team now i need to generate shifts to create schedule
                    // allShifts.Generate_MGR_Shifts(MGRTeam);
                }

                var shifts = db.Shifts.ToList();

                foreach (Shift element in shifts)
                {
                    string name = element.Employee.jobTitle;
                    switch(name)
                    {
                        case "MGR":
                            typeOfJob = 0;
                            break;
                        case "OLB":
                            if (element.ShiftPrimary == true)
                            {
                                typeOfJob = 1;
                                break;
                            }
                            else
                            {
                                typeOfJob = 2;
                                break;
                            }
                        case "HSF":
                            if(element.ShiftPrimary == true)
                            {
                                typeOfJob = 3;
                                break;
                            }
                            else{
                                typeOfJob = 4;
                                break;
                            }
                        default:
                            break;
                    }
                    
                    string status_string = Enums.GetName<ScheduleType>((ScheduleType)typeOfJob);
                    string statusColor = Enums.GetEnumDescription<ScheduleType>(status_string);
                    string jobDescription = statusColor.Substring(8, statusColor.Length - 8);
                    string colorCode = statusColor.Substring(0, statusColor.IndexOf(":"));
                    //  int shift_num = 1;
                    if (name == "MGR")
                    {
                        all_schedule.Add
                            (
                            new JanitorSchedule
                            {
                                ID = element.ShiftID,
                                Title = jobDescription + " " + element.Employee.lastName,
                                IsAllDayEvent = true,
                                Start = element.ShiftDate,
                                End = element.ShiftDate.AddDays(13),
                                color = colorCode
                            }

                            );
                    }else{
                        all_schedule.Add
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
                }

                 return all_schedule; // don't need this till the end
        }

    }
}


   
