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
    public class StudentTblesController : Controller
    {
        private SchoolErpSystemEntities db = new SchoolErpSystemEntities();

        // GET: StudentTbles
        public ActionResult Index()
        {
            var studentTbles = db.StudentTbles.Include(s => s.ClassLevel1).Include(s => s.ClassType).Include(s => s.tblCountry);
            return View(studentTbles.ToList());
        }

        // GET: StudentTbles/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            StudentTble studentTble = db.StudentTbles.Find(id);
            if (studentTble == null)
            {
                return HttpNotFound();
            }
            return View(studentTble);
        }

        // GET: StudentTbles/Create
        public ActionResult Create(int id = 0)
        {
            StudentTble tb = new StudentTble();
            var lastStudent = db.StudentTbles.OrderByDescending(c => c.ID).FirstOrDefault();
            if (id != 0)
            {
                tb = db.StudentTbles.Where(x => x.ID == id).FirstOrDefault<StudentTble>();
            }
            else if (lastStudent == null)
            {
                tb.AdmissionNo = "ADMSN-ID001";
            }
            else
            {
                tb.AdmissionNo = "ADMSN-ID" + (Convert.ToInt32(lastStudent.AdmissionNo.Substring(9, lastStudent.AdmissionNo.Length - 9) + 1)).ToString("D3");
            }
            ViewBag.ClassLevel = new SelectList(db.ClassLevels, "LevelID", "Levelname");
            ViewBag.Section = new SelectList(db.ClassTypes, "ClassTypeID", "ClassType1");
            ViewBag.Nationality = new SelectList(db.tblCountries, "CID", "Cname");
            return View();
        }

        // POST: StudentTbles/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "ID,AdmissionNo,Firstname,middlename,Lastname,DOB,Nationality,LocalGov,Gender,Religion,Section,ClassLevel,ClassAlphabet,AdmissionDate,Fathername,FatherOccupation,Mothersname,Mothersoccupation,Fatherno,Fathersemail,Mothersno,mothersEmail,StudentNo,StudentEmail,Address")] StudentTble studentTble)
        {
            if (ModelState.IsValid)
            {
                db.StudentTbles.Add(studentTble);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.ClassLevel = new SelectList(db.ClassLevels, "LevelID", "Levelname", studentTble.ClassLevel);
            ViewBag.Section = new SelectList(db.ClassTypes, "ClassTypeID", "ClassType1", studentTble.Section);
            ViewBag.Nationality = new SelectList(db.tblCountries, "CID", "Cname", studentTble.Nationality);
            TempData["SuccessMessage"] = "Save Successfully";
            return View(studentTble);
        }

        // GET: StudentTbles/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            StudentTble studentTble = db.StudentTbles.Find(id);
            if (studentTble == null)
            {
                return HttpNotFound();
            }
            ViewBag.ClassLevel = new SelectList(db.ClassLevels, "LevelID", "Levelname", studentTble.ClassLevel);
            ViewBag.Section = new SelectList(db.ClassTypes, "ClassTypeID", "ClassType1", studentTble.Section);
            ViewBag.Nationality = new SelectList(db.tblCountries, "CID", "Cname", studentTble.Nationality);
            return View(studentTble);
        }

        // POST: StudentTbles/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "ID,AdmissionNo,Firstname,middlename,Lastname,DOB,Nationality,LocalGov,Gender,Religion,Section,ClassLevel,ClassAlphabet,AdmissionDate,Fathername,FatherOccupation,Mothersname,Mothersoccupation,Fatherno,Fathersemail,Mothersno,mothersEmail,StudentNo,StudentEmail,Address")] StudentTble studentTble)
        {
            if (ModelState.IsValid)
            {
                db.Entry(studentTble).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.ClassLevel = new SelectList(db.ClassLevels, "LevelID", "Levelname", studentTble.ClassLevel);
            ViewBag.Section = new SelectList(db.ClassTypes, "ClassTypeID", "ClassType1", studentTble.Section);
            ViewBag.Nationality = new SelectList(db.tblCountries, "CID", "Cname", studentTble.Nationality);
            return View(studentTble);
        }

        // GET: StudentTbles/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            StudentTble studentTble = db.StudentTbles.Find(id);
            if (studentTble == null)
            {
                return HttpNotFound();
            }
            return View(studentTble);
        }

        // POST: StudentTbles/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            StudentTble studentTble = db.StudentTbles.Find(id);
            db.StudentTbles.Remove(studentTble);
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
