using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using CommerceApp.MockClass;
using CommerceApp.Models;
using CommerceApp;
using Newtonsoft.Json;
using Rotativa;

namespace CommerceApp.Controllers
{
    public class ScheduleController : Controller
    {
        private EmployeeDBContext db = new EmployeeDBContext();
        int count = 0;
        
        // GET: Schedule
        public ActionResult Calendar()
        {
            return View();
        }

        // GET: Pdf
        public ActionResult ExportPdf()
        {
          //  var s = new Shift();
          //  var calendarList = s.getShifts(1).ToList();
            return new ActionAsPdf("Pdf")
            {

                PageSize = Rotativa.Options.Size.A4,
                PageOrientation = Rotativa.Options.Orientation.Landscape,
                PageMargins = { Left = 1, Right = 1 }
            };
            //          return new RazorPDF.PdfResult(calendarList.ToList());
            //         return View(calendarList);
        }
        public ActionResult Pdf()
        {
            return View();
        }   
public JsonResult GetCalendarEvents(double start, double end)
        {
          
                var newSchedule = new GenerateSchedule();
                var scheduleDetails = newSchedule.GenerateOLB();
            
                var calendarList = from item in scheduleDetails
                                   select new
                                   {
                                       id = item.ID,
                                       title = item.Title,
                                       start = item.Start.ToString("s"),
                                       end = item.End.ToString("s"),
                                       color = item.color,
                                       editable = true
                                   };
            return Json(calendarList.ToArray(),JsonRequestBehavior.AllowGet);
        }

        public ActionResult Delete()
        {
            foreach (var shift in db.Shifts)
            {
                db.Shifts.Remove(shift);
            }
            db.SaveChanges();
            return RedirectToAction("Index", "Home");
        }
    }
}