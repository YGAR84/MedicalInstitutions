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
using MedicalInstitutions.Models.Staffs.MedicalStaffs;
using PagedList;

namespace MedicalInstitutions.Controllers
{
	[Authorize(Roles = "Admin,HR")]
	[OutputCache(CacheProfile = "defaultCacheProfile")]
	public class MedicalStaffEmploymentsController : Controller
    {
        private MedicalInstitutionsContext db = new MedicalInstitutionsContext();

		// GET: MedicalStaffEmployments
		public ActionResult Index(int? pageNum)
		{
			int sizeOfPage = 4;
			int numOfPage = (pageNum ?? 1);

			var medicalStaffEmployments = db.MedicalStaffEmployments
	            .Include(m => m.MedicalInstitution
	            ).Include(m => m.MedicalStaff)
	            .OrderBy(l => l.Id);
			return View(medicalStaffEmployments.ToPagedList(numOfPage, sizeOfPage));
        }


		// GET: MedicalStaffEmployments/Details/5
		public async Task<ActionResult> Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            MedicalStaffEmployment medicalStaffEmployment = await db.MedicalStaffEmployments.FindAsync(id);
            if (medicalStaffEmployment == null)
            {
                return HttpNotFound();
            }
            return View(medicalStaffEmployment);
        }

        // GET: MedicalStaffEmployments/Create
        public ActionResult Create()
        {
            ViewBag.MedicalInstitutionId = new SelectList(db.MedicalInstitutions, "Id", "MedicalInstitutionName");
            ViewBag.MedicalStaffId = new SelectList(db.MedicalStaffs.Include(ms => ms.Profile).ToList().Select(ms => new { ms.Id, Name = ms.MedicalStaffName }), "Id", "Name");
			return View();
        }

        // POST: MedicalStaffEmployments/Create
        // Чтобы защититься от атак чрезмерной передачи данных, включите определенные свойства, для которых следует установить привязку. Дополнительные 
        // сведения см. в статье https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create([Bind(Include = "Id,MedicalStaffId,EmploymentType,MedicalInstitutionId,Salary,Vacation,EmploymentDate,DischargeDate")] MedicalStaffEmployment medicalStaffEmployment)
        {
            if (ModelState.IsValid)
            {
                db.MedicalStaffEmployments.Add(medicalStaffEmployment);
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }

            ViewBag.MedicalInstitutionId = new SelectList(db.MedicalInstitutions, "Id", "MedicalInstitutionName", medicalStaffEmployment.MedicalInstitutionId);
            ViewBag.MedicalStaffId = new SelectList(db.MedicalStaffs.Include(ms => ms.Profile).ToList().Select(ms => new { ms.Id, Name = ms.MedicalStaffName }), "Id", "Name", medicalStaffEmployment.MedicalStaffId);
			return View(medicalStaffEmployment);
        }

        // GET: MedicalStaffEmployments/Edit/5
        public async Task<ActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            MedicalStaffEmployment medicalStaffEmployment = await db.MedicalStaffEmployments.FindAsync(id);
            if (medicalStaffEmployment == null)
            {
                return HttpNotFound();
            }
            ViewBag.MedicalInstitutionId = new SelectList(db.MedicalInstitutions, "Id", "MedicalInstitutionName", medicalStaffEmployment.MedicalInstitutionId);
            ViewBag.MedicalStaffId = new SelectList(db.MedicalStaffs.Include(ms => ms.Profile).ToList().Select(ms => new { ms.Id, Name = ms.MedicalStaffName }), "Id", "Name", medicalStaffEmployment.MedicalStaffId);
			return View(medicalStaffEmployment);
        }

        // POST: MedicalStaffEmployments/Edit/5
        // Чтобы защититься от атак чрезмерной передачи данных, включите определенные свойства, для которых следует установить привязку. Дополнительные 
        // сведения см. в статье https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit([Bind(Include = "Id,MedicalStaffId,EmploymentType,MedicalInstitutionId,Salary,Vacation,EmploymentDate,DischargeDate")] MedicalStaffEmployment medicalStaffEmployment)
        {
            if (ModelState.IsValid)
            {
                db.Entry(medicalStaffEmployment).State = EntityState.Modified;
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            ViewBag.MedicalInstitutionId = new SelectList(db.MedicalInstitutions, "Id", "MedicalInstitutionName", medicalStaffEmployment.MedicalInstitutionId);
            ViewBag.MedicalStaffId = new SelectList(db.MedicalStaffs.Include(ms => ms.Profile).ToList().Select(ms => new { ms.Id, Name = ms.MedicalStaffName }), "Id", "Name", medicalStaffEmployment.MedicalStaffId);
			return View(medicalStaffEmployment);
        }

        // GET: MedicalStaffEmployments/Delete/5
        public async Task<ActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            MedicalStaffEmployment medicalStaffEmployment = await db.MedicalStaffEmployments.FindAsync(id);
            if (medicalStaffEmployment == null)
            {
                return HttpNotFound();
            }
            return View(medicalStaffEmployment);
        }

        // POST: MedicalStaffEmployments/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> DeleteConfirmed(int id)
        {
            MedicalStaffEmployment medicalStaffEmployment = await db.MedicalStaffEmployments.FindAsync(id);
            db.MedicalStaffEmployments.Remove(medicalStaffEmployment);
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
