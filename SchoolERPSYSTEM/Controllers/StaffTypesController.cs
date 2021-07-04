﻿using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using SchoolERPSYSTEM.Models;

namespace SchoolERPSYSTEM.Controllers
{
    public class StaffTypesController : Controller
    {
        private SchoolErpSystemEntities db = new SchoolErpSystemEntities();

        // GET: StaffTypes
        public ActionResult Index()
        {
            return View(db.StaffTypes.ToList());
        }

        // GET: StaffTypes/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            StaffType staffType = db.StaffTypes.Find(id);
            if (staffType == null)
            {
                return HttpNotFound();
            }
            return View(staffType);
        }

        // GET: StaffTypes/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: StaffTypes/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "StaffTypeID,StaffType1")] StaffType staffType)
        {
            if (ModelState.IsValid)
            {
                db.StaffTypes.Add(staffType);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(staffType);
        }

        // GET: StaffTypes/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            StaffType staffType = db.StaffTypes.Find(id);
            if (staffType == null)
            {
                return HttpNotFound();
            }
            return View(staffType);
        }

        // POST: StaffTypes/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "StaffTypeID,StaffType1")] StaffType staffType)
        {
            if (ModelState.IsValid)
            {
                db.Entry(staffType).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(staffType);
        }

        // GET: StaffTypes/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            StaffType staffType = db.StaffTypes.Find(id);
            if (staffType == null)
            {
                return HttpNotFound();
            }
            return View(staffType);
        }

        // POST: StaffTypes/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            StaffType staffType = db.StaffTypes.Find(id);
            db.StaffTypes.Remove(staffType);
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
