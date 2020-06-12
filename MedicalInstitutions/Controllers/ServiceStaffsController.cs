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
using MedicalInstitutions.Models.Staffs.ServiceStaffs;
using PagedList;

namespace MedicalInstitutions.Controllers
{
	[Authorize(Roles = "Admin,HR")]
	[OutputCache(CacheProfile = "defaultCacheProfile")]
	public class ServiceStaffsController : Controller
    {
        private MedicalInstitutionsContext db = new MedicalInstitutionsContext();

		// GET: ServiceStaffs
		public ActionResult Index(int? pageNum)
		{
			int sizeOfPage = 4;
			int numOfPage = (pageNum ?? 1);

			var serviceStaffs = db.ServiceStaffs
				.Include(s => s.Specialty)
				.OrderBy(l => l.Id);
			return View(serviceStaffs.ToPagedList(numOfPage, sizeOfPage));
        }

		// GET: ServiceStaffs/Details/5
		public async Task<ActionResult> Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ServiceStaff serviceStaff = await db.ServiceStaffs.FindAsync(id);
            if (serviceStaff == null)
            {
                return HttpNotFound();
            }
            return View(serviceStaff);
        }

        // GET: ServiceStaffs/Create
        public ActionResult Create()
        {
            ViewBag.SpecialtyId = new SelectList(db.ServiceStaffSpecialties, "Id", "SpecialtyName");
            return View();
        }

        // POST: ServiceStaffs/Create
        // Чтобы защититься от атак чрезмерной передачи данных, включите определенные свойства, для которых следует установить привязку. Дополнительные 
        // сведения см. в статье https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create([Bind(Include = "Id,SpecialtyId,FirstName,SecondName")] ServiceStaff serviceStaff)
        {
            if (ModelState.IsValid)
            {
                db.ServiceStaffs.Add(serviceStaff);
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }

            ViewBag.SpecialtyId = new SelectList(db.ServiceStaffSpecialties, "Id", "SpecialtyName", serviceStaff.SpecialtyId);
            return View(serviceStaff);
        }

        // GET: ServiceStaffs/Edit/5
        public async Task<ActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ServiceStaff serviceStaff = await db.ServiceStaffs.FindAsync(id);
            if (serviceStaff == null)
            {
                return HttpNotFound();
            }
            ViewBag.SpecialtyId = new SelectList(db.ServiceStaffSpecialties, "Id", "SpecialtyName", serviceStaff.SpecialtyId);
            return View(serviceStaff);
        }

        // POST: ServiceStaffs/Edit/5
        // Чтобы защититься от атак чрезмерной передачи данных, включите определенные свойства, для которых следует установить привязку. Дополнительные 
        // сведения см. в статье https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit([Bind(Include = "Id,SpecialtyId,FirstName,SecondName")] ServiceStaff serviceStaff)
        {
            if (ModelState.IsValid)
            {
                db.Entry(serviceStaff).State = EntityState.Modified;
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            ViewBag.SpecialtyId = new SelectList(db.ServiceStaffSpecialties, "Id", "SpecialtyName", serviceStaff.SpecialtyId);
            return View(serviceStaff);
        }

        // GET: ServiceStaffs/Delete/5
        public async Task<ActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ServiceStaff serviceStaff = await db.ServiceStaffs.FindAsync(id);
            if (serviceStaff == null)
            {
                return HttpNotFound();
            }
            return View(serviceStaff);
        }

        // POST: ServiceStaffs/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> DeleteConfirmed(int id)
        {
            ServiceStaff serviceStaff = await db.ServiceStaffs.FindAsync(id);
            db.ServiceStaffs.Remove(serviceStaff);
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
