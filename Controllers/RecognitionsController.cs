using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using Team6New_MIS4200.DAL;
using Team6New_MIS4200.Models;

namespace Team6New_MIS4200.Controllers
{
    public class RecognitionsController : Controller
    {
        private MIS4200Context db = new MIS4200Context();

        public Guid Nominee { get; private set; }

        // GET: Recognitions

        public ActionResult Index()
        {
            var rec = db.Recognition;
            var recList = rec.ToList();
            ViewBag.rec = recList;
            var totalCnt = recList.Count();
            ViewBag.total = totalCnt;
           

            var recognition = db.Recognition.Include(r => r.Nominations).Include(r => r.recognition);
            return View(recognition.ToList());
        }

        // GET: Recognitions/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Recognition recognition = db.Recognition.Find(id);
            if (recognition == null)
            {
                return HttpNotFound();
            }
            return View(recognition);
        }

        // GET: Recognitions/Create
        [Authorize]
        public ActionResult Create()
        {
            ViewBag.recognizor = new SelectList(db.Employees, "ID", "fullName");
            ViewBag.Nominee = new SelectList(db.Employees, "ID", "fullName");
            return View();
        }

        // POST: Recognitions/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "surveyID,award,recognizor,Nominee,recognizationDate")] Recognition recognition)
        {
            if (ModelState.IsValid)
            {
                db.Recognition.Add(recognition);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.recognizor = new SelectList(db.Employees, "ID", "Email", recognition.recognizor);
            ViewBag.Nominee = new SelectList(db.Employees, "ID", "Email", recognition.Nominee);
            return View(recognition);
        }

        // GET: Recognitions/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Recognition recognition = db.Recognition.Find(id);
            if (recognition == null)
            {
                return HttpNotFound();
            }
            ViewBag.recognizor = new SelectList(db.Employees, "ID", "Email", recognition.recognizor);
            ViewBag.Nominee = new SelectList(db.Employees, "ID", "Email", recognition.Nominee);
            return View(recognition);
        }

        // POST: Recognitions/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "surveyID,award,recognizor,Nominee,recognizationDate")] Recognition recognition)
        {
            if (ModelState.IsValid)
            {
                db.Entry(recognition).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.recognizor = new SelectList(db.Employees, "ID", "Email", recognition.recognizor);
            ViewBag.Nominee = new SelectList(db.Employees, "ID", "Email", recognition.Nominee);
            return View(recognition);
        }

        // GET: Recognitions/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Recognition recognition = db.Recognition.Find(id);
            if (recognition == null)
            {
                return HttpNotFound();
            }
            return View(recognition);
        }

        // POST: Recognitions/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Recognition recognition = db.Recognition.Find(id);
            db.Recognition.Remove(recognition);
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
