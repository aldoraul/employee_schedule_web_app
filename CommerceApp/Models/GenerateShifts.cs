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

        bool prim_or_sec = true;
        
       
        public void Generate_OLB_Shifts(List<Employee> olbTeam)
        {
            DateTime startDate = new DateTime(2015, 1, 7);
            DateTime endDate = new DateTime(2016, 1, 5);
            //int count = 0;

            while (startDate < endDate)
            {
                foreach (Employee person in olbTeam)
                {
                    Shift again = new Shift();

                    again.ShiftID = shift_id++;
                    again.EmployeeID = person.EmployeeID;

                    again.ShiftDate = startDate;//.AddDays(count);
                    again.ShiftPrimary = prim_or_sec;

                    db.Entry(again).State = EntityState.Added;
                    db.SaveChanges();

                    if (prim_or_sec == true)
                    {                           // switch back from primary to secondary
                        prim_or_sec = false;
                    }
                    else
                    {
                        startDate = startDate.AddDays(7);
                        prim_or_sec = true;
                    }

                }

            }
        }
    public void Generate_MGR_Shifts(List<Employee> mgrTeam){
     //   int count = 0;
        DateTime startDate = new DateTime(2015, 1, 7);
        DateTime endDate = new DateTime(2016, 1, 5);
        while (startDate < endDate)
        {
            foreach (Employee person in mgrTeam)
            {
                Shift newShift = new Shift();
                newShift.ShiftID = shift_id++;
                newShift.EmployeeID = person.EmployeeID;
                newShift.ShiftDate = startDate;//.AddDays(count);
                //newShift.ShiftPrimary = prim_or_sec;

                db.Entry(newShift).State = EntityState.Added;
                db.SaveChanges();
                startDate = startDate.AddDays(14);
            }

        }
    
    }
    public void Generate_HSF_Shifts(List<Employee> hsfTeam)
    {
        DateTime startDate = new DateTime(2015, 1, 7);
        DateTime endDate = new DateTime(2016, 1, 5);
        //int count = 0;

        while (startDate < endDate)
        {
            foreach (Employee person in hsfTeam)
            {
                Shift again = new Shift();

                again.ShiftID = shift_id++;
                again.EmployeeID = person.EmployeeID;

                again.ShiftDate = startDate;//.AddDays(count);
                again.ShiftPrimary = prim_or_sec;

                db.Entry(again).State = EntityState.Added;
                db.SaveChanges();

                if (prim_or_sec == true)
                {                           // switch back from primary to secondary
                    prim_or_sec = false;
                }
                else
                {
                    startDate = startDate.AddDays(7);
                    prim_or_sec = true;
                }

            }

        }
    }
         
   }
    
}
