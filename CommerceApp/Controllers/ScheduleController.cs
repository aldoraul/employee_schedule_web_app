using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using CommerceApp.MockClass;
using CommerceApp.Models;
using CommerceApp;

namespace CommerceApp.Controllers
{
    public class ScheduleController : Controller
    {
        // GET: Schedule
        public ActionResult Calendar()
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
    }
}