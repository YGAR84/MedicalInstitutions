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
	public class WardsController : Controller
    {
        private MedicalInstitutionsContext db = new MedicalInstitutionsContext();

		// GET: Wards
		public ActionResult Index(int? pageNum)
		{
			int sizeOfPage = 4;
			int numOfPage = (pageNum ?? 1);

			var wards = db.Wards
	            .Include(w => w.HospitalDepartment)
	            .OrderBy(l => l.Id);
			return View(wards.ToPagedList(numOfPage, sizeOfPage));
        }

		// GET: Wards/Details/5
		public async Task<ActionResult> Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Ward ward = await db.Wards.FindAsync(id);
            if (ward == null)
            {
                return HttpNotFound();
            }
            return View(ward);
        }

        // GET: Wards/Create
        public ActionResult Create()
        {
            ViewBag.HospitalDepartmentId = new SelectList(db.HospitalDepartments.ToList().Select(hd => new { hd.Id, Name = hd.DepartmentName }), "Id", "Name");
            return View();
        }

        // POST: Wards/Create
        // Чтобы защититься от атак чрезмерной передачи данных, включите определенные свойства, для которых следует установить привязку. Дополнительные 
        // сведения см. в статье https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create([Bind(Include = "Id,Number,NumOfBeds,HospitalDepartmentId")] Ward ward)
        {
            if (ModelState.IsValid)
            {
                db.Wards.Add(ward);
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }

            ViewBag.HospitalDepartmentId = new SelectList(db.HospitalDepartments.ToList().Select(hd => new { hd.Id, Name = hd.DepartmentName }), "Id", "Name");
			return View(ward);
        }

        // GET: Wards/Edit/5
        public async Task<ActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Ward ward = await db.Wards.FindAsync(id);
            if (ward == null)
            {
                return HttpNotFound();
            }
            ViewBag.HospitalDepartmentId = new SelectList(db.HospitalDepartments.ToList().Select(hd => new { hd.Id, Name = hd.DepartmentName }), "Id", "Name");
			return View(ward);
        }

        // POST: Wards/Edit/5
        // Чтобы защититься от атак чрезмерной передачи данных, включите определенные свойства, для которых следует установить привязку. Дополнительные 
        // сведения см. в статье https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit([Bind(Include = "Id,Number,NumOfBeds,HospitalDepartmentId")] Ward ward)
        {
            if (ModelState.IsValid)
            {
                db.Entry(ward).State = EntityState.Modified;
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            ViewBag.HospitalDepartmentId = new SelectList(db.HospitalDepartments.ToList().Select(hd => new { hd.Id, Name = hd.DepartmentName }), "Id", "Name");
			return View(ward);
        }

        // GET: Wards/Delete/5
        public async Task<ActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Ward ward = await db.Wards.FindAsync(id);
            if (ward == null)
            {
                return HttpNotFound();
            }
            return View(ward);
        }

        // POST: Wards/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> DeleteConfirmed(int id)
        {
            Ward ward = await db.Wards.FindAsync(id);
            db.Wards.Remove(ward);
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
