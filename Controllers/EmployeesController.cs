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
using Microsoft.AspNet.Identity;
using System.IO;

namespace Team6New_MIS4200.Controllers
{
    public class EmployeesController : Controller
    {
        private MIS4200Context db = new MIS4200Context();

        // GET: Employees
        [Authorize]
        public ActionResult Index()
        {
            return View(db.Employees.ToList());
        }

        // GET: Employees/Details/5
        public ActionResult Details(Guid? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Employees employees = db.Employees.Find(id);
            if (employees == null)
            {
                return HttpNotFound();
            }

            var rec = db.Recognition.Where(r => r.Nominee == id).OrderBy(a => a.recognizationDate);

            var recList = rec.ToList();
            ViewBag.rec = recList;

            var totalCnt = recList.Count();
            var rec1Cnt = recList.Where(r => r.award == Recognition.CoreValue.Excellence).Count();
            var rec2Cnt = recList.Where(r => r.award == Recognition.CoreValue.Integrity).Count();
            var rec3Cnt = recList.Where(r => r.award == Recognition.CoreValue.Balanced).Count();
            var rec4Cnt = recList.Where(r => r.award == Recognition.CoreValue.Culture).Count();
            var rec5Cnt = recList.Where(r => r.award == Recognition.CoreValue.Innovate).Count();
            var rec6Cnt = recList.Where(r => r.award == Recognition.CoreValue.Passion).Count();
            var rec7Cnt = recList.Where(r => r.award == Recognition.CoreValue.Stewardship).Count();

            //copy values into viewbag
            ViewBag.total = totalCnt;
            ViewBag.Excellence = rec1Cnt;
            ViewBag.Integrity = rec2Cnt;
            ViewBag.Balanced = rec3Cnt;
            ViewBag.Culture = rec4Cnt;
            ViewBag.Innovate = rec5Cnt;
            ViewBag.Passion = rec6Cnt;
            ViewBag.Stewardship = rec7Cnt;



            return View(employees);
        }

        // GET: Employees/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Employees/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "ID,Email,firstName,lastName,PhoneNumber,Office,Position,hireDate,photo")] Employees employees)
        {
            //  if (!ModelState.IsValid) - this is a temporary work around 
            if (true)
            {
                Guid ID;
                Guid.TryParse(User.Identity.GetUserId(), out ID);
                employees.ID = ID;
                HttpPostedFileBase file = Request.Files["UploadedImage"]; //(A) – see notes below
                if (file != null && file.FileName != null && file.FileName != "") //(B)
                {
                    FileInfo fi = new FileInfo(file.FileName); //(C)
                    if (fi.Extension != ".jpeg" && fi.Extension != ".jpg" && fi.Extension != "gif" && fi.Extension != "png") //(D)
                    {
                        TempData["Errormsg"] = "Image File Extension is not valid"; //(E)
                        return View(employees);
                    }
                    else
                    {
                        // this saves the file as the user’s ID and file extension        
                        employees.photo = employees.ID + fi.Extension; //(F)
                        file.SaveAs(Server.MapPath("~/Images/" + employees.photo));  //(G)
                    }

                }

                db.Employees.Add(employees);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(employees);
        }
    

        // GET: Employees/Edit/5
        public ActionResult Edit(Guid? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Employees employees = db.Employees.Find(id);
            if (employees == null)
            {
                return HttpNotFound();
            }
            return View(employees);
        }

        // POST: Employees/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "ID,Email,firstName,lastName,PhoneNumber,Office,Position,hireDate,photo")] Employees employees)
        {
            if (ModelState.IsValid)
            {
                db.Entry(employees).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(employees);
        }

        // GET: Employees/Delete/5
        public ActionResult Delete(Guid? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Employees employees = db.Employees.Find(id);
            if (employees == null)
            {
                return HttpNotFound();
            }
            return View(employees);
        }

        // POST: Employees/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(Guid id)
        {
            Employees employees = db.Employees.Find(id);
            db.Employees.Remove(employees);
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
