using System;
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
    public class ClassTypesController : Controller
    {
        private SchoolErpSystemEntities db = new SchoolErpSystemEntities();

        // GET: ClassTypes
        public ActionResult Index()
        {
            return View(db.ClassTypes.ToList());
        }

        // GET: ClassTypes/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ClassType classType = db.ClassTypes.Find(id);
            if (classType == null)
            {
                return HttpNotFound();
            }
            return View(classType);
        }

        // GET: ClassTypes/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: ClassTypes/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "ClassTypeID,ClassType1")] ClassType classType)
        {
            if (ModelState.IsValid)
            {
                db.ClassTypes.Add(classType);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(classType);
        }

        // GET: ClassTypes/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ClassType classType = db.ClassTypes.Find(id);
            if (classType == null)
            {
                return HttpNotFound();
            }
            return View(classType);
        }

        // POST: ClassTypes/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "ClassTypeID,ClassType1")] ClassType classType)
        {
            if (ModelState.IsValid)
            {
                db.Entry(classType).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(classType);
        }

        // GET: ClassTypes/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ClassType classType = db.ClassTypes.Find(id);
            if (classType == null)
            {
                return HttpNotFound();
            }
            return View(classType);
        }

        // POST: ClassTypes/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            ClassType classType = db.ClassTypes.Find(id);
            db.ClassTypes.Remove(classType);
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
