//using System;
//using System.Collections.Generic;
//using System.Data;
//using System.Data.Entity;
//using System.Linq;
//using System.Threading.Tasks;
//using System.Net;
//using System.Web;
//using System.Web.Mvc;
//using MedicalInstitutions.Models;
//using MedicalInstitutions.Models.Staffs.MedicalStaffs.Surgeons;

//namespace MedicalInstitutions.Controllers
//{
//    public class SurgeonsController : Controller
//    {
//        private MedicalInstitutionsContext db = new MedicalInstitutionsContext();

//        // GET: Surgeons
//        public async Task<ActionResult> Index()
//        {
//            var medicalStaffs = db.Surgeons.Include(s => s.Clinic).Include(s => s.HospitalDepartment);
//            return View(await medicalStaffs.ToListAsync());
//        }

//        // GET: Surgeons/Details/5
//        public async Task<ActionResult> Details(int? id)
//        {
//            if (id == null)
//            {
//                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
//            }
//            Surgeon surgeon = await db.Surgeons.FindAsync(id);
//            if (surgeon == null)
//            {
//                return HttpNotFound();
//            }
//            return View(surgeon);
//        }

//        // GET: Surgeons/Create
//        public ActionResult Create()
//        {
//            ViewBag.ClinicId = new SelectList(db.Clinics, "Id", "MedicalInstitutionName");
//            ViewBag.HospitalDepartmentId = new SelectList(db.HospitalDepartments, "Id", "Id");
//            return View();
//        }

//        // POST: Surgeons/Create
//        // Чтобы защититься от атак чрезмерной передачи данных, включите определенные свойства, для которых следует установить привязку. Дополнительные 
//        // сведения см. в статье https://go.microsoft.com/fwlink/?LinkId=317598.
//        [HttpPost]
//        [ValidateAntiForgeryToken]
//        public async Task<ActionResult> Create([Bind(Include = "Id,Degree,HospitalDepartmentId,ClinicId,Salary,Vacation,EmploymentDate,SalaryAddition,VacationAddition,FirstName,SecondName")] Surgeon surgeon)
//        {
//            if (ModelState.IsValid)
//            {
//                db.Surgeons.Add(surgeon);
//                await db.SaveChangesAsync();
//                return RedirectToAction("Index");
//            }

//            ViewBag.ClinicId = new SelectList(db.Clinics, "Id", "MedicalInstitutionName", surgeon.ClinicId);
//            ViewBag.HospitalDepartmentId = new SelectList(db.HospitalDepartments, "Id", "Id", surgeon.HospitalDepartmentId);
//            return View(surgeon);
//        }

//        // GET: Surgeons/Edit/5
//        public async Task<ActionResult> Edit(int? id)
//        {
//            if (id == null)
//            {
//                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
//            }
//            Surgeon surgeon = await db.Surgeons.FindAsync(id);
//            if (surgeon == null)
//            {
//                return HttpNotFound();
//            }
//            ViewBag.ClinicId = new SelectList(db.Clinics, "Id", "MedicalInstitutionName", surgeon.ClinicId);
//            ViewBag.HospitalDepartmentId = new SelectList(db.HospitalDepartments, "Id", "Id", surgeon.HospitalDepartmentId);
//            return View(surgeon);
//        }

//        // POST: Surgeons/Edit/5
//        // Чтобы защититься от атак чрезмерной передачи данных, включите определенные свойства, для которых следует установить привязку. Дополнительные 
//        // сведения см. в статье https://go.microsoft.com/fwlink/?LinkId=317598.
//        [HttpPost]
//        [ValidateAntiForgeryToken]
//        public async Task<ActionResult> Edit([Bind(Include = "Id,Degree,HospitalDepartmentId,ClinicId,Salary,Vacation,EmploymentDate,SalaryAddition,VacationAddition,FirstName,SecondName")] Surgeon surgeon)
//        {
//            if (ModelState.IsValid)
//            {
//                db.Entry(surgeon).State = EntityState.Modified;
//                await db.SaveChangesAsync();
//                return RedirectToAction("Index");
//            }
//            ViewBag.ClinicId = new SelectList(db.Clinics, "Id", "MedicalInstitutionName", surgeon.ClinicId);
//            ViewBag.HospitalDepartmentId = new SelectList(db.HospitalDepartments, "Id", "Id", surgeon.HospitalDepartmentId);
//            return View(surgeon);
//        }

//        // GET: Surgeons/Delete/5
//        public async Task<ActionResult> Delete(int? id)
//        {
//            if (id == null)
//            {
//                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
//            }
//            Surgeon surgeon = await db.Surgeons.FindAsync(id);
//            if (surgeon == null)
//            {
//                return HttpNotFound();
//            }
//            return View(surgeon);
//        }

//        // POST: Surgeons/Delete/5
//        [HttpPost, ActionName("Delete")]
//        [ValidateAntiForgeryToken]
//        public async Task<ActionResult> DeleteConfirmed(int id)
//        {
//            Surgeon surgeon = await db.Surgeons.FindAsync(id);
//            db.Surgeons.Remove(surgeon);
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
