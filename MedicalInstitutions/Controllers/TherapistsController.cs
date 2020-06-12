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
//using MedicalInstitutions.Models.Staffs.MedicalStaffs;

//namespace MedicalInstitutions.Controllers
//{
//    public class TherapistsController : Controller
//    {
//        private MedicalInstitutionsContext db = new MedicalInstitutionsContext();

//        // GET: Therapists
//        public async Task<ActionResult> Index()
//        {
//            var medicalStaffs = db.Therapists.Include(t => t.Clinic).Include(t => t.HospitalDepartment);
//            return View(await medicalStaffs.ToListAsync());
//        }

//        // GET: Therapists/Details/5
//        public async Task<ActionResult> Details(int? id)
//        {
//            if (id == null)
//            {
//                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
//            }
//            Therapist therapist = await db.Therapists.FindAsync(id);
//            if (therapist == null)
//            {
//                return HttpNotFound();
//            }
//            return View(therapist);
//        }

//        // GET: Therapists/Create
//        public ActionResult Create()
//        {
//            ViewBag.ClinicId = new SelectList(db.Clinics, "Id", "MedicalInstitutionName");
//            ViewBag.HospitalDepartmentId = new SelectList(db.HospitalDepartments, "Id", "Id");
//            return View();
//        }

//        // POST: Therapists/Create
//        // Чтобы защититься от атак чрезмерной передачи данных, включите определенные свойства, для которых следует установить привязку. Дополнительные 
//        // сведения см. в статье https://go.microsoft.com/fwlink/?LinkId=317598.
//        [HttpPost]
//        [ValidateAntiForgeryToken]
//        public async Task<ActionResult> Create([Bind(Include = "Id,Degree,HospitalDepartmentId,ClinicId,Salary,Vacation,EmploymentDate,SalaryAddition,VacationAddition,FirstName,SecondName")] Therapist therapist)
//        {
//            if (ModelState.IsValid)
//            {
//                db.Therapists.Add(therapist);
//                await db.SaveChangesAsync();
//                return RedirectToAction("Index");
//            }

//            ViewBag.ClinicId = new SelectList(db.Clinics, "Id", "MedicalInstitutionName", therapist.ClinicId);
//            ViewBag.HospitalDepartmentId = new SelectList(db.HospitalDepartments, "Id", "Id", therapist.HospitalDepartmentId);
//            return View(therapist);
//        }

//        // GET: Therapists/Edit/5
//        public async Task<ActionResult> Edit(int? id)
//        {
//            if (id == null)
//            {
//                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
//            }
//            Therapist therapist = await db.Therapists.FindAsync(id);
//            if (therapist == null)
//            {
//                return HttpNotFound();
//            }
//            ViewBag.ClinicId = new SelectList(db.Clinics, "Id", "MedicalInstitutionName", therapist.ClinicId);
//            ViewBag.HospitalDepartmentId = new SelectList(db.HospitalDepartments, "Id", "Id", therapist.HospitalDepartmentId);
//            return View(therapist);
//        }

//        // POST: Therapists/Edit/5
//        // Чтобы защититься от атак чрезмерной передачи данных, включите определенные свойства, для которых следует установить привязку. Дополнительные 
//        // сведения см. в статье https://go.microsoft.com/fwlink/?LinkId=317598.
//        [HttpPost]
//        [ValidateAntiForgeryToken]
//        public async Task<ActionResult> Edit([Bind(Include = "Id,Degree,HospitalDepartmentId,ClinicId,Salary,Vacation,EmploymentDate,SalaryAddition,VacationAddition,FirstName,SecondName")] Therapist therapist)
//        {
//            if (ModelState.IsValid)
//            {
//                db.Entry(therapist).State = EntityState.Modified;
//                await db.SaveChangesAsync();
//                return RedirectToAction("Index");
//            }
//            ViewBag.ClinicId = new SelectList(db.Clinics, "Id", "MedicalInstitutionName", therapist.ClinicId);
//            ViewBag.HospitalDepartmentId = new SelectList(db.HospitalDepartments, "Id", "Id", therapist.HospitalDepartmentId);
//            return View(therapist);
//        }

//        // GET: Therapists/Delete/5
//        public async Task<ActionResult> Delete(int? id)
//        {
//            if (id == null)
//            {
//                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
//            }
//            Therapist therapist = await db.Therapists.FindAsync(id);
//            if (therapist == null)
//            {
//                return HttpNotFound();
//            }
//            return View(therapist);
//        }

//        // POST: Therapists/Delete/5
//        [HttpPost, ActionName("Delete")]
//        [ValidateAntiForgeryToken]
//        public async Task<ActionResult> DeleteConfirmed(int id)
//        {
//            Therapist therapist = await db.Therapists.FindAsync(id);
//            db.Therapists.Remove(therapist);
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
