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
	public class CabinetsController : Controller
    {
        private MedicalInstitutionsContext db = new MedicalInstitutionsContext();

		// GET: Cabinets
		public ActionResult Index(int? pageNum)
		{
			int sizeOfPage = 4;
			int numOfPage = (pageNum ?? 1);
			var cabinets = db.Cabinets.Include(c => c.Clinic).OrderBy(i => i.Id);
			return View(cabinets.ToPagedList(numOfPage, sizeOfPage));
		}


		// GET: Cabinets/Details/5
		public async Task<ActionResult> Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Cabinet cabinet = await db.Cabinets.FindAsync(id);
            if (cabinet == null)
            {
                return HttpNotFound();
            }
            return View(cabinet);
        }

        // GET: Cabinets/Create
        public ActionResult Create()
        {
            ViewBag.ClinicId = new SelectList(db.Clinics, "Id", "MedicalInstitutionName");
            return View();
        }

        // POST: Cabinets/Create
        // Чтобы защититься от атак чрезмерной передачи данных, включите определенные свойства, для которых следует установить привязку. Дополнительные 
        // сведения см. в статье https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create([Bind(Include = "Id,Number,ClinicId")] Cabinet cabinet)
        {
            if (ModelState.IsValid)
            {
                db.Cabinets.Add(cabinet);
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }

            ViewBag.ClinicId = new SelectList(db.Clinics, "Id", "MedicalInstitutionName", cabinet.ClinicId);
            return View(cabinet);
        }

        // GET: Cabinets/Edit/5
        public async Task<ActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Cabinet cabinet = await db.Cabinets.FindAsync(id);
            if (cabinet == null)
            {
                return HttpNotFound();
            }
            ViewBag.ClinicId = new SelectList(db.Clinics, "Id", "MedicalInstitutionName", cabinet.ClinicId);
            return View(cabinet);
        }

        // POST: Cabinets/Edit/5
        // Чтобы защититься от атак чрезмерной передачи данных, включите определенные свойства, для которых следует установить привязку. Дополнительные 
        // сведения см. в статье https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit([Bind(Include = "Id,Number,ClinicId")] Cabinet cabinet)
        {
            if (ModelState.IsValid)
            {
                db.Entry(cabinet).State = EntityState.Modified;
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            ViewBag.ClinicId = new SelectList(db.Clinics, "Id", "MedicalInstitutionName", cabinet.ClinicId);
            return View(cabinet);
        }

        // GET: Cabinets/Delete/5
        public async Task<ActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Cabinet cabinet = await db.Cabinets.FindAsync(id);
            if (cabinet == null)
            {
                return HttpNotFound();
            }
            return View(cabinet);
        }

        // POST: Cabinets/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> DeleteConfirmed(int id)
        {
            Cabinet cabinet = await db.Cabinets.FindAsync(id);
            db.Cabinets.Remove(cabinet);
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
