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
//    public class NeuropathologistsController : Controller
//    {
//        private MedicalInstitutionsContext db = new MedicalInstitutionsContext();

//        // GET: Neuropathologists
//        public async Task<ActionResult> Index()
//        {
//            var medicalStaffs = db.Neuropathologists.Include(n => n.Clinic).Include(n => n.HospitalDepartment);
//            return View(await medicalStaffs.ToListAsync());
//        }

//        // GET: Neuropathologists/Details/5
//        public async Task<ActionResult> Details(int? id)
//        {
//            if (id == null)
//            {
//                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
//            }
//            Neuropathologist neuropathologist = await db.Neuropathologists.FindAsync(id);
//            if (neuropathologist == null)
//            {
//                return HttpNotFound();
//            }
//            return View(neuropathologist);
//        }

//        // GET: Neuropathologists/Create
//        public ActionResult Create()
//        {
//            ViewBag.ClinicId = new SelectList(db.Clinics, "Id", "MedicalInstitutionName");
//            ViewBag.HospitalDepartmentId = new SelectList(db.HospitalDepartments, "Id", "Id");
//            return View();
//        }

//        // POST: Neuropathologists/Create
//        // Чтобы защититься от атак чрезмерной передачи данных, включите определенные свойства, для которых следует установить привязку. Дополнительные 
//        // сведения см. в статье https://go.microsoft.com/fwlink/?LinkId=317598.
//        [HttpPost]
//        [ValidateAntiForgeryToken]
//        public async Task<ActionResult> Create([Bind(Include = "Id,Degree,HospitalDepartmentId,ClinicId,Salary,Vacation,EmploymentDate,SalaryAddition,VacationAddition,FirstName,SecondName")] Neuropathologist neuropathologist)
//        {
//            if (ModelState.IsValid)
//            {
//                db.Neuropathologists.Add(neuropathologist);
//                await db.SaveChangesAsync();
//                return RedirectToAction("Index");
//            }

//            ViewBag.ClinicId = new SelectList(db.Clinics, "Id", "MedicalInstitutionName", neuropathologist.ClinicId);
//            ViewBag.HospitalDepartmentId = new SelectList(db.HospitalDepartments, "Id", "Id", neuropathologist.HospitalDepartmentId);
//            return View(neuropathologist);
//        }

//        // GET: Neuropathologists/Edit/5
//        public async Task<ActionResult> Edit(int? id)
//        {
//            if (id == null)
//            {
//                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
//            }
//            Neuropathologist neuropathologist = await db.Neuropathologists.FindAsync(id);
//            if (neuropathologist == null)
//            {
//                return HttpNotFound();
//            }
//            ViewBag.ClinicId = new SelectList(db.Clinics, "Id", "MedicalInstitutionName", neuropathologist.ClinicId);
//            ViewBag.HospitalDepartmentId = new SelectList(db.HospitalDepartments, "Id", "Id", neuropathologist.HospitalDepartmentId);
//            return View(neuropathologist);
//        }

//        // POST: Neuropathologists/Edit/5
//        // Чтобы защититься от атак чрезмерной передачи данных, включите определенные свойства, для которых следует установить привязку. Дополнительные 
//        // сведения см. в статье https://go.microsoft.com/fwlink/?LinkId=317598.
//        [HttpPost]
//        [ValidateAntiForgeryToken]
//        public async Task<ActionResult> Edit([Bind(Include = "Id,Degree,HospitalDepartmentId,ClinicId,Salary,Vacation,EmploymentDate,SalaryAddition,VacationAddition,FirstName,SecondName")] Neuropathologist neuropathologist)
//        {
//            if (ModelState.IsValid)
//            {
//                db.Entry(neuropathologist).State = EntityState.Modified;
//                await db.SaveChangesAsync();
//                return RedirectToAction("Index");
//            }
//            ViewBag.ClinicId = new SelectList(db.Clinics, "Id", "MedicalInstitutionName", neuropathologist.ClinicId);
//            ViewBag.HospitalDepartmentId = new SelectList(db.HospitalDepartments, "Id", "Id", neuropathologist.HospitalDepartmentId);
//            return View(neuropathologist);
//        }

//        // GET: Neuropathologists/Delete/5
//        public async Task<ActionResult> Delete(int? id)
//        {
//            if (id == null)
//            {
//                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
//            }
//            Neuropathologist neuropathologist = await db.Neuropathologists.FindAsync(id);
//            if (neuropathologist == null)
//            {
//                return HttpNotFound();
//            }
//            return View(neuropathologist);
//        }

//        // POST: Neuropathologists/Delete/5
//        [HttpPost, ActionName("Delete")]
//        [ValidateAntiForgeryToken]
//        public async Task<ActionResult> DeleteConfirmed(int id)
//        {
//            Neuropathologist neuropathologist = await db.Neuropathologists.FindAsync(id);
//            db.Neuropathologists.Remove(neuropathologist);
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
