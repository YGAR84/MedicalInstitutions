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
using MedicalInstitutions.Models.Patients.Visit;
using PagedList;

namespace MedicalInstitutions.Controllers
{
	[Authorize(Roles = "Admin,Doctor")]
	[OutputCache(CacheProfile = "defaultCacheProfile")]
	public class ClinicVisitsController : Controller
	{
		private MedicalInstitutionsContext db = new MedicalInstitutionsContext();

		// GET: ClinicVisits
		public ActionResult Index(int? pageNum)
		{
			int sizeOfPage = 4;
			int numOfPage = (pageNum ?? 1);
			var medicalInstitutionVisits = db.ClinicVisits
				.Include(c => c.Disease)
				.Include(c => c.Doctor)
				.Include(c => c.Patient)
				.Include(c => c.Cabinet
				).OrderBy(i => i.Id);
			return View(medicalInstitutionVisits.ToPagedList(numOfPage, sizeOfPage));
		}

		// GET: ClinicVisits/Details/5
		public async Task<ActionResult> Details(int? id)
		{
			if (id == null)
			{
				return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
			}
			ClinicVisit clinicVisit = await db.ClinicVisits.FindAsync(id);
			if (clinicVisit == null)
			{
				return HttpNotFound();
			}
			return View(clinicVisit);
		}

		// GET: ClinicVisits/Create
		public ActionResult Create()
		{
			ViewBag.DiseaseId = new SelectList(db.Diseases, "Id", "DiseaseName");

			ViewBag.PatientId = new SelectList(db.Patients.ToList().Select(p => new { p.Id, Name = p.FullName }), "Id", "Name");
			ViewBag.CabinetId = new SelectList(db.Cabinets.Include(c => c.Clinic).ToList().Select(c => new {c.Id, Name = c.CabinetName}), "Id", "Name");
			ViewBag.DoctorId = new SelectList(" ");
			//			ViewBag.DoctorId = new SelectList(db.MedicalStaffs.Include(ms => ms.Profile).ToList().Select(ms => new { ms.Id, Name = ms.MedicalStaffName }), "Id", "Name");
			return View();
		}

		// POST: ClinicVisits/Create
		// Чтобы защититься от атак чрезмерной передачи данных, включите определенные свойства, для которых следует установить привязку. Дополнительные 
		// сведения см. в статье https://go.microsoft.com/fwlink/?LinkId=317598.
		[HttpPost]
		[ValidateAntiForgeryToken]
		public async Task<ActionResult> Create([Bind(Include = "Id,PatientId,DoctorId,VisitDate,DiseaseId,CabinetId")] ClinicVisit clinicVisit)
		{
			if (ModelState.IsValid)
			{
				db.ClinicVisits.Add(clinicVisit);
				await db.SaveChangesAsync();
				return RedirectToAction("Index");
			}

			ViewBag.DiseaseId = new SelectList(db.Diseases, "Id", "DiseaseName", clinicVisit.DiseaseId);

			ViewBag.PatientId = new SelectList(db.Patients.ToList().Select(p => new { p.Id, Name = p.FullName }), "Id", "Name", clinicVisit.PatientId);
			ViewBag.CabinetId = new SelectList(db.Cabinets.Include(c => c.Clinic).ToList().Select(c => new { c.Id, Name = c.CabinetName }), "Id", "Name", clinicVisit.CabinetId);

			//ViewBag.DoctorId = new SelectList(db.MedicalStaffs.Include(ms => ms.Profile).ToList().Select(ms => new { ms.Id, Name = ms.MedicalStaffName }), "Id", "Name", clinicVisit.DoctorId);
			ViewBag.DoctorId = GetDoctorsByDiseaseAndHospital(clinicVisit.CabinetId, clinicVisit.DiseaseId,
				clinicVisit.VisitDate, clinicVisit.DoctorId);
			return View(clinicVisit);
		}

		public JsonResult GetDoctorList(int? cabinetId, int? diseaseId, DateTime? visitDate)
		{
			var medicalStaffId = GetDoctorsByDiseaseAndHospital(cabinetId, diseaseId, visitDate, null);
			return Json(medicalStaffId, JsonRequestBehavior.AllowGet);
		}

		private SelectList GetDoctorsByDiseaseAndHospital(int? cabinetId, int? diseaseId, DateTime? visitDate, int? doctorId)
		{
			if (cabinetId == null || diseaseId == null || visitDate == null)
			{
				return null;
			}

			db.Configuration.ProxyCreationEnabled = false;

			var disease = db.Diseases.First(d => d.Id == diseaseId);
			var clinicId = db.Cabinets
				.Where(c => c.Id == cabinetId)
				.Select(c => c.ClinicId)
				.First();
;

			var medicalStaff = db.MedicalStaffs
				.Include(ms => ms.Profile)
				.Where(ms => ms.Profile.DiseaseGroupId == disease.DiseaseGroupId)
				.Where(ms => ms.MedicalStaffEmployments
					.Any(mse => mse.MedicalInstitutionId == clinicId
								&& mse.EmploymentDate <= visitDate
								&& (mse.DischargeDate == null || mse.DischargeDate >= visitDate)))
				.ToList();

			if (doctorId != null)
			{
				return new SelectList(medicalStaff.Select(ms => new { ms.Id, Name = ms.MedicalStaffName }), "Id", "Name", doctorId);
			}

			return new SelectList(medicalStaff.Select(ms => new { ms.Id, Name = ms.MedicalStaffName }), "Id", "Name");

		}

		// GET: ClinicVisits/Edit/5
		public async Task<ActionResult> Edit(int? id)
		{
			if (id == null)
			{
				return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
			}
			ClinicVisit clinicVisit = await db.ClinicVisits.FindAsync(id);
			if (clinicVisit == null)
			{
				return HttpNotFound();
			}
			ViewBag.DiseaseId = new SelectList(db.Diseases, "Id", "DiseaseName", clinicVisit.DiseaseId);
			//ViewBag.DoctorId = new SelectList(db.MedicalStaffs.Include(ms => ms.Profile).ToList().Select(ms => new { ms.Id, Name = ms.MedicalStaffName }), "Id", "Name", clinicVisit.DoctorId);
			ViewBag.DoctorId = GetDoctorsByDiseaseAndHospital(clinicVisit.CabinetId, clinicVisit.DiseaseId,
				clinicVisit.VisitDate, clinicVisit.DoctorId);
			ViewBag.PatientId = new SelectList(db.Patients.ToList().Select(p => new { p.Id, Name = p.FullName }), "Id", "Name", clinicVisit.PatientId);
			ViewBag.CabinetId = new SelectList(db.Cabinets.Include(c => c.Clinic).ToList().Select(c => new { c.Id, Name = c.CabinetName }), "Id", "Name", clinicVisit.CabinetId);
			return View(clinicVisit);
		}

		// POST: ClinicVisits/Edit/5
		// Чтобы защититься от атак чрезмерной передачи данных, включите определенные свойства, для которых следует установить привязку. Дополнительные 
		// сведения см. в статье https://go.microsoft.com/fwlink/?LinkId=317598.
		[HttpPost]
		[ValidateAntiForgeryToken]
		public async Task<ActionResult> Edit([Bind(Include = "Id,PatientId,DoctorId,VisitDate,DiseaseId,CabinetId")] ClinicVisit clinicVisit)
		{
			if (ModelState.IsValid)
			{
				db.Entry(clinicVisit).State = EntityState.Modified;
				await db.SaveChangesAsync();
				return RedirectToAction("Index");
			}
			ViewBag.DiseaseId = new SelectList(db.Diseases, "Id", "DiseaseName", clinicVisit.DiseaseId);
			//ViewBag.DoctorId = new SelectList(db.MedicalStaffs.Include(ms => ms.Profile).ToList().Select(ms => new { ms.Id, Name = ms.MedicalStaffName }), "Id", "Name", clinicVisit.DoctorId);
			ViewBag.DoctorId = GetDoctorsByDiseaseAndHospital(clinicVisit.CabinetId, clinicVisit.DiseaseId,
				clinicVisit.VisitDate, clinicVisit.DoctorId);
			ViewBag.PatientId = new SelectList(db.Patients.ToList().Select(p => new { p.Id, Name = p.FullName}), "Id", "Name", clinicVisit.PatientId);
			ViewBag.CabinetId = new SelectList(db.Cabinets.Include(c => c.Clinic).ToList().Select(c => new { c.Id, Name = c.CabinetName }), "Id", "Name", clinicVisit.CabinetId);
			return View(clinicVisit);
		}

		// GET: ClinicVisits/Delete/5
		public async Task<ActionResult> Delete(int? id)
		{
			if (id == null)
			{
				return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
			}
			ClinicVisit clinicVisit = await db.ClinicVisits.FindAsync(id);
			if (clinicVisit == null)
			{
				return HttpNotFound();
			}
			return View(clinicVisit);
		}

		// POST: ClinicVisits/Delete/5
		[HttpPost, ActionName("Delete")]
		[ValidateAntiForgeryToken]
		public async Task<ActionResult> DeleteConfirmed(int id)
		{
			ClinicVisit clinicVisit = await db.ClinicVisits.FindAsync(id);
			db.ClinicVisits.Remove(clinicVisit);
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
