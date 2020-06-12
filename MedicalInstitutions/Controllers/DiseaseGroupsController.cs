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
using MedicalInstitutions.Models.Diseases;
using PagedList;

namespace MedicalInstitutions.Controllers
{
	[Authorize(Roles = "Admin")]
	[OutputCache(CacheProfile = "defaultCacheProfile")]
	public class DiseaseGroupsController : Controller
    {
        private MedicalInstitutionsContext db = new MedicalInstitutionsContext();

        // GET: DiseaseGroups
         public ActionResult Index(int? pageNum)
        {
	        int sizeOfPage = 4;
	        int numOfPage = (pageNum ?? 1);
	        var diseaseGroups = db.DiseaseGroups.OrderBy(i => i.Id);
	        return View(diseaseGroups.ToPagedList(numOfPage, sizeOfPage));
        }

         // GET: DiseaseGroups/Details/5
		public async Task<ActionResult> Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            DiseaseGroup diseaseGroup = await db.DiseaseGroups.FindAsync(id);
            if (diseaseGroup == null)
            {
                return HttpNotFound();
            }
            return View(diseaseGroup);
        }

        // GET: DiseaseGroups/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: DiseaseGroups/Create
        // Чтобы защититься от атак чрезмерной передачи данных, включите определенные свойства, для которых следует установить привязку. Дополнительные 
        // сведения см. в статье https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create([Bind(Include = "Id,Name")] DiseaseGroup diseaseGroup)
        {
            if (ModelState.IsValid)
            {
                db.DiseaseGroups.Add(diseaseGroup);
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }

            return View(diseaseGroup);
        }

        // GET: DiseaseGroups/Edit/5
        public async Task<ActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            DiseaseGroup diseaseGroup = await db.DiseaseGroups.FindAsync(id);
            if (diseaseGroup == null)
            {
                return HttpNotFound();
            }
            return View(diseaseGroup);
        }

        // POST: DiseaseGroups/Edit/5
        // Чтобы защититься от атак чрезмерной передачи данных, включите определенные свойства, для которых следует установить привязку. Дополнительные 
        // сведения см. в статье https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit([Bind(Include = "Id,Name")] DiseaseGroup diseaseGroup)
        {
            if (ModelState.IsValid)
            {
                db.Entry(diseaseGroup).State = EntityState.Modified;
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            return View(diseaseGroup);
        }

        // GET: DiseaseGroups/Delete/5
        public async Task<ActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            DiseaseGroup diseaseGroup = await db.DiseaseGroups.FindAsync(id);
            if (diseaseGroup == null)
            {
                return HttpNotFound();
            }
            return View(diseaseGroup);
        }

        // POST: DiseaseGroups/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> DeleteConfirmed(int id)
        {
            DiseaseGroup diseaseGroup = await db.DiseaseGroups.FindAsync(id);
            db.DiseaseGroups.Remove(diseaseGroup);
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
