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
    public class ClassLevelsController : Controller
    {
        private SchoolErpSystemEntities db = new SchoolErpSystemEntities();

        // GET: ClassLevels
        public ActionResult Index()
        {
            var classLevels = db.ClassLevels.Include(c => c.ClassType);
            return View(classLevels.ToList());
        }

        // GET: ClassLevels/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ClassLevel classLevel = db.ClassLevels.Find(id);
            if (classLevel == null)
            {
                return HttpNotFound();
            }
            return View(classLevel);
        }

        // GET: ClassLevels/Create
        public ActionResult Create()
        {
            ViewBag.Section = new SelectList(db.ClassTypes, "ClassTypeID", "ClassType1");
            return View();
        }

        // POST: ClassLevels/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "LevelID,Levelname,Section")] ClassLevel classLevel)
        {
            if (ModelState.IsValid)
            {
                db.ClassLevels.Add(classLevel);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.Section = new SelectList(db.ClassTypes, "ClassTypeID", "ClassType1", classLevel.Section);
            return View(classLevel);
        }

        // GET: ClassLevels/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ClassLevel classLevel = db.ClassLevels.Find(id);
            if (classLevel == null)
            {
                return HttpNotFound();
            }
            ViewBag.Section = new SelectList(db.ClassTypes, "ClassTypeID", "ClassType1", classLevel.Section);
            return View(classLevel);
        }

        // POST: ClassLevels/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "LevelID,Levelname,Section")] ClassLevel classLevel)
        {
            if (ModelState.IsValid)
            {
                db.Entry(classLevel).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.Section = new SelectList(db.ClassTypes, "ClassTypeID", "ClassType1", classLevel.Section);
            return View(classLevel);
        }

        // GET: ClassLevels/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ClassLevel classLevel = db.ClassLevels.Find(id);
            if (classLevel == null)
            {
                return HttpNotFound();
            }
            return View(classLevel);
        }

        // POST: ClassLevels/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            ClassLevel classLevel = db.ClassLevels.Find(id);
            db.ClassLevels.Remove(classLevel);
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
