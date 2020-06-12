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
using MedicalInstitutions.Models.MedicalInstitutions.Laboratories;
using PagedList;

namespace MedicalInstitutions.Controllers
{
	[Authorize(Roles = "Admin")]
	[OutputCache(CacheProfile = "defaultCacheProfile")]
	public class LaboratoryContractsController : Controller
    {
        private MedicalInstitutionsContext db = new MedicalInstitutionsContext();

		// GET: LaboratoryContracts
		public ActionResult Index(int? pageNum)
		{
			int sizeOfPage = 4;
			int numOfPage = (pageNum ?? 1);
			var laboratoryContracts = db.LaboratoryContracts
	            .Include(l => l.Laboratory)
	            .Include(l => l.MedicalInstitution)
	            .OrderBy(l => l.Id);

			return View(laboratoryContracts.ToPagedList(numOfPage, sizeOfPage));
		}
 

		// GET: LaboratoryContracts/Details/5
		public async Task<ActionResult> Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            LaboratoryContract laboratoryContract = await db.LaboratoryContracts.FindAsync(id);
            if (laboratoryContract == null)
            {
                return HttpNotFound();
            }
            return View(laboratoryContract);
        }

        // GET: LaboratoryContracts/Create
        public ActionResult Create()
        {
	        ViewBag.LaboratoryId = new SelectList(db.Laboratories.ToList().Select(l => new { l.Id, Name = l.LaboratoryName }), "Id", "Name");
	        ViewBag.MedicalInstitutionId = new SelectList(db.MedicalInstitutions.ToList().Select(mi => new { mi.Id, Name = mi.MedicalInstitutionName }), "Id", "Name");
			return View();
        }

        // POST: LaboratoryContracts/Create
        // Чтобы защититься от атак чрезмерной передачи данных, включите определенные свойства, для которых следует установить привязку. Дополнительные 
        // сведения см. в статье https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create([Bind(Include = "Id,LaboratoryId,MedicalInstitutionId,ContractPrice")] LaboratoryContract laboratoryContract)
        {
            if (ModelState.IsValid)
            {
                db.LaboratoryContracts.Add(laboratoryContract);
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            ViewBag.LaboratoryId = new SelectList(db.Laboratories.ToList().Select(l => new { l.Id, Name = l.LaboratoryName }), "Id", "Name", laboratoryContract.LaboratoryId);
            ViewBag.MedicalInstitutionId = new SelectList(db.MedicalInstitutions.ToList().Select(mi => new { mi.Id, Name = mi.MedicalInstitutionName }), "Id", "Name", laboratoryContract.MedicalInstitutionId);
			//ViewBag.LaboratoryId = new SelectList(db.Laboratories, "Id", "Address", laboratoryContract.LaboratoryId);
            //ViewBag.MedicalInstitutionId = new SelectList(db.MedicalInstitutions, "Id", "MedicalInstitutionName", laboratoryContract.MedicalInstitutionId);
            return View(laboratoryContract);
        }

        // GET: LaboratoryContracts/Edit/5
        public async Task<ActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            LaboratoryContract laboratoryContract = await db.LaboratoryContracts.FindAsync(id);
            if (laboratoryContract == null)
            {
                return HttpNotFound();
            }
            ViewBag.LaboratoryId = new SelectList(db.Laboratories.ToList().Select(l => new { l.Id, Name = l.LaboratoryName }), "Id", "Name", laboratoryContract.LaboratoryId);
            ViewBag.MedicalInstitutionId = new SelectList(db.MedicalInstitutions.ToList().Select(mi => new { mi.Id, Name = mi.MedicalInstitutionName }), "Id", "Name", laboratoryContract.MedicalInstitutionId);
			//ViewBag.LaboratoryId = new SelectList(db.Laboratories, "Id", "Address", laboratoryContract.LaboratoryId);
			//ViewBag.MedicalInstitutionId = new SelectList(db.MedicalInstitutions, "Id", "MedicalInstitutionName", laboratoryContract.MedicalInstitutionId);
			return View(laboratoryContract);
        }

        // POST: LaboratoryContracts/Edit/5
        // Чтобы защититься от атак чрезмерной передачи данных, включите определенные свойства, для которых следует установить привязку. Дополнительные 
        // сведения см. в статье https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit([Bind(Include = "Id,LaboratoryId,MedicalInstitutionId,ContractPrice")] LaboratoryContract laboratoryContract)
        {
            if (ModelState.IsValid)
            {
                db.Entry(laboratoryContract).State = EntityState.Modified;
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            ViewBag.LaboratoryId = new SelectList(db.Laboratories.ToList().Select(l => new { l.Id, Name = l.LaboratoryName }), "Id", "Name", laboratoryContract.LaboratoryId);
            ViewBag.MedicalInstitutionId = new SelectList(db.MedicalInstitutions.ToList().Select(mi => new { mi.Id, Name = mi.MedicalInstitutionName }), "Id", "Name", laboratoryContract.MedicalInstitutionId);
			//ViewBag.LaboratoryId = new SelectList(db.Laboratories, "Id", "Address", laboratoryContract.LaboratoryId);
			//ViewBag.MedicalInstitutionId = new SelectList(db.MedicalInstitutions, "Id", "MedicalInstitutionName", laboratoryContract.MedicalInstitutionId);
			return View(laboratoryContract);
        }

        // GET: LaboratoryContracts/Delete/5
        public async Task<ActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            LaboratoryContract laboratoryContract = await db.LaboratoryContracts.FindAsync(id);
            if (laboratoryContract == null)
            {
                return HttpNotFound();
            }
            return View(laboratoryContract);
        }

        // POST: LaboratoryContracts/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> DeleteConfirmed(int id)
        {
            LaboratoryContract laboratoryContract = await db.LaboratoryContracts.FindAsync(id);
            db.LaboratoryContracts.Remove(laboratoryContract);
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
