using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;
using System.Net;
using System.Web;
using System.Web.Mvc;
using MedicalInstitutions.Models;
using MedicalInstitutions.Models.Staffs.ServiceStaffs;
using PagedList;

namespace MedicalInstitutions.Controllers
{
	[Authorize(Roles = "Admin,HR")]
	[OutputCache(CacheProfile = "defaultCacheProfile")]
	public class ServiceStaffEmploymentsController : Controller
    {
        private MedicalInstitutionsContext db = new MedicalInstitutionsContext();

		// GET: ServiceStaffEmployments
		public ActionResult Index(int? pageNum)
		{
			int sizeOfPage = 4;
			int numOfPage = (pageNum ?? 1);

			var serviceStaffEmployments = db.ServiceStaffEmployments
				.Include(s => s.MedicalInstitution)
				.Include(s => s.ServiceStaff)
				.OrderBy(l => l.Id);
			return View(serviceStaffEmployments.ToPagedList(numOfPage, sizeOfPage));
        }

		// GET: ServiceStaffEmployments/Details/5
		public async Task<ActionResult> Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ServiceStaffEmployment serviceStaffEmployment = await db.ServiceStaffEmployments.FindAsync(id);
            if (serviceStaffEmployment == null)
            {
                return HttpNotFound();
            }
            return View(serviceStaffEmployment);
        }

        // GET: ServiceStaffEmployments/Create
        public ActionResult Create()
        {
            ViewBag.MedicalInstitutionId = new SelectList(db.MedicalInstitutions, "Id", "MedicalInstitutionName");
            ViewBag.ServiceStaffId = new SelectList(db.ServiceStaffs.Include(ss => ss.Specialty).ToList().Select(ss => new { ss.Id, Name = ss.FirstName + " " + ss.SecondName + "|" + ss.Specialty.SpecialtyName }), "Id", "Name");
			return View();
        }

        // POST: ServiceStaffEmployments/Create
        // Чтобы защититься от атак чрезмерной передачи данных, включите определенные свойства, для которых следует установить привязку. Дополнительные 
        // сведения см. в статье https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create([Bind(Include = "Id,ServiceStaffId,MedicalInstitutionId,Salary,Vacation,EmploymentDate,DischargeDate")] ServiceStaffEmployment serviceStaffEmployment)
        {
            if (ModelState.IsValid)
            {
                db.ServiceStaffEmployments.Add(serviceStaffEmployment);
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }

            ViewBag.MedicalInstitutionId = new SelectList(db.MedicalInstitutions, "Id", "MedicalInstitutionName", serviceStaffEmployment.MedicalInstitutionId);
            ViewBag.ServiceStaffId = new SelectList(db.ServiceStaffs.Include(ss => ss.Specialty).ToList().Select(ss => new { ss.Id, Name = ss.FirstName + " " + ss.SecondName + "|" + ss.Specialty.SpecialtyName }), "Id", "Name", serviceStaffEmployment.ServiceStaffId);
            return View(serviceStaffEmployment);
        }

        // GET: ServiceStaffEmployments/Edit/5
        public async Task<ActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ServiceStaffEmployment serviceStaffEmployment = await db.ServiceStaffEmployments.FindAsync(id);
            if (serviceStaffEmployment == null)
            {
                return HttpNotFound();
            }
            ViewBag.MedicalInstitutionId = new SelectList(db.MedicalInstitutions, "Id", "MedicalInstitutionName", serviceStaffEmployment.MedicalInstitutionId);
            ViewBag.ServiceStaffId = new SelectList(db.ServiceStaffs.Include(ss => ss.Specialty).ToList().Select(ss => new { ss.Id, Name = ss.FirstName + " " + ss.SecondName + "|" + ss.Specialty.SpecialtyName }), "Id", "Name", serviceStaffEmployment.ServiceStaffId);
			return View(serviceStaffEmployment);
        }

        // POST: ServiceStaffEmployments/Edit/5
        // Чтобы защититься от атак чрезмерной передачи данных, включите определенные свойства, для которых следует установить привязку. Дополнительные 
        // сведения см. в статье https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit([Bind(Include = "Id,ServiceStaffId,MedicalInstitutionId,Salary,Vacation,EmploymentDate,DischargeDate")] ServiceStaffEmployment serviceStaffEmployment)
        {
            if (ModelState.IsValid)
            {
                db.Entry(serviceStaffEmployment).State = EntityState.Modified;
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            ViewBag.MedicalInstitutionId = new SelectList(db.MedicalInstitutions, "Id", "MedicalInstitutionName", serviceStaffEmployment.MedicalInstitutionId);
            ViewBag.ServiceStaffId = new SelectList(db.ServiceStaffs.Include(ss => ss.Specialty).ToList().Select(ss => new { ss.Id, Name = ss.FirstName + " " + ss.SecondName + "|" + ss.Specialty.SpecialtyName }), "Id", "Name", serviceStaffEmployment.ServiceStaffId);
			return View(serviceStaffEmployment);
        }

        // GET: ServiceStaffEmployments/Delete/5
        public async Task<ActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ServiceStaffEmployment serviceStaffEmployment = await db.ServiceStaffEmployments.FindAsync(id);
            if (serviceStaffEmployment == null)
            {
                return HttpNotFound();
            }
            return View(serviceStaffEmployment);
        }

        // POST: ServiceStaffEmployments/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> DeleteConfirmed(int id)
        {
            ServiceStaffEmployment serviceStaffEmployment = await db.ServiceStaffEmployments.FindAsync(id);
            db.ServiceStaffEmployments.Remove(serviceStaffEmployment);
            await db.SaveChangesAsync();
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
