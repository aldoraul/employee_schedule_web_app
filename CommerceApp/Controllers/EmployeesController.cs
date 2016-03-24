﻿using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using CommerceApp.Models;

namespace CommerceApp.Controllers
{
    public class EmployeesController : Controller
    {
        private EmployeeDBContext db = new EmployeeDBContext();

        // GET: Employees
        public ActionResult Index(string sortOrder, string searchString)
        {
            ViewBag.fNameSortParm = sortOrder == "fname" ? "fname_desc": "fname";
            ViewBag.lNameSortParm = sortOrder == "lname" ? "lname_desc" : "lname";
            ViewBag.jobSortParm = sortOrder == "job_type" ? "job_type_desc" : "job_type";
            var employee = from s in db.Employees
                           select s;
            if (!String.IsNullOrEmpty(searchString))
            {
                employee = employee.Where(s => s.lastName.Contains(searchString)
                                       || s.firstName.Contains(searchString)
                                       || s.jobTitle.Contains(searchString));
            }
            switch (sortOrder)
            {
                case "fName_desc":
                    employee = employee.OrderByDescending(s => s.firstName);
                    break;
                case "fname":
                    employee = employee.OrderBy(s => s.firstName);
                    break;
                case "lname":
                    employee = employee.OrderBy(s => s.lastName);
                    break;
                case "lname_desc":
                    employee = employee.OrderByDescending(s => s.lastName);
                    break;
                case "job_type":
                    employee = employee.OrderBy(s => s.jobTitle);
                    break;
                case "job_type_desc":
                    employee = employee.OrderByDescending(s => s.jobTitle);
                    break;
                default:
                    employee = employee.OrderBy(s => s.jobTitle);
                    break;
            }
            return View(employee.ToList());
        }

        // GET: Employees/Details/5
        public ActionResult Details(int id)
        {
            if (id < 0)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Employee employee = db.Employees.Find(id);
            if (employee == null)
            {
                return HttpNotFound();
            }
            return View(employee);
        }


        // GET: Employees/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Employees/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "EmployeeID,firstName,lastName,jobTitle,hireDate,daysFirstCall,daysSecondCall,Email")] Employee employee)
        {
            if (ModelState.IsValid)
            {
                db.Employees.Add(employee);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(employee);
        }

        // GET: Employees/Edit/5
        public ActionResult Edit(int id)
        {
            if (id < 1)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Employee employee = db.Employees.Find(id);
            if (employee == null)
            {
                return HttpNotFound();
            }
            return View(employee);
        }

        // POST: Employees/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "EmployeeID,firstName,lastName,jobTitle,hireDate,daysFirstCall,daysSecondCall,Email")] Employee employee)
        {
            if (ModelState.IsValid)
            {
                db.Entry(employee).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(employee);
        }

        // GET: Employees/Delete/5
        public ActionResult Delete(int id)
        {
            if (id < 1)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Employee employee = db.Employees.Find(id);
            if (employee == null)
            {
                return HttpNotFound();
            }
            return View(employee);
        }

        // POST: Employees/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Employee employee = db.Employees.Find(id);
            db.Employees.Remove(employee);
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
