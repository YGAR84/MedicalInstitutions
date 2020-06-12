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
//    public class GynecologistsController : Controller
//    {
//        private MedicalInstitutionsContext db = new MedicalInstitutionsContext();

//        // GET: Gynecologists
//        public async Task<ActionResult> Index()
//        {
//            var medicalStaffs = db.Gynecologists.Include(g => g.Clinic).Include(g => g.HospitalDepartment);
//            return View(await medicalStaffs.ToListAsync());
//        }

//        // GET: Gynecologists/Details/5
//        public async Task<ActionResult> Details(int? id)
//        {
//            if (id == null)
//            {
//                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
//            }
//            Gynecologist gynecologist = await db.Gynecologists.FindAsync(id);
//            if (gynecologist == null)
//            {
//                return HttpNotFound();
//            }
//            return View(gynecologist);
//        }

//        // GET: Gynecologists/Create
//        public ActionResult Create()
//        {
//            ViewBag.ClinicId = new SelectList(db.Clinics, "Id", "MedicalInstitutionName");
//            ViewBag.HospitalDepartmentId = new SelectList(db.HospitalDepartments, "Id", "Id");
//            return View();
//        }

//        // POST: Gynecologists/Create
//        // Чтобы защититься от атак чрезмерной передачи данных, включите определенные свойства, для которых следует установить привязку. Дополнительные 
//        // сведения см. в статье https://go.microsoft.com/fwlink/?LinkId=317598.
//        [HttpPost]
//        [ValidateAntiForgeryToken]
//        public async Task<ActionResult> Create([Bind(Include = "Id,Degree,HospitalDepartmentId,ClinicId,Salary,Vacation,EmploymentDate,SalaryAddition,VacationAddition,FirstName,SecondName")] Gynecologist gynecologist)
//        {
//            if (ModelState.IsValid)
//            {
//                db.Gynecologists.Add(gynecologist);
//                await db.SaveChangesAsync();
//                return RedirectToAction("Index");
//            }

//            ViewBag.ClinicId = new SelectList(db.Clinics, "Id", "MedicalInstitutionName", gynecologist.ClinicId);
//            ViewBag.HospitalDepartmentId = new SelectList(db.HospitalDepartments, "Id", "Id", gynecologist.HospitalDepartmentId);
//            return View(gynecologist);
//        }

//        // GET: Gynecologists/Edit/5
//        public async Task<ActionResult> Edit(int? id)
//        {
//            if (id == null)
//            {
//                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
//            }
//            Gynecologist gynecologist = await db.Gynecologists.FindAsync(id);
//            if (gynecologist == null)
//            {
//                return HttpNotFound();
//            }
//            ViewBag.ClinicId = new SelectList(db.Clinics, "Id", "MedicalInstitutionName", gynecologist.ClinicId);
//            ViewBag.HospitalDepartmentId = new SelectList(db.HospitalDepartments, "Id", "Id", gynecologist.HospitalDepartmentId);
//            return View(gynecologist);
//        }

//        // POST: Gynecologists/Edit/5
//        // Чтобы защититься от атак чрезмерной передачи данных, включите определенные свойства, для которых следует установить привязку. Дополнительные 
//        // сведения см. в статье https://go.microsoft.com/fwlink/?LinkId=317598.
//        [HttpPost]
//        [ValidateAntiForgeryToken]
//        public async Task<ActionResult> Edit([Bind(Include = "Id,Degree,HospitalDepartmentId,ClinicId,Salary,Vacation,EmploymentDate,SalaryAddition,VacationAddition,FirstName,SecondName")] Gynecologist gynecologist)
//        {
//            if (ModelState.IsValid)
//            {
//                db.Entry(gynecologist).State = EntityState.Modified;
//                await db.SaveChangesAsync();
//                return RedirectToAction("Index");
//            }
//            ViewBag.ClinicId = new SelectList(db.Clinics, "Id", "MedicalInstitutionName", gynecologist.ClinicId);
//            ViewBag.HospitalDepartmentId = new SelectList(db.HospitalDepartments, "Id", "Id", gynecologist.HospitalDepartmentId);
//            return View(gynecologist);
//        }

//        // GET: Gynecologists/Delete/5
//        public async Task<ActionResult> Delete(int? id)
//        {
//            if (id == null)
//            {
//                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
//            }
//            Gynecologist gynecologist = await db.Gynecologists.FindAsync(id);
//            if (gynecologist == null)
//            {
//                return HttpNotFound();
//            }
//            return View(gynecologist);
//        }

//        // POST: Gynecologists/Delete/5
//        [HttpPost, ActionName("Delete")]
//        [ValidateAntiForgeryToken]
//        public async Task<ActionResult> DeleteConfirmed(int id)
//        {
//            Gynecologist gynecologist = await db.Gynecologists.FindAsync(id);
//            db.Gynecologists.Remove(gynecologist);
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
