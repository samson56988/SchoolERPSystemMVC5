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
    public class FeeStructuresController : Controller
    {
        private SchoolErpSystemEntities db = new SchoolErpSystemEntities();

        // GET: FeeStructures
        public ActionResult Index()
        {
            var feeStructures = db.FeeStructures.Include(f => f.ClassLevel).Include(f => f.ClassType).Include(f => f.FeeType);
            return View(feeStructures.ToList());
        }

        // GET: FeeStructures/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            FeeStructure feeStructure = db.FeeStructures.Find(id);
            if (feeStructure == null)
            {
                return HttpNotFound();
            }
            return View(feeStructure);
        }

        // GET: FeeStructures/Create
        public ActionResult Create()
        {
            ViewBag.LevelID = new SelectList(db.ClassLevels, "LevelID", "Levelname");
            ViewBag.SectionID = new SelectList(db.ClassTypes, "ClassTypeID", "ClassType1");
            ViewBag.FeeTypeID = new SelectList(db.FeeTypes, "FeeTypeID", "Fee");
            return View();
        }

        // POST: FeeStructures/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "FeeID,SectionID,LevelID,FeeTypeID,Amount")] FeeStructure feeStructure)
        {
            if (ModelState.IsValid)
            {
                db.FeeStructures.Add(feeStructure);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.LevelID = new SelectList(db.ClassLevels, "LevelID", "Levelname", feeStructure.LevelID);
            ViewBag.SectionID = new SelectList(db.ClassTypes, "ClassTypeID", "ClassType1", feeStructure.SectionID);
            ViewBag.FeeTypeID = new SelectList(db.FeeTypes, "FeeTypeID", "Fee", feeStructure.FeeTypeID);
            return View(feeStructure);
        }

        // GET: FeeStructures/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            FeeStructure feeStructure = db.FeeStructures.Find(id);
            if (feeStructure == null)
            {
                return HttpNotFound();
            }
            ViewBag.LevelID = new SelectList(db.ClassLevels, "LevelID", "Levelname", feeStructure.LevelID);
            ViewBag.SectionID = new SelectList(db.ClassTypes, "ClassTypeID", "ClassType1", feeStructure.SectionID);
            ViewBag.FeeTypeID = new SelectList(db.FeeTypes, "FeeTypeID", "Fee", feeStructure.FeeTypeID);
            return View(feeStructure);
        }

        // POST: FeeStructures/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "FeeID,SectionID,LevelID,FeeTypeID,Amount")] FeeStructure feeStructure)
        {
            if (ModelState.IsValid)
            {
                db.Entry(feeStructure).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.LevelID = new SelectList(db.ClassLevels, "LevelID", "Levelname", feeStructure.LevelID);
            ViewBag.SectionID = new SelectList(db.ClassTypes, "ClassTypeID", "ClassType1", feeStructure.SectionID);
            ViewBag.FeeTypeID = new SelectList(db.FeeTypes, "FeeTypeID", "Fee", feeStructure.FeeTypeID);
            return View(feeStructure);
        }

        // GET: FeeStructures/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            FeeStructure feeStructure = db.FeeStructures.Find(id);
            if (feeStructure == null)
            {
                return HttpNotFound();
            }
            return View(feeStructure);
        }

        // POST: FeeStructures/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            FeeStructure feeStructure = db.FeeStructures.Find(id);
            db.FeeStructures.Remove(feeStructure);
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
