using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using CommerceApp.Models;
using CommerceApp.MockClass;

namespace CommerceApp.Controllers
{
    public class TimeOffsController : Controller
    {
        private EmployeeDBContext db = new EmployeeDBContext();

        // GET: TimeOffs
        public ActionResult Index()
        {
            var timeOffs = db.TimeOffs.Include(t => t.Employee);
            return View(timeOffs.ToList());
        }

        // GET: TimeOffs/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            TimeOff timeOff = db.TimeOffs.Find(id);
            if (timeOff == null)
            {
                return HttpNotFound();
            }
            return View(timeOff);
        }

        // GET: TimeOffs/Create
        public ActionResult Create()
        {
            ViewBag.EmployeeID = new SelectList(db.Employees, "EmployeeID", "firstName");
            return View();
        }

        // POST: TimeOffs/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "TimeOffID,EmployeeID,FirstDay,LastDay,TimeOffType")] TimeOff timeOff)
        {
            if (ModelState.IsValid)
            {
                db.TimeOffs.Add(timeOff);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.EmployeeID = new SelectList(db.Employees, "EmployeeID", "firstName", timeOff.EmployeeID);
            return View(timeOff);
        }

        // GET: TimeOffs/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            TimeOff timeOff = db.TimeOffs.Find(id);
            if (timeOff == null)
            {
                return HttpNotFound();
            }
            ViewBag.EmployeeID = new SelectList(db.Employees, "EmployeeID", "firstName", timeOff.EmployeeID);
            return View(timeOff);
        }

        // POST: TimeOffs/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "TimeOffID,EmployeeID,FirstDay,LastDay,TimeOffType")] TimeOff timeOff)
        {
            if (ModelState.IsValid)
            {
                db.Entry(timeOff).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.EmployeeID = new SelectList(db.Employees, "EmployeeID", "firstName", timeOff.EmployeeID);
            return View(timeOff);
        }

        // GET: TimeOffs/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            TimeOff timeOff = db.TimeOffs.Find(id);
            if (timeOff == null)
            {
                return HttpNotFound();
            }
            return View(timeOff);
        }

        // POST: TimeOffs/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            TimeOff timeOff = db.TimeOffs.Find(id);
            db.TimeOffs.Remove(timeOff);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
