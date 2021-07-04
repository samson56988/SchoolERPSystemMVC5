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
    public class TblSessionsController : Controller
    {
        private SchoolErpSystemEntities db = new SchoolErpSystemEntities();

        // GET: TblSessions
        public ActionResult Index()
        {
            return View(db.TblSessions.ToList());
        }

        // GET: TblSessions/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            TblSession tblSession = db.TblSessions.Find(id);
            if (tblSession == null)
            {
                return HttpNotFound();
            }
            return View(tblSession);
        }

        // GET: TblSessions/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: TblSessions/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "SessionID,Session,Term,IsActive")] TblSession tblSession)
        {
            if (ModelState.IsValid)
            {
                db.TblSessions.Add(tblSession);
                db.SaveChanges();
                TempData["SuccessMessage"] = "Save Successfully";
                return RedirectToAction("Index");
            }

            return View(tblSession);
        }

        // GET: TblSessions/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            TblSession tblSession = db.TblSessions.Find(id);
            if (tblSession == null)
            {
                return HttpNotFound();
            }
            return View(tblSession);
        }

        // POST: TblSessions/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "SessionID,Session,Term,IsActive")] TblSession tblSession)
        {
            if (ModelState.IsValid)
            {
                db.Entry(tblSession).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(tblSession);
        }

        // GET: TblSessions/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            TblSession tblSession = db.TblSessions.Find(id);
            if (tblSession == null)
            {
                return HttpNotFound();
            }
            return View(tblSession);
        }

        // POST: TblSessions/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            TblSession tblSession = db.TblSessions.Find(id);
            db.TblSessions.Remove(tblSession);
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
