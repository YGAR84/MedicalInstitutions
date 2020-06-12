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
using MedicalInstitutions.Models.MedicalInstitutions.Clinics;
using PagedList;

namespace MedicalInstitutions.Controllers
{
	[Authorize(Roles = "Admin")]
	[OutputCache(CacheProfile = "defaultCacheProfile")]
	public class ClinicsController : Controller
    {
        private MedicalInstitutionsContext db = new MedicalInstitutionsContext();

		// GET: Clinics
		public ActionResult Index(int? pageNum)
		{
			int sizeOfPage = 4;
			int numOfPage = (pageNum ?? 1);
			var medicalInstitutions = db.Clinics.Include(c => c.Hospital).OrderBy(i => i.Id);
			return View(medicalInstitutions.ToPagedList(numOfPage, sizeOfPage));
		}


		// GET: Clinics/Details/5
		public async Task<ActionResult> Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Clinic clinic = await db.Clinics.FindAsync(id);
            if (clinic == null)
            {
                return HttpNotFound();
            }
            return View(clinic);
        }

        // GET: Clinics/Create
        public ActionResult Create()
        {
            ViewBag.HospitalId = new SelectList(db.Hospitals, "Id", "MedicalInstitutionName", null);
            return View();
        }

        // POST: Clinics/Create
        // Чтобы защититься от атак чрезмерной передачи данных, включите определенные свойства, для которых следует установить привязку. Дополнительные 
        // сведения см. в статье https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create([Bind(Include = "Id,MedicalInstitutionName,Address,HospitalId")] Clinic clinic)
        {
            if (ModelState.IsValid)
            {
                db.Clinics.Add(clinic);
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }

            ViewBag.HospitalId = new SelectList(db.Hospitals, "Id", "MedicalInstitutionName", clinic.HospitalId);
            return View(clinic);
        }

        // GET: Clinics/Edit/5
        public async Task<ActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Clinic clinic = await db.Clinics.FindAsync(id);
            if (clinic == null)
            {
                return HttpNotFound();
            }
            ViewBag.HospitalId = new SelectList(db.Hospitals, "Id", "MedicalInstitutionName", clinic.HospitalId);
            return View(clinic);
        }

        // POST: Clinics/Edit/5
        // Чтобы защититься от атак чрезмерной передачи данных, включите определенные свойства, для которых следует установить привязку. Дополнительные 
        // сведения см. в статье https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit([Bind(Include = "Id,MedicalInstitutionName,Address,HospitalId")] Clinic clinic)
        {
            if (ModelState.IsValid)
            {
                db.Entry(clinic).State = EntityState.Modified;
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            ViewBag.HospitalId = new SelectList(db.Hospitals, "Id", "MedicalInstitutionName", clinic.HospitalId);
            return View(clinic);
        }

        // GET: Clinics/Delete/5
        public async Task<ActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Clinic clinic = await db.Clinics.FindAsync(id);
            if (clinic == null)
            {
                return HttpNotFound();
            }
            return View(clinic);
        }

        // POST: Clinics/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> DeleteConfirmed(int id)
        {
            Clinic clinic = await db.Clinics.FindAsync(id);
            db.Clinics.Remove(clinic);
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
