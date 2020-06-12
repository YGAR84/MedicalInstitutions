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
	[Authorize(Roles = "Admin")]
	[OutputCache(CacheProfile = "defaultCacheProfile")]
	public class ServiceStaffSpecialtiesController : Controller
    {
        private MedicalInstitutionsContext db = new MedicalInstitutionsContext();

		// GET: ServiceStaffSpecialties
		public ActionResult Index(int? pageNum)
		{
			int sizeOfPage = 4;
			int numOfPage = (pageNum ?? 1);
			var serviceStaffSpecialties = db.ServiceStaffSpecialties
				.OrderBy(l => l.Id);
			return View(serviceStaffSpecialties.ToPagedList(numOfPage, sizeOfPage));
        }


		// GET: ServiceStaffSpecialties/Details/5
		public async Task<ActionResult> Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ServiceStaffSpecialty serviceStaffSpecialty = await db.ServiceStaffSpecialties.FindAsync(id);
            if (serviceStaffSpecialty == null)
            {
                return HttpNotFound();
            }
            return View(serviceStaffSpecialty);
        }

        // GET: ServiceStaffSpecialties/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: ServiceStaffSpecialties/Create
        // Чтобы защититься от атак чрезмерной передачи данных, включите определенные свойства, для которых следует установить привязку. Дополнительные 
        // сведения см. в статье https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create([Bind(Include = "Id,SpecialtyName")] ServiceStaffSpecialty serviceStaffSpecialty)
        {
            if (ModelState.IsValid)
            {
                db.ServiceStaffSpecialties.Add(serviceStaffSpecialty);
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }

            return View(serviceStaffSpecialty);
        }

        // GET: ServiceStaffSpecialties/Edit/5
        public async Task<ActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ServiceStaffSpecialty serviceStaffSpecialty = await db.ServiceStaffSpecialties.FindAsync(id);
            if (serviceStaffSpecialty == null)
            {
                return HttpNotFound();
            }
            return View(serviceStaffSpecialty);
        }

        // POST: ServiceStaffSpecialties/Edit/5
        // Чтобы защититься от атак чрезмерной передачи данных, включите определенные свойства, для которых следует установить привязку. Дополнительные 
        // сведения см. в статье https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit([Bind(Include = "Id,SpecialtyName")] ServiceStaffSpecialty serviceStaffSpecialty)
        {
            if (ModelState.IsValid)
            {
                db.Entry(serviceStaffSpecialty).State = EntityState.Modified;
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            return View(serviceStaffSpecialty);
        }

        // GET: ServiceStaffSpecialties/Delete/5
        public async Task<ActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ServiceStaffSpecialty serviceStaffSpecialty = await db.ServiceStaffSpecialties.FindAsync(id);
            if (serviceStaffSpecialty == null)
            {
                return HttpNotFound();
            }
            return View(serviceStaffSpecialty);
        }

        // POST: ServiceStaffSpecialties/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> DeleteConfirmed(int id)
        {
            ServiceStaffSpecialty serviceStaffSpecialty = await db.ServiceStaffSpecialties.FindAsync(id);
            db.ServiceStaffSpecialties.Remove(serviceStaffSpecialty);
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
