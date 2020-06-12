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
using MedicalInstitutions.Models.MedicalInstitutions.Hospitals;
using PagedList;

namespace MedicalInstitutions.Controllers
{
	[Authorize(Roles = "Admin")]
	[OutputCache(CacheProfile = "defaultCacheProfile")]
	public class HospitalBuildingsController : Controller
    {
        private MedicalInstitutionsContext db = new MedicalInstitutionsContext();

		// GET: HospitalBuildings
		public ActionResult Index(int? pageNum)
		{
			int sizeOfPage = 4;
			int numOfPage = (pageNum ?? 1);
			var hospitalBuildings = db.HospitalBuildings
				.Include(h => h.Hospital)
				.OrderBy(i => i.Id);
			return View(hospitalBuildings.ToPagedList(numOfPage, sizeOfPage));
        }

		// GET: HospitalBuildings/Details/5
		public async Task<ActionResult> Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            HospitalBuilding hospitalBuilding = await db.HospitalBuildings.FindAsync(id);
            if (hospitalBuilding == null)
            {
                return HttpNotFound();
            }
            return View(hospitalBuilding);
        }

        // GET: HospitalBuildings/Create
        public ActionResult Create()
        {
            ViewBag.HospitalId = new SelectList(db.Hospitals, "Id", "MedicalInstitutionName");
            return View();
        }

        // POST: HospitalBuildings/Create
        // Чтобы защититься от атак чрезмерной передачи данных, включите определенные свойства, для которых следует установить привязку. Дополнительные 
        // сведения см. в статье https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create([Bind(Include = "Id,HospitalId,Number")] HospitalBuilding hospitalBuilding)
        {
            if (ModelState.IsValid)
            {
                db.HospitalBuildings.Add(hospitalBuilding);
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }

            ViewBag.HospitalId = new SelectList(db.Hospitals, "Id", "MedicalInstitutionName", hospitalBuilding.HospitalId);
            return View(hospitalBuilding);
        }

        // GET: HospitalBuildings/Edit/5
        public async Task<ActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            HospitalBuilding hospitalBuilding = await db.HospitalBuildings.FindAsync(id);
            if (hospitalBuilding == null)
            {
                return HttpNotFound();
            }
            ViewBag.HospitalId = new SelectList(db.MedicalInstitutions, "Id", "MedicalInstitutionName", hospitalBuilding.HospitalId);
            return View(hospitalBuilding);
        }

        // POST: HospitalBuildings/Edit/5
        // Чтобы защититься от атак чрезмерной передачи данных, включите определенные свойства, для которых следует установить привязку. Дополнительные 
        // сведения см. в статье https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit([Bind(Include = "Id,HospitalId")] HospitalBuilding hospitalBuilding)
        {
            if (ModelState.IsValid)
            {
                db.Entry(hospitalBuilding).State = EntityState.Modified;
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            ViewBag.HospitalId = new SelectList(db.MedicalInstitutions, "Id", "MedicalInstitutionName", hospitalBuilding.HospitalId);
            return View(hospitalBuilding);
        }

        // GET: HospitalBuildings/Delete/5
        public async Task<ActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            HospitalBuilding hospitalBuilding = await db.HospitalBuildings.FindAsync(id);
            if (hospitalBuilding == null)
            {
                return HttpNotFound();
            }
            return View(hospitalBuilding);
        }

        // POST: HospitalBuildings/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> DeleteConfirmed(int id)
        {
            HospitalBuilding hospitalBuilding = await db.HospitalBuildings.FindAsync(id);
            db.HospitalBuildings.Remove(hospitalBuilding);
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
