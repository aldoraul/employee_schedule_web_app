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
        //int count = 0;
        CalendarDate start_end = new CalendarDate();
        // GET: Schedule
        public ActionResult Calendar()
        {
            return View();
        }
        
        public JsonResult GetCalendarEvents(double start, double end)
        {

            var newSchedule = new GenerateSchedule();
            var OLBDetails = newSchedule.GenerateOLB();
            var AllDetails = OLBDetails;

            var Allcalendar = from item in AllDetails
                              select new
                              {
                                  id = item.ID,
                                  title = item.Title,
                                  start = item.Start.ToString("s"),
                                  end = item.End.ToString("s"),
                                  color = item.color,
                                  editable = true
                              };

            return Json(Allcalendar.ToArray(), JsonRequestBehavior.AllowGet);
        }
    
        public ActionResult CalendarDate()
        {
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult CalendarDate([Bind(Include = "StartDate,EndDate")] CalendarDate start)
        {
            if (ModelState.IsValid)
            {
                start_end = start; 
                return RedirectToAction("Calendar", start);
            }
            return View(start_end);
        }
        // GET: Pdf
        public ActionResult ExportPdf(GeneratePDF config)
        {
            return new ActionAsPdf("Pdf", config)
            {

                PageSize = Rotativa.Options.Size.A4,
                PageOrientation = Rotativa.Options.Orientation.Landscape,
                PageMargins = { Left = 1, Right = 1 }
            };

        }
        public ActionResult Pdf(GeneratePDF config)
        {
            return View(config);
        }
        public ActionResult ConfigurePDF()
        {
            return View();
        }
        // POST: Schedule/ConfigurePDF
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult ConfigurePDF([Bind(Include = "numberOfMonths,startDate")] GeneratePDF config)
        {
            if (ModelState.IsValid)
            {
                return RedirectToAction("ExportPDF", config);
            }

            return View(config);
        }
       

        public ActionResult Delete()
        {
            foreach (var shift in db.Shifts)
            {
                db.Shifts.Remove(shift);
            }
            foreach (var employee in db.Employees)
            {
                employee.daysFirstCall = 0;
                employee.daysSecondCall = 0;
            }
            db.SaveChanges();
            return RedirectToAction("Index", "Home");
        }
    }
}