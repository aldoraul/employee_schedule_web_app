﻿using System;
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
    public class ShiftsController : Controller
    {
        private EmployeeDBContext db = new EmployeeDBContext();

        // GET: Shifts
        public ActionResult Index(string sortOrder)
        {
            ViewBag.dateSortParm = sortOrder == "Date" ? "Date_desc": "Date";
            ; ViewBag.nameSortParm = sortOrder == "name" ? "name_desc" : "name";
            var shift = from s in db.Shifts 
                        select s;
            switch (sortOrder)
            {
                case "Date":
                    shift = shift.OrderBy(s => s.ShiftDate);
                    break;
                case "Date_desc":
                    shift = shift.OrderByDescending(s => s.ShiftDate);
                    break;
                case "name":
                    shift = shift.OrderBy(s => s.Employee.lastName);
                    break;
                case "name_desc":
                    shift = shift.OrderByDescending(s => s.Employee.lastName);
                    break;
                default:
                    shift = shift.OrderBy(s => s.ShiftDate);
                    break;
            }
            return View(shift.ToList());
        }
   /* {
            
            var shifts = db.Shifts.Include(s => s.Employee);
            return View(shifts.ToList());
        }
        */
        // GET: Shifts/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Shift shift = db.Shifts.Find(id);
            if (shift == null)
            {
                return HttpNotFound();
            }
            return View(shift);
        }

        // GET: Shifts/Create
        public ActionResult Create()
        {
            ViewBag.EmployeeID = new SelectList(db.Employees, "EmployeeID", "firstName");
            return View();
        }

        // POST: Shifts/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "ShiftID,EmployeeID,ShiftDate,ShiftPrimary")] Shift shift)
        {
            if (ModelState.IsValid)
            {
                db.Shifts.Add(shift);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.EmployeeID = new SelectList(db.Employees, "EmployeeID", "firstName", shift.EmployeeID);
            return View(shift);
        }

        // GET: Shifts/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Shift shift = db.Shifts.Find(id);
            if (shift == null)
            {
                return HttpNotFound();
            }
            ViewBag.EmployeeID = new SelectList(db.Employees, "EmployeeID", "firstName", shift.EmployeeID);
            return View(shift);
        }

        // POST: Shifts/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "ShiftID,EmployeeID,ShiftDate,ShiftPrimary")] Shift shift)
        {
            if (ModelState.IsValid)
            {
                db.Entry(shift).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.EmployeeID = new SelectList(db.Employees, "EmployeeID", "firstName", shift.EmployeeID);
            return View(shift);
        }

        // GET: Shifts/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Shift shift = db.Shifts.Find(id);
            if (shift == null)
            {
                return HttpNotFound();
            }
            return View(shift);
        }

        // POST: Shifts/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Shift shift = db.Shifts.Find(id);
            db.Shifts.Remove(shift);
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
