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
	public class HospitalDepartmentsController : Controller
    {
        private MedicalInstitutionsContext db = new MedicalInstitutionsContext();

		// GET: HospitalDepartments
		public ActionResult Index(int? pageNum)
		{
			int sizeOfPage = 4;
			int numOfPage = (pageNum ?? 1);
			var hospitalDepartments = db.HospitalDepartments
				.Include(h => h.DiseaseGroup)
				.Include(h => h.HospitalBuilding)
				.OrderBy(i => i.Id);
			return View(hospitalDepartments.ToPagedList(numOfPage, sizeOfPage));
        }

		// GET: HospitalDepartments/Details/5
		public async Task<ActionResult> Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            HospitalDepartment hospitalDepartment = await db.HospitalDepartments.FindAsync(id);
            if (hospitalDepartment == null)
            {
                return HttpNotFound();
            }
            return View(hospitalDepartment);
        }

        // GET: HospitalDepartments/Create
        public ActionResult Create()
        {
            ViewBag.DiseaseGroupId = new SelectList(db.DiseaseGroups, "Id", "Name");
            ViewBag.HospitalBuildingId = new SelectList(db.HospitalBuildings.ToList().Select(hd => new {hd.Id, Name = hd.HospitalBuildingName}), "Id", "Name");

			return View();
        }

        // POST: HospitalDepartments/Create
        // Чтобы защититься от атак чрезмерной передачи данных, включите определенные свойства, для которых следует установить привязку. Дополнительные 
        // сведения см. в статье https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create([Bind(Include = "Id,DiseaseGroupId,HospitalBuildingId")] HospitalDepartment hospitalDepartment)
        {
            if (ModelState.IsValid)
            {
                db.HospitalDepartments.Add(hospitalDepartment);
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }

            ViewBag.DiseaseGroupId = new SelectList(db.DiseaseGroups, "Id", "Name", hospitalDepartment.DiseaseGroupId);
            ViewBag.HospitalBuildingId = new SelectList(db.HospitalBuildings.ToList().Select(hd => new { hd.Id, Name = hd.HospitalBuildingName }), "Id", "Name");
			return View(hospitalDepartment);
        }

        // GET: HospitalDepartments/Edit/5
        public async Task<ActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            HospitalDepartment hospitalDepartment = await db.HospitalDepartments.FindAsync(id);
            if (hospitalDepartment == null)
            {
                return HttpNotFound();
            }
            ViewBag.DiseaseGroupId = new SelectList(db.DiseaseGroups, "Id", "Name", hospitalDepartment.DiseaseGroupId);
            ViewBag.HospitalBuildingId = new SelectList(db.HospitalBuildings.ToList().Select(hd => new { hd.Id, Name = hd.HospitalBuildingName }), "Id", "Name");
			return View(hospitalDepartment);
        }

        // POST: HospitalDepartments/Edit/5
        // Чтобы защититься от атак чрезмерной передачи данных, включите определенные свойства, для которых следует установить привязку. Дополнительные 
        // сведения см. в статье https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit([Bind(Include = "Id,DiseaseGroupId,HospitalBuildingId")] HospitalDepartment hospitalDepartment)
        {
            if (ModelState.IsValid)
            {
                db.Entry(hospitalDepartment).State = EntityState.Modified;
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            ViewBag.DiseaseGroupId = new SelectList(db.DiseaseGroups, "Id", "Name", hospitalDepartment.DiseaseGroupId);
            ViewBag.HospitalBuildingId = new SelectList(db.HospitalBuildings.ToList().Select(hd => new { hd.Id, Name = hd.HospitalBuildingName }), "Id", "Name");
			return View(hospitalDepartment);
        }

        // GET: HospitalDepartments/Delete/5
        public async Task<ActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            HospitalDepartment hospitalDepartment = await db.HospitalDepartments.FindAsync(id);
            if (hospitalDepartment == null)
            {
                return HttpNotFound();
            }
            return View(hospitalDepartment);
        }

        // POST: HospitalDepartments/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> DeleteConfirmed(int id)
        {
            HospitalDepartment hospitalDepartment = await db.HospitalDepartments.FindAsync(id);
            db.HospitalDepartments.Remove(hospitalDepartment);
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
