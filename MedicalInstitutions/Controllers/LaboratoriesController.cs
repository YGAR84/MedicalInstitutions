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
	public class LaboratoriesController : Controller
    {
        private MedicalInstitutionsContext db = new MedicalInstitutionsContext();

        // GET: Laboratories
        public ActionResult Index(int? pageNum)
        {
	        int sizeOfPage = 4;
	        int numOfPage = (pageNum ?? 1);
	        var labs = db.Laboratories.Include(l => l.LaboratorySpecializations).OrderBy(l => l.Id);
            return View(labs.ToPagedList(numOfPage, sizeOfPage));
        }

        // GET: Laboratories/Details/5
        public async Task<ActionResult> Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Laboratory laboratory = await db.Laboratories.Include(l => l.LaboratorySpecializations).FirstOrDefaultAsync(l => l.Id == id);
            if (laboratory == null)
            {
                return HttpNotFound();
            }
            return View(laboratory);
        }

        // GET: Laboratories/Create
        public async Task<ActionResult> Create()
        {
	        ViewBag.LaboratorySpecializations = await db.LaboratorySpecializations.ToListAsync();
			return View();
        }

        // POST: Laboratories/Create
        // Чтобы защититься от атак чрезмерной передачи данных, включите определенные свойства, для которых следует установить привязку. Дополнительные 
        // сведения см. в статье https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create([Bind(Include = "Id,Address,selectedSpecializations")] Laboratory laboratory, int[] selectedSpecializations)
        {
	        if (ModelState.IsValid)
            {
	            
				if (selectedSpecializations != null)
	            {
		            laboratory.LaboratorySpecializations.Clear();
					foreach (var ls in db.LaboratorySpecializations.Where(ls => selectedSpecializations.Contains(ls.Id)))
		            {
			            laboratory.LaboratorySpecializations.Add(ls);
		            }
	            }

	            db.Laboratories.Add(laboratory);

                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }

            return View(laboratory);
        }

        // GET: Laboratories/Edit/5
        public async Task<ActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Laboratory laboratory = await db.Laboratories.FindAsync(id);
            if (laboratory == null)
            {
                return HttpNotFound();
            }

            ViewBag.LaboratorySpecializations = await db.LaboratorySpecializations.ToListAsync();
            return View(laboratory);
        }

        // POST: Laboratories/Edit/5
        // Чтобы защититься от атак чрезмерной передачи данных, включите определенные свойства, для которых следует установить привязку. Дополнительные 
        // сведения см. в статье https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit([Bind(Include = "Id,Address,selectedSpecializations")] Laboratory laboratory, int[] selectedSpecializations)
        {
	        if (ModelState.IsValid)
            {
	            db.Entry(laboratory).State = EntityState.Modified;

	            db.Entry(laboratory).Collection(l => l.LaboratorySpecializations).Load();
	            laboratory.LaboratorySpecializations.Clear();
				if (selectedSpecializations != null)
	            {
		            foreach (var ls in db.LaboratorySpecializations.Where(ls => selectedSpecializations.Contains(ls.Id)))
		            {
						laboratory.LaboratorySpecializations.Add(ls);
		            }
	            }
                
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            return View(laboratory);
        }

        // GET: Laboratories/Delete/5
        public async Task<ActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Laboratory laboratory = await db.Laboratories.FindAsync(id);
            if (laboratory == null)
            {
                return HttpNotFound();
            }
            return View(laboratory);
        }

        // POST: Laboratories/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> DeleteConfirmed(int id)
        {
            Laboratory laboratory = await db.Laboratories.FindAsync(id);
            db.Laboratories.Remove(laboratory);
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
