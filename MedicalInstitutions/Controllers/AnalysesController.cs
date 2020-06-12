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
	[Authorize(Roles = "Admin,Doctor")]
	[OutputCache(CacheProfile = "defaultCacheProfile")]
	public class AnalyzesController : Controller
    {
        private MedicalInstitutionsContext db = new MedicalInstitutionsContext();

        // GET: Analyzes
        public ActionResult Index(int? pageNum)
        {
	        int sizeOfPage = 4;
	        int numOfPage = (pageNum ?? 1);
	        var analyzes = db.Analyzes.Include(a => a.Laboratory).Include(a => a.MedicalInstitutionVisit).OrderBy(a => a.Id);
			return View(analyzes.ToPagedList(numOfPage, sizeOfPage));
		}

        // GET: Analyzes/Details/5
        public async Task<ActionResult> Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Analysis analysis = await db.Analyzes.FindAsync(id);
            if (analysis == null)
            {
                return HttpNotFound();
            }
            return View(analysis);
        }

        // GET: Analyzes/Create
        public ActionResult Create()
        {
            //ViewBag.LaboratoryId = new SelectList(db.Laboratories, "Id", "Address");
            ViewBag.LaboratoryId = new SelectList(db.Laboratories.ToList().Select(l => new { l.Id, Name = l.LaboratoryName }), "Id", "Name");
            ViewBag.MedicalInstitutionVisitId = new SelectList(db.MedicalInstitutionVisits.ToList().Select(miv => new { miv.Id, Name = miv.MedicalInstitutionVisitName }), "Id", "Name");
			//ViewBag.MedicalInstitutionVisitId = new SelectList(db.MedicalInstitutionVisits, "Id", "Id");
            return View();
        }

        // POST: Analyzes/Create
        // Чтобы защититься от атак чрезмерной передачи данных, включите определенные свойства, для которых следует установить привязку. Дополнительные 
        // сведения см. в статье https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create([Bind(Include = "Id,AnalysisName,LaboratoryId,MedicalInstitutionVisitId,AnalysisDate")] Analysis analysis)
        {
            if (ModelState.IsValid)
            {
                db.Analyzes.Add(analysis);
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }

            ViewBag.LaboratoryId = new SelectList(db.Laboratories.ToList().Select(l => new { l.Id, Name = l.LaboratoryName }), "Id", "Name", analysis.LaboratoryId);
            ViewBag.MedicalInstitutionVisitId = new SelectList(db.MedicalInstitutionVisits.ToList().Select(miv => new { miv.Id, Name = miv.MedicalInstitutionVisitName }), "Id", "Name", analysis.MedicalInstitutionVisitId);
			//ViewBag.LaboratoryId = new SelectList(db.Laboratories, "Id", "Address", analysis.LaboratoryId);
            //ViewBag.MedicalInstitutionVisitId = new SelectList(db.MedicalInstitutionVisits, "Id", "Id", analysis.MedicalInstitutionVisitId);
            return View(analysis);
        }

        // GET: Analyzes/Edit/5
        public async Task<ActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Analysis analysis = await db.Analyzes.FindAsync(id);
            if (analysis == null)
            {
                return HttpNotFound();
            }

            ViewBag.LaboratoryId = new SelectList(db.Laboratories.ToList().Select(l => new { l.Id, Name = l.LaboratoryName }), "Id", "Name", analysis.LaboratoryId);
            ViewBag.MedicalInstitutionVisitId = new SelectList(db.MedicalInstitutionVisits.ToList().Select(miv => new { miv.Id, Name = miv.MedicalInstitutionVisitName }), "Id", "Name", analysis.MedicalInstitutionVisitId);
//			ViewBag.LaboratoryId = new SelectList(db.Laboratories, "Id", "Address", analysis.LaboratoryId);
  //          ViewBag.MedicalInstitutionVisitId = new SelectList(db.MedicalInstitutionVisits, "Id", "Id", analysis.MedicalInstitutionVisitId);
            return View(analysis);
        }

        // POST: Analyzes/Edit/5
        // Чтобы защититься от атак чрезмерной передачи данных, включите определенные свойства, для которых следует установить привязку. Дополнительные 
        // сведения см. в статье https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit([Bind(Include = "Id,AnalysisName,LaboratoryId,MedicalInstitutionVisitId,AnalysisDate")] Analysis analysis)
        {
            if (ModelState.IsValid)
            {
                db.Entry(analysis).State = EntityState.Modified;
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            ViewBag.LaboratoryId = new SelectList(db.Laboratories.ToList().Select(l => new { l.Id, Name = l.LaboratoryName }), "Id", "Name", analysis.LaboratoryId);
            ViewBag.MedicalInstitutionVisitId = new SelectList(db.MedicalInstitutionVisits.ToList().Select(miv => new { miv.Id, Name = miv.MedicalInstitutionVisitName }), "Id", "Name", analysis.MedicalInstitutionVisitId);
			//ViewBag.LaboratoryId = new SelectList(db.Laboratories, "Id", "Address", analysis.LaboratoryId);
            //ViewBag.MedicalInstitutionVisitId = new SelectList(db.MedicalInstitutionVisits, "Id", "Id", analysis.MedicalInstitutionVisitId);
            return View(analysis);
        }

        // GET: Analyzes/Delete/5
        public async Task<ActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Analysis analysis = await db.Analyzes.FindAsync(id);
            if (analysis == null)
            {
                return HttpNotFound();
            }
            return View(analysis);
        }

        // POST: Analyzes/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> DeleteConfirmed(int id)
        {
            Analysis analysis = await db.Analyzes.FindAsync(id);
            db.Analyzes.Remove(analysis);
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
