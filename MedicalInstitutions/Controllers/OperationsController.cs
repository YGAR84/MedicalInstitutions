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
using MedicalInstitutions.Models.Patients;
using MedicalInstitutions.Models.Patients.Visit;
using MedicalInstitutions.Models.Staffs.MedicalStaffs;
using PagedList;

namespace MedicalInstitutions.Controllers
{
	[Authorize(Roles = "Admin,Doctor")]
	[OutputCache(CacheProfile = "defaultCacheProfile")]
	public class OperationsController : Controller
	{
		private MedicalInstitutionsContext db = new MedicalInstitutionsContext();

		private class MedicalInstitutionVisitItem
		{
			public int Id { get; set; }
			public string Name { get; set; }
		}

		// GET: Operations
		public ActionResult Index(int? pageNum)
		{
			int sizeOfPage = 4;
			int numOfPage = (pageNum ?? 1);

			var operations = db.Operations
				.Include(o => o.MedicalInstitutionVisit)
				.Include(o => o.Surgeon)
				.Include(o => o.Surgeon.Profile)
				.Where(o => o.Surgeon.Profile.IsSurgeon)
				.OrderBy(l => l.Id);
			return View(operations.ToPagedList(numOfPage, sizeOfPage));
		}

		// GET: Operations/Details/5
		public async Task<ActionResult> Details(int? id)
		{
			if (id == null)
			{
				return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
			}
			Operation operation = await db.Operations.FindAsync(id);
			if (operation == null)
			{
				return HttpNotFound();
			}
			return View(operation);
		}

		private IEnumerable<MedicalInstitutionVisitItem> GetHospitalVisits()
		{
			return  db.HospitalVisits
				.Include(hv => hv.Ward)
				.Include(hv => hv.Ward.HospitalDepartment)
				.ToList()
				.Select(hv => new MedicalInstitutionVisitItem  {Id = hv.Id, Name = hv.MedicalInstitutionVisitName });
		}

		private IEnumerable<MedicalInstitutionVisitItem> GetClnicVisits()
		{
			return db.ClinicVisits
				.Include(cv => cv.Cabinet)
				.Include(cv => cv.Cabinet.Clinic)
				.ToList()
				.Select(cv => new MedicalInstitutionVisitItem { Id = cv.Id, Name = cv.MedicalInstitutionVisitName });
		}

		private IEnumerable<MedicalInstitutionVisitItem>  GetMedicalInstitutionVisits()
		{
			return GetClnicVisits().Concat(GetHospitalVisits());
		}

		// GET: Operations/Create
		public ActionResult Create()
		{
			ViewBag.MedicalInstitutionVisitId = new SelectList(GetMedicalInstitutionVisits(), "Id", "Name");
			ViewBag.SurgeonId = new SelectList(" ");
			return View();
		}

		public JsonResult GetSurgeonList(int? medicalItInstitutionVisitId)
		{
			var resultSurgeonId = GetSurgeonSelectList(medicalItInstitutionVisitId);
			return Json(resultSurgeonId, JsonRequestBehavior.AllowGet);
		}

		private SelectList GetSurgeonSelectList(int? medicalItInstitutionVisitId)
		{
			if (medicalItInstitutionVisitId == null)
			{
				return null;
			}

			db.Configuration.ProxyCreationEnabled = false;
			var medicalInstitutionVisit =
				db.MedicalInstitutionVisits.First(miv => miv.Id == medicalItInstitutionVisitId);
			List<MedicalStaff> surgeonList = null;

			if (medicalInstitutionVisit is HospitalVisit hospitalVisit)
			{
				var ward = db.Wards
					.Include(w => w.HospitalDepartment)
					.Include(w => w.HospitalDepartment.HospitalBuilding)
					.First(w => w.Id == hospitalVisit.WardId);

				var hospitalId = ward.HospitalDepartment.HospitalBuilding.HospitalId;

				surgeonList = db.MedicalStaffs
					.Where(ms => ms.MedicalStaffEmployments
						.Any(me => me.EmploymentType == MedicalStaffEmploymentType.StaffDoctor &&
						           me.MedicalInstitutionId == hospitalId &&
						           me.EmploymentDate <= hospitalVisit.VisitDate &&
						           (me.DischargeDate == null || me.DischargeDate >= hospitalVisit.VisitEndDate)))
					.Include(ms => ms.Profile)
					.Where(s => s.Profile.IsSurgeon)
					.ToList();
			}
			else if (medicalInstitutionVisit is ClinicVisit clinicVisit)
			{
				var cabinet = db.Cabinets.First(c => c.Id == clinicVisit.CabinetId);

				var clinicId = cabinet.ClinicId;

				surgeonList = db.MedicalStaffs
					.Where(ms => ms.MedicalStaffEmployments
						.Any(me => me.EmploymentType == MedicalStaffEmploymentType.StaffDoctor &&
						           me.MedicalInstitutionId == clinicId && me.EmploymentDate <= clinicVisit.VisitDate &&
						           (me.DischargeDate == null || me.DischargeDate >= clinicVisit.VisitDate)))
					.Include(ms => ms.Profile)
					.Where(s => s.Profile.IsSurgeon)
					.ToList();
			}

			var resultSurgeonId = new SelectList(surgeonList?.Select(s => new {s.Id, Name = s.MedicalStaffName}), "Id",
				"Name");

			return resultSurgeonId;
		}

		// POST: Operations/Create
		// Чтобы защититься от атак чрезмерной передачи данных, включите определенные свойства, для которых следует установить привязку. Дополнительные 
		// сведения см. в статье https://go.microsoft.com/fwlink/?LinkId=317598.
		[HttpPost]
		[ValidateAntiForgeryToken]
		public async Task<ActionResult> Create([Bind(Include = "Id,MedicalInstitutionVisitId,OperationName,SurgeonId,OperationDate,OperationResult")] Operation operation)
		{
			if (ModelState.IsValid)
			{
				db.Operations.Add(operation);
				await db.SaveChangesAsync();
				return RedirectToAction("Index");
			}

			
			var medicalInstitutionVisits = GetMedicalInstitutionVisits();
			var medicalInstitutionVisitItems = medicalInstitutionVisits.ToList();
			ViewBag.MedicalInstitutionVisitId = new SelectList(medicalInstitutionVisitItems, "Id", "Name", operation.MedicalInstitutionVisitId);
			ViewBag.SurgeonId = GetSurgeonSelectList(medicalInstitutionVisitItems.First()?.Id);
			return View(operation);
		}

		// GET: Operations/Edit/5
		public async Task<ActionResult> Edit(int? id)
		{
			if (id == null)
			{
				return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
			}
			Operation operation = await db.Operations.FindAsync(id);
			if (operation == null)
			{
				return HttpNotFound();
			}
			var medicalInstitutionVisits = GetMedicalInstitutionVisits();
			var medicalInstitutionVisitItems = medicalInstitutionVisits.ToList();
			ViewBag.MedicalInstitutionVisitId = new SelectList(medicalInstitutionVisitItems, "Id", "Name", operation.MedicalInstitutionVisitId);
			//ViewBag.SurgeonId = GetSurgeonSelectList(medicalInstitutionVisitItems.First()?.Id);

			ViewBag.SurgeonId = new SelectList(db.MedicalStaffs.Include(ms => ms.Profile).Where(ms => ms.Profile.IsSurgeon).ToList().Select(ms => new { ms.Id, Name = ms.MedicalStaffName }), "Id", "Name", operation.SurgeonId);
			return View(operation);
		}

		// POST: Operations/Edit/5
		// Чтобы защититься от атак чрезмерной передачи данных, включите определенные свойства, для которых следует установить привязку. Дополнительные 
		// сведения см. в статье https://go.microsoft.com/fwlink/?LinkId=317598.
		[HttpPost]
		[ValidateAntiForgeryToken]
		public async Task<ActionResult> Edit([Bind(Include = "Id,MedicalInstitutionVisitId,OperationName,SurgeonId,OperationDate,OperationResult")] Operation operation)
		{
			if (ModelState.IsValid)
			{
				db.Entry(operation).State = EntityState.Modified;
				await db.SaveChangesAsync();
				return RedirectToAction("Index");
			}
			var medicalInstitutionVisits = GetMedicalInstitutionVisits();
			var medicalInstitutionVisitItems = medicalInstitutionVisits.ToList();
			ViewBag.MedicalInstitutionVisitId = new SelectList(medicalInstitutionVisitItems, "Id", "Name", operation.MedicalInstitutionVisitId);
			//ViewBag.SurgeonId = GetSurgeonSelectList(medicalInstitutionVisitItems.First()?.Id);

			ViewBag.SurgeonId = new SelectList(db.MedicalStaffs.Include(ms => ms.Profile).Where(ms => ms.Profile.IsSurgeon).ToList().Select(ms => new { ms.Id, Name = ms.MedicalStaffName }), "Id", "Name", operation.SurgeonId);
			return View(operation);
		}

		// GET: Operations/Delete/5
		public async Task<ActionResult> Delete(int? id)
		{
			if (id == null)
			{
				return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
			}
			Operation operation = await db.Operations.FindAsync(id);
			if (operation == null)
			{
				return HttpNotFound();
			}
			return View(operation);
		}

		// POST: Operations/Delete/5
		[HttpPost, ActionName("Delete")]
		[ValidateAntiForgeryToken]
		public async Task<ActionResult> DeleteConfirmed(int id)
		{
			Operation operation = await db.Operations.FindAsync(id);
			db.Operations.Remove(operation);
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
