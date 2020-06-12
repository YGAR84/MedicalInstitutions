﻿//using System;
//using System.Collections.Generic;
//using System.Data;
//using System.Data.Entity;
//using System.Linq;
//using System.Threading.Tasks;
//using System.Net;
//using System.Web;
//using System.Web.Mvc;
//using MedicalInstitutions.Models;
//using MedicalInstitutions.Models.Staffs.MedicalStaffs;

//namespace MedicalInstitutions.Controllers
//{
//    public class OculistsController : Controller
//    {
//        private MedicalInstitutionsContext db = new MedicalInstitutionsContext();

//        // GET: Oculists
//        public async Task<ActionResult> Index()
//        {
//            var medicalStaffs = db.Oculists.Include(o => o.Clinic).Include(o => o.HospitalDepartment);
//            return View(await medicalStaffs.ToListAsync());
//        }

//        // GET: Oculists/Details/5
//        public async Task<ActionResult> Details(int? id)
//        {
//            if (id == null)
//            {
//                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
//            }
//            Oculist oculist = await db.Oculists.FindAsync(id);
//            if (oculist == null)
//            {
//                return HttpNotFound();
//            }
//            return View(oculist);
//        }

//        // GET: Oculists/Create
//        public ActionResult Create()
//        {
//            ViewBag.ClinicId = new SelectList(db.Clinics, "Id", "MedicalInstitutionName");
//            ViewBag.HospitalDepartmentId = new SelectList(db.HospitalDepartments, "Id", "Id");
//            return View();
//        }

//        // POST: Oculists/Create
//        // Чтобы защититься от атак чрезмерной передачи данных, включите определенные свойства, для которых следует установить привязку. Дополнительные 
//        // сведения см. в статье https://go.microsoft.com/fwlink/?LinkId=317598.
//        [HttpPost]
//        [ValidateAntiForgeryToken]
//        public async Task<ActionResult> Create([Bind(Include = "Id,Degree,HospitalDepartmentId,ClinicId,Salary,Vacation,EmploymentDate,SalaryAddition,VacationAddition,FirstName,SecondName")] Oculist oculist)
//        {
//            if (ModelState.IsValid)
//            {
//                db.Oculists.Add(oculist);
//                await db.SaveChangesAsync();
//                return RedirectToAction("Index");
//            }

//            ViewBag.ClinicId = new SelectList(db.Clinics, "Id", "MedicalInstitutionName", oculist.ClinicId);
//            ViewBag.HospitalDepartmentId = new SelectList(db.HospitalDepartments, "Id", "Id", oculist.HospitalDepartmentId);
//            return View(oculist);
//        }

//        // GET: Oculists/Edit/5
//        public async Task<ActionResult> Edit(int? id)
//        {
//            if (id == null)
//            {
//                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
//            }
//            Oculist oculist = await db.Oculists.FindAsync(id);
//            if (oculist == null)
//            {
//                return HttpNotFound();
//            }
//            ViewBag.ClinicId = new SelectList(db.Clinics, "Id", "MedicalInstitutionName", oculist.ClinicId);
//            ViewBag.HospitalDepartmentId = new SelectList(db.HospitalDepartments, "Id", "Id", oculist.HospitalDepartmentId);
//            return View(oculist);
//        }

//        // POST: Oculists/Edit/5
//        // Чтобы защититься от атак чрезмерной передачи данных, включите определенные свойства, для которых следует установить привязку. Дополнительные 
//        // сведения см. в статье https://go.microsoft.com/fwlink/?LinkId=317598.
//        [HttpPost]
//        [ValidateAntiForgeryToken]
//        public async Task<ActionResult> Edit([Bind(Include = "Id,Degree,HospitalDepartmentId,ClinicId,Salary,Vacation,EmploymentDate,SalaryAddition,VacationAddition,FirstName,SecondName")] Oculist oculist)
//        {
//            if (ModelState.IsValid)
//            {
//                db.Entry(oculist).State = EntityState.Modified;
//                await db.SaveChangesAsync();
//                return RedirectToAction("Index");
//            }
//            ViewBag.ClinicId = new SelectList(db.Clinics, "Id", "MedicalInstitutionName", oculist.ClinicId);
//            ViewBag.HospitalDepartmentId = new SelectList(db.HospitalDepartments, "Id", "Id", oculist.HospitalDepartmentId);
//            return View(oculist);
//        }

//        // GET: Oculists/Delete/5
//        public async Task<ActionResult> Delete(int? id)
//        {
//            if (id == null)
//            {
//                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
//            }
//            Oculist oculist = await db.Oculists.FindAsync(id);
//            if (oculist == null)
//            {
//                return HttpNotFound();
//            }
//            return View(oculist);
//        }

//        // POST: Oculists/Delete/5
//        [HttpPost, ActionName("Delete")]
//        [ValidateAntiForgeryToken]
//        public async Task<ActionResult> DeleteConfirmed(int id)
//        {
//            Oculist oculist = await db.Oculists.FindAsync(id);
//            db.Oculists.Remove(oculist);
//            await db.SaveChangesAsync();
//            return RedirectToAction("Index");
//        }

//        protected override void Dispose(bool disposing)
//        {
//            if (disposing)
//            {
//                db.Dispose();
//            }
//            base.Dispose(disposing);
//        }
//    }
//}
