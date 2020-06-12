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
	public class LaboratorySpecializationsController : Controller
    {
        private MedicalInstitutionsContext db = new MedicalInstitutionsContext();

        // GET: LaboratorySpecializations
        public ActionResult Index(int ? pageNum)
        {
	        int sizeOfPage = 4;
	        int numOfPage = (pageNum ?? 1);
	        var laboratorySpecifications = db.LaboratorySpecializations.OrderBy(ls => ls.Id);
			return View(laboratorySpecifications.ToPagedList(numOfPage, sizeOfPage));
        }

        // GET: LaboratorySpecializations/Details/5
        public async Task<ActionResult> Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            LaboratorySpecialization laboratorySpecialization = await db.LaboratorySpecializations.FindAsync(id);
            if (laboratorySpecialization == null)
            {
                return HttpNotFound();
            }
            return View(laboratorySpecialization);
        }

        // GET: LaboratorySpecializations/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: LaboratorySpecializations/Create
        // Чтобы защититься от атак чрезмерной передачи данных, включите определенные свойства, для которых следует установить привязку. Дополнительные 
        // сведения см. в статье https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create([Bind(Include = "Id,Name")] LaboratorySpecialization laboratorySpecialization)
        {
            if (ModelState.IsValid)
            {
                db.LaboratorySpecializations.Add(laboratorySpecialization);
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }

            return View(laboratorySpecialization);
        }

        // GET: LaboratorySpecializations/Edit/5
        public async Task<ActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            LaboratorySpecialization laboratorySpecialization = await db.LaboratorySpecializations.FindAsync(id);
            if (laboratorySpecialization == null)
            {
                return HttpNotFound();
            }
            return View(laboratorySpecialization);
        }

        // POST: LaboratorySpecializations/Edit/5
        // Чтобы защититься от атак чрезмерной передачи данных, включите определенные свойства, для которых следует установить привязку. Дополнительные 
        // сведения см. в статье https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit([Bind(Include = "Id,Name")] LaboratorySpecialization laboratorySpecialization)
        {
            if (ModelState.IsValid)
            {
                db.Entry(laboratorySpecialization).State = EntityState.Modified;
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            return View(laboratorySpecialization);
        }

        // GET: LaboratorySpecializations/Delete/5
        public async Task<ActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            LaboratorySpecialization laboratorySpecialization = await db.LaboratorySpecializations.FindAsync(id);
            if (laboratorySpecialization == null)
            {
                return HttpNotFound();
            }
            return View(laboratorySpecialization);
        }

        // POST: LaboratorySpecializations/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> DeleteConfirmed(int id)
        {
            LaboratorySpecialization laboratorySpecialization = await db.LaboratorySpecializations.FindAsync(id);
            db.LaboratorySpecializations.Remove(laboratorySpecialization);
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
