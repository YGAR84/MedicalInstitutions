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
	[Authorize(Roles = "Admin")]
	[OutputCache(CacheProfile = "defaultCacheProfile")]
	public class MedicalStaffProfilesController : Controller
    {
        private MedicalInstitutionsContext db = new MedicalInstitutionsContext();

		// GET: MedicalStaffProfiles
		public ActionResult Index(int? pageNum)
		{
			int sizeOfPage = 4;
			int numOfPage = (pageNum ?? 1);

			var medicalStaffProfiles = db.MedicalStaffProfiles
				.Include(m => m.DiseaseGroup)
				.OrderBy(l => l.Id);
			return View(medicalStaffProfiles.ToPagedList(numOfPage, sizeOfPage));
        }

		// GET: MedicalStaffProfiles/Details/5
		public async Task<ActionResult> Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            MedicalStaffProfile medicalStaffProfile = await db.MedicalStaffProfiles.FindAsync(id);
            if (medicalStaffProfile == null)
            {
                return HttpNotFound();
            }
            return View(medicalStaffProfile);
        }

        // GET: MedicalStaffProfiles/Create
        public ActionResult Create()
        {
            ViewBag.DiseaseGroupId = new SelectList(db.DiseaseGroups, "Id", "Name");
            return View();
        }

        // POST: MedicalStaffProfiles/Create
        // Чтобы защититься от атак чрезмерной передачи данных, включите определенные свойства, для которых следует установить привязку. Дополнительные 
        // сведения см. в статье https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create([Bind(Include = "Id,ProfileName,DiseaseGroupId,SalaryAddition,VacationAddition,IsSurgeon")] MedicalStaffProfile medicalStaffProfile)
        {
            if (ModelState.IsValid)
            {
                db.MedicalStaffProfiles.Add(medicalStaffProfile);
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }

            ViewBag.DiseaseGroupId = new SelectList(db.DiseaseGroups, "Id", "Name", medicalStaffProfile.DiseaseGroupId);
            return View(medicalStaffProfile);
        }

        // GET: MedicalStaffProfiles/Edit/5
        public async Task<ActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            MedicalStaffProfile medicalStaffProfile = await db.MedicalStaffProfiles.FindAsync(id);
            if (medicalStaffProfile == null)
            {
                return HttpNotFound();
            }
            ViewBag.DiseaseGroupId = new SelectList(db.DiseaseGroups, "Id", "Name", medicalStaffProfile.DiseaseGroupId);
            return View(medicalStaffProfile);
        }

        // POST: MedicalStaffProfiles/Edit/5
        // Чтобы защититься от атак чрезмерной передачи данных, включите определенные свойства, для которых следует установить привязку. Дополнительные 
        // сведения см. в статье https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit([Bind(Include = "Id,ProfileName,DiseaseGroupId,SalaryAddition,VacationAddition,IsSurgeon")] MedicalStaffProfile medicalStaffProfile)
        {
            if (ModelState.IsValid)
            {
                db.Entry(medicalStaffProfile).State = EntityState.Modified;
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            ViewBag.DiseaseGroupId = new SelectList(db.DiseaseGroups, "Id", "Name", medicalStaffProfile.DiseaseGroupId);
            return View(medicalStaffProfile);
        }

        // GET: MedicalStaffProfiles/Delete/5
        public async Task<ActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            MedicalStaffProfile medicalStaffProfile = await db.MedicalStaffProfiles.FindAsync(id);
            if (medicalStaffProfile == null)
            {
                return HttpNotFound();
            }
            return View(medicalStaffProfile);
        }

        // POST: MedicalStaffProfiles/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> DeleteConfirmed(int id)
        {
            MedicalStaffProfile medicalStaffProfile = await db.MedicalStaffProfiles.FindAsync(id);
            db.MedicalStaffProfiles.Remove(medicalStaffProfile);
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
