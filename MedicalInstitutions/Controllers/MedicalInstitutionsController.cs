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
using MedicalInstitutions.Models.MedicalInstitutions;

namespace MedicalInstitutions.Controllers
{
    public class MedicalInstitutionsController : Controller
    {
        private MedicalInstitutionsContext db = new MedicalInstitutionsContext();

        // GET: MedicalInstitutions
        public async Task<ActionResult> Index()
        {
            return View(await db.MedicalInstitutions.ToListAsync());
        }

        // GET: MedicalInstitutions/Details/5
        public async Task<ActionResult> Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            MedicalInstitution medicalInstitution = await db.MedicalInstitutions.FindAsync(id);
            if (medicalInstitution == null)
            {
                return HttpNotFound();
            }
            return View(medicalInstitution);
        }

        // GET: MedicalInstitutions/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: MedicalInstitutions/Create
        // Чтобы защититься от атак чрезмерной передачи данных, включите определенные свойства, для которых следует установить привязку. Дополнительные 
        // сведения см. в статье https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create([Bind(Include = "Id,MedicalInstitutionName,Address")] MedicalInstitution medicalInstitution)
        {
            if (ModelState.IsValid)
            {
                db.MedicalInstitutions.Add(medicalInstitution);
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }

            return View(medicalInstitution);
        }

        // GET: MedicalInstitutions/Edit/5
        public async Task<ActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            MedicalInstitution medicalInstitution = await db.MedicalInstitutions.FindAsync(id);
            if (medicalInstitution == null)
            {
                return HttpNotFound();
            }
            return View(medicalInstitution);
        }

        // POST: MedicalInstitutions/Edit/5
        // Чтобы защититься от атак чрезмерной передачи данных, включите определенные свойства, для которых следует установить привязку. Дополнительные 
        // сведения см. в статье https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit([Bind(Include = "Id,MedicalInstitutionName,Address")] MedicalInstitution medicalInstitution)
        {
            if (ModelState.IsValid)
            {
                db.Entry(medicalInstitution).State = EntityState.Modified;
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            return View(medicalInstitution);
        }

        // GET: MedicalInstitutions/Delete/5
        public async Task<ActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            MedicalInstitution medicalInstitution = await db.MedicalInstitutions.FindAsync(id);
            if (medicalInstitution == null)
            {
                return HttpNotFound();
            }
            return View(medicalInstitution);
        }

        // POST: MedicalInstitutions/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> DeleteConfirmed(int id)
        {
            MedicalInstitution medicalInstitution = await db.MedicalInstitutions.FindAsync(id);
            db.MedicalInstitutions.Remove(medicalInstitution);
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
