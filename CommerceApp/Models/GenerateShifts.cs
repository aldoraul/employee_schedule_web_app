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
    public class GenerateShifts
    {
        int shift_id = 1;
        public EmployeeDBContext db = new EmployeeDBContext();
       // DateTime startDate = new DateTime(2015, 1, 7);
       // DateTime endDate = new DateTime(2016, 1, 5);
        DateTime current = DateTime.Now;
       
        public void Generate_OLB_Shifts(List<Employee> olbTeam)
        {
            int leastShift = 1;
            bool hasTimeOff = false;
            bool prim_or_sec = true;
            bool next = true;
            DateTime for_now = current.AddMonths(-6);
            while ((int)for_now.DayOfWeek != 3)
                for_now = for_now.AddDays(1);
            DateTime startDate = for_now;
            DateTime endDate = current.AddMonths(12);
            while (startDate < endDate)
            {
                foreach (Employee person in olbTeam)
                {
                    if (startDate > endDate)
                        break;
                    hasTimeOff = checkTimeOff(startDate, person, false);
                    if (hasTimeOff == false)
                    {
                        Shift again = new Shift();
                        if (person.daysFirstCall < leastShift || prim_or_sec != true)
                        {
                            again.ShiftID = shift_id++;
                            again.EmployeeID = person.EmployeeID;

                            again.ShiftDate = startDate;//.AddDays(count);
                            again.ShiftPrimary = prim_or_sec;

                            db.Entry(again).State = EntityState.Added;
                            db.SaveChanges();

                            if (prim_or_sec == true)
                            {
                                Employee employee;
                                employee = db.Employees.Where(s => s.EmployeeID == person.EmployeeID).FirstOrDefault<Employee>();
                                employee.daysFirstCall = employee.daysFirstCall + 1;
                                person.daysFirstCall = person.daysFirstCall + 1;
                                db.Entry(employee).State = EntityState.Modified;
                                db.SaveChanges();               // switch back from primary to secondary
                                prim_or_sec = false;
                            }
                            else
                            {
                                Employee employee;
                                employee = db.Employees.Where(s => s.EmployeeID == person.EmployeeID).FirstOrDefault<Employee>();
                                employee.daysSecondCall = employee.daysSecondCall + 1;
                                person.daysSecondCall = person.daysSecondCall + 1;
                                db.Entry(employee).State = EntityState.Modified;
                                db.SaveChanges();
                                startDate = startDate.AddDays(7);
                                prim_or_sec = true;

                            }
                        }
                    }
                    
                }
                if (next == true)
                {
                    next = false;
                }
                else
                {
                    ++leastShift;
                    next = true;
                }
                        
               

            }
        }
        public void Generate_MGR_Shifts(List<Employee> mgrTeam)
        {
            DateTime for_now = current.AddMonths(-6);
            while ((int)for_now.DayOfWeek != 3)
                for_now = for_now.AddDays(1);
            DateTime startDate = for_now;
            DateTime endDate = current.AddMonths(12);
        bool hasTimeOff = false;

        while (startDate < endDate)
        {
            foreach (Employee person in mgrTeam)
            {
                if (startDate > endDate)
                    break;
                hasTimeOff = checkTimeOff(startDate, person, true);
                if (hasTimeOff == false)
                {
                    Shift newShift = new Shift();
                    newShift.ShiftID = shift_id++;
                    newShift.EmployeeID = person.EmployeeID;
                    newShift.ShiftDate = startDate;//.AddDays(count);
                    //newShift.ShiftPrimary = prim_or_sec;

                    db.Entry(newShift).State = EntityState.Added;
                    Employee employee;
                    employee = db.Employees.Where(s => s.EmployeeID == person.EmployeeID).FirstOrDefault<Employee>();
                    employee.daysFirstCall = employee.daysFirstCall + 1;
                    person.daysFirstCall = person.daysFirstCall + 1;
                    db.Entry(employee).State = EntityState.Modified;
                    db.SaveChanges();               // switch back from primary to seconda

                    startDate = startDate.AddDays(14);
                }
               
            }

        }
    
    }
    public void Generate_HSF_Shifts(List<Employee> hsfTeam)
    {
        DateTime for_now = current.AddMonths(-6);
        while ((int)for_now.DayOfWeek != 3)
            for_now = for_now.AddDays(1);
        DateTime startDate = for_now;
        DateTime endDate = current.AddMonths(12);
        int leastShift = 1;
        bool prim_or_sec = true;
        bool next = true;
        bool hasTimeOff = false;

        while (startDate < endDate)
        {
            foreach (Employee person in hsfTeam)
            {
                if (startDate > endDate)
                    break;
                hasTimeOff = checkTimeOff(startDate, person, false);
                if (hasTimeOff == false)
                {
                    Shift again = new Shift();
                    if (person.daysFirstCall < leastShift || prim_or_sec != true)
                    {
                        again.ShiftID = shift_id++;
                        again.EmployeeID = person.EmployeeID;

                        again.ShiftDate = startDate;//.AddDays(count);
                        again.ShiftPrimary = prim_or_sec;

                        db.Entry(again).State = EntityState.Added;
                        db.SaveChanges();

                        if (prim_or_sec == true)
                        {
                            Employee employee;
                            employee = db.Employees.Where(s => s.EmployeeID == person.EmployeeID).FirstOrDefault<Employee>();
                            employee.daysFirstCall = employee.daysFirstCall + 1;
                            person.daysFirstCall = person.daysFirstCall + 1;
                            db.Entry(employee).State = EntityState.Modified;
                            db.SaveChanges();               // switch back from primary to secondary
                            prim_or_sec = false;
                        }
                        else
                        {
                            Employee employee;
                            employee = db.Employees.Where(s => s.EmployeeID == person.EmployeeID).FirstOrDefault<Employee>();
                            employee.daysSecondCall = employee.daysSecondCall + 1;
                            person.daysSecondCall = person.daysSecondCall + 1;
                            db.Entry(employee).State = EntityState.Modified;
                            db.SaveChanges();
                            startDate = startDate.AddDays(7);
                            prim_or_sec = true;

                        }
                    }

                  }
                }
                if (next == true)
                {
                    next = false;
                }
                else
                {
                    ++leastShift;
                    next = true;
                }
        }
    }

        public bool checkTimeOff(DateTime startDate, Employee person, bool type)
    {
        bool hasTimeOff = false;
        //TimeSpan  diff1;
       // TimeSpan diff2;
        DateTime endShift;
        bool mgr = false;
        if (mgr == true)
            endShift = startDate.AddDays(6);
        else
            endShift = startDate.AddDays(13);
         foreach (TimeOff var in db.TimeOffs)
         {
         /*    if (person.EmployeeID == var.EmployeeID && var.FirstDay >= startDate && var.FirstDay <= endShift) {
                 hasTimeOff = true;
                 goto loop;
             }

             if (person.EmployeeID == var.EmployeeID && startDate >= var.FirstDay && endShift <= var.LastDay)
             {
                 hasTimeOff = true;
                 goto loop;
             }*/
           /*  if (person.EmployeeID == var.EmployeeID && startDate <= var.LastDay && startDate >= var.FirstDay)
             {
                 hasTimeOff = true;
                 break;
             }*/
             if (person.EmployeeID == var.EmployeeID && startDate <= var.LastDay && startDate >= var.FirstDay)
             {
                 hasTimeOff = true;
                 goto loop;
             }else if(person.EmployeeID == var.EmployeeID && var.FirstDay >= startDate && var.FirstDay <= endShift)
             {
                 hasTimeOff = true;
                 goto loop;
             }
         }
            loop:
            return hasTimeOff;
           
    }
         
   }
    
}
