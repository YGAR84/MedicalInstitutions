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
//    public class DentistsController : Controller
//    {
//        private MedicalInstitutionsContext db = new MedicalInstitutionsContext();

//        // GET: Dentists
//        public async Task<ActionResult> Index()
//        {
//            var medicalStaffs = db.Dentists.Include(d => d.Clinic).Include(d => d.HospitalDepartment);
//            return View(await medicalStaffs.ToListAsync());
//        }

//        // GET: Dentists/Details/5
//        public async Task<ActionResult> Details(int? id)
//        {
//            if (id == null)
//            {
//                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
//            }
//            Dentist dentist = await db.Dentists.FindAsync(id);
//            if (dentist == null)
//            {
//                return HttpNotFound();
//            }
//            return View(dentist);
//        }

//        // GET: Dentists/Create
//        public ActionResult Create()
//        {
//            ViewBag.ClinicId = new SelectList(db.Clinics, "Id", "MedicalInstitutionName");
//            ViewBag.HospitalDepartmentId = new SelectList(db.HospitalDepartments, "Id", "Id");
//            return View();
//        }

//        // POST: Dentists/Create
//        // Чтобы защититься от атак чрезмерной передачи данных, включите определенные свойства, для которых следует установить привязку. Дополнительные 
//        // сведения см. в статье https://go.microsoft.com/fwlink/?LinkId=317598.
//        [HttpPost]
//        [ValidateAntiForgeryToken]
//        public async Task<ActionResult> Create([Bind(Include = "Id,Degree,HospitalDepartmentId,ClinicId,Salary,Vacation,EmploymentDate,SalaryAddition,VacationAddition,FirstName,SecondName")] Dentist dentist)
//        {
//            if (ModelState.IsValid)
//            {
//                db.Dentists.Add(dentist);
//                await db.SaveChangesAsync();
//                return RedirectToAction("Index");
//            }

//            ViewBag.ClinicId = new SelectList(db.Clinics, "Id", "MedicalInstitutionName", dentist.ClinicId);
//            ViewBag.HospitalDepartmentId = new SelectList(db.HospitalDepartments, "Id", "HospitalDepartmentName", dentist.HospitalDepartmentId);
//            return View(dentist);
//        }

//        // GET: Dentists/Edit/5
//        public async Task<ActionResult> Edit(int? id)
//        {
//            if (id == null)
//            {
//                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
//            }
//            Dentist dentist = await db.Dentists.FindAsync(id);
//            if (dentist == null)
//            {
//                return HttpNotFound();
//            }
//            ViewBag.ClinicId = new SelectList(db.Clinics, "Id", "MedicalInstitutionName", dentist.ClinicId);
//            ViewBag.HospitalDepartmentId = new SelectList(db.HospitalDepartments, "Id", "Id", dentist.HospitalDepartmentId);
//            return View(dentist);
//        }

//        // POST: Dentists/Edit/5
//        // Чтобы защититься от атак чрезмерной передачи данных, включите определенные свойства, для которых следует установить привязку. Дополнительные 
//        // сведения см. в статье https://go.microsoft.com/fwlink/?LinkId=317598.
//        [HttpPost]
//        [ValidateAntiForgeryToken]
//        public async Task<ActionResult> Edit([Bind(Include = "Id,Degree,HospitalDepartmentId,ClinicId,Salary,Vacation,EmploymentDate,SalaryAddition,VacationAddition,FirstName,SecondName")] Dentist dentist)
//        {
//            if (ModelState.IsValid)
//            {
//                db.Entry(dentist).State = EntityState.Modified;
//                await db.SaveChangesAsync();
//                return RedirectToAction("Index");
//            }
//            ViewBag.ClinicId = new SelectList(db.Clinics, "Id", "MedicalInstitutionName", dentist.ClinicId);
//            ViewBag.HospitalDepartmentId = new SelectList(db.HospitalDepartments, "Id", "Id", dentist.HospitalDepartmentId);
//            return View(dentist);
//        }

//        // GET: Dentists/Delete/5
//        public async Task<ActionResult> Delete(int? id)
//        {
//            if (id == null)
//            {
//                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
//            }
//            Dentist dentist = await db.Dentists.FindAsync(id);
//            if (dentist == null)
//            {
//                return HttpNotFound();
//            }
//            return View(dentist);
//        }

//        // POST: Dentists/Delete/5
//        [HttpPost, ActionName("Delete")]
//        [ValidateAntiForgeryToken]
//        public async Task<ActionResult> DeleteConfirmed(int id)
//        {
//            Dentist dentist = await db.Dentists.FindAsync(id);
//            db.Dentists.Remove(dentist);
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
