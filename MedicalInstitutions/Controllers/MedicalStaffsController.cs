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
	public class MedicalStaffsController : Controller
    {
        private MedicalInstitutionsContext db = new MedicalInstitutionsContext();

		// GET: MedicalStaffs

		public ActionResult Index(int? pageNum)
		{
			int sizeOfPage = 4;
			int numOfPage = (pageNum ?? 1);

			var medicalStaffs = db.MedicalStaffs
	            .Include(m => m.Profile)
	            .OrderBy(l => l.Id);
			return View(medicalStaffs.ToPagedList(numOfPage, sizeOfPage));
        }


		// GET: MedicalStaffs/Details/5
		public async Task<ActionResult> Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            MedicalStaff medicalStaff = await db.MedicalStaffs.FindAsync(id);
            if (medicalStaff == null)
            {
                return HttpNotFound();
            }
            return View(medicalStaff);
        }

        // GET: MedicalStaffs/Create
        public ActionResult Create()
        {
            ViewBag.ProfileId = new SelectList(db.MedicalStaffProfiles, "Id", "ProfileName");
            return View();
        }

        // POST: MedicalStaffs/Create
        // Чтобы защититься от атак чрезмерной передачи данных, включите определенные свойства, для которых следует установить привязку. Дополнительные 
        // сведения см. в статье https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create([Bind(Include = "Id,Degree,ProfileId,FirstName,SecondName")] MedicalStaff medicalStaff)
        {
            if (ModelState.IsValid)
            {
                db.MedicalStaffs.Add(medicalStaff);
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }

            ViewBag.ProfileId = new SelectList(db.MedicalStaffProfiles, "Id", "ProfileName", medicalStaff.ProfileId);
            return View(medicalStaff);
        }

        // GET: MedicalStaffs/Edit/5
        public async Task<ActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            MedicalStaff medicalStaff = await db.MedicalStaffs.FindAsync(id);
            if (medicalStaff == null)
            {
                return HttpNotFound();
            }
            ViewBag.ProfileId = new SelectList(db.MedicalStaffProfiles, "Id", "ProfileName", medicalStaff.ProfileId);
            return View(medicalStaff);
        }

        // POST: MedicalStaffs/Edit/5
        // Чтобы защититься от атак чрезмерной передачи данных, включите определенные свойства, для которых следует установить привязку. Дополнительные 
        // сведения см. в статье https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit([Bind(Include = "Id,Degree,ProfileId,FirstName,SecondName")] MedicalStaff medicalStaff)
        {
            if (ModelState.IsValid)
            {
                db.Entry(medicalStaff).State = EntityState.Modified;
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            ViewBag.ProfileId = new SelectList(db.MedicalStaffProfiles, "Id", "ProfileName", medicalStaff.ProfileId);
            return View(medicalStaff);
        }

        // GET: MedicalStaffs/Delete/5
        public async Task<ActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            MedicalStaff medicalStaff = await db.MedicalStaffs.FindAsync(id);
            if (medicalStaff == null)
            {
                return HttpNotFound();
            }
            return View(medicalStaff);
        }

        // POST: MedicalStaffs/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> DeleteConfirmed(int id)
        {
            MedicalStaff medicalStaff = await db.MedicalStaffs.FindAsync(id);
            db.MedicalStaffs.Remove(medicalStaff);
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
