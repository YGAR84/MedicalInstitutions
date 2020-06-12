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
using MedicalInstitutions.Models.Patients.Visit;
using PagedList;

namespace MedicalInstitutions.Controllers
{
	[Authorize(Roles = "Admin,Doctor")]
	[OutputCache(CacheProfile = "defaultCacheProfile")]
	public class HospitalVisitsController : Controller
    {
        private MedicalInstitutionsContext db = new MedicalInstitutionsContext();

		// GET: HospitalVisits
		public ActionResult Index(int? pageNum)
		{
			int sizeOfPage = 4;
			int numOfPage = (pageNum ?? 1);
			var medicalInstitutionVisits = db.HospitalVisits
				.Include(h => h.Disease)
				.Include(h => h.Doctor)
				.Include(h => h.Patient)
				.Include(h => h.Ward)
				.OrderBy(i => i.Id);
			return View(medicalInstitutionVisits.ToPagedList(numOfPage, sizeOfPage));
        }

		// GET: HospitalVisits/Details/5
		public async Task<ActionResult> Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            HospitalVisit hospitalVisit = await db.HospitalVisits.FindAsync(id);
            if (hospitalVisit == null)
            {
                return HttpNotFound();
            }
            return View(hospitalVisit);
        }

        // GET: HospitalVisits/Create
        public ActionResult Create()
        {
			ViewBag.DiseaseId = new SelectList(db.Diseases, "Id", "DiseaseName");
			//ViewBag.DoctorId = new SelectList(db.MedicalStaffs.Include(ms => ms.Profile).ToList().Select(ms => new { ms.Id, Name = ms.MedicalStaffName }), "Id", "Name");
			ViewBag.DoctorId = new SelectList(" ");
			ViewBag.PatientId = new SelectList(db.Patients.ToList().Select(p => new { p.Id, Name = p.FullName }), "Id", "Name");
			//ViewBag.WardId = new SelectList(db.Wards.Include(w => w.HospitalDepartment).ToList().Select(w => new { w.Id, Name = w.WardName }), "Id", "Name");
			ViewBag.WardId = new SelectList(" ");
			return View();
        }

        // POST: HospitalVisits/Create
        // Чтобы защититься от атак чрезмерной передачи данных, включите определенные свойства, для которых следует установить привязку. Дополнительные 
        // сведения см. в статье https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create([Bind(Include = "Id,PatientId,DoctorId,VisitDate,DiseaseId,WardId,VisitEndDate,Temperature,PatientCondition")] HospitalVisit hospitalVisit)
        {
            if (ModelState.IsValid)
            {
                db.HospitalVisits.Add(hospitalVisit);
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }


			ViewBag.DiseaseId = new SelectList(db.Diseases, "Id", "DiseaseName");
			//ViewBag.DoctorId = new SelectList(db.MedicalStaffs.Include(ms => ms.Profile).ToList().Select(ms => new { ms.Id, Name = ms.MedicalStaffName }), "Id", "Name");
			//ViewBag.WardId = new SelectList(db.Wards.Include(w => w.HospitalDepartment).ToList().Select(w => new { w.Id, Name = w.WardName }), "Id", "Name");
			ViewBag.WardId = GetWardsByDisease(hospitalVisit.DiseaseId, hospitalVisit.WardId);
			ViewBag.DoctorId = GetDoctorsByDiseaseAndHospital(hospitalVisit.WardId, hospitalVisit.DiseaseId, hospitalVisit.VisitDate, hospitalVisit.VisitEndDate, hospitalVisit.DoctorId);
			ViewBag.PatientId = new SelectList(db.Patients.ToList().Select(p => new { p.Id, Name = p.FullName }), "Id", "Name");

			return View(hospitalVisit);
        }

        // GET: HospitalVisits/Edit/5
        public async Task<ActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            HospitalVisit hospitalVisit = await db.HospitalVisits.FindAsync(id);
            if (hospitalVisit == null)
            {
                return HttpNotFound();
            }


            ViewBag.WardId = GetWardsByDisease(hospitalVisit.DiseaseId, hospitalVisit.WardId);
            ViewBag.DoctorId = GetDoctorsByDiseaseAndHospital(hospitalVisit.WardId, hospitalVisit.DiseaseId, hospitalVisit.VisitDate, hospitalVisit.VisitEndDate, hospitalVisit.DoctorId);
			ViewBag.PatientId = new SelectList(db.Patients.ToList().Select(p => new { p.Id, Name = p.FullName }), "Id", "Name", hospitalVisit.PatientId);
			ViewBag.DiseaseId = new SelectList(db.Diseases, "Id", "DiseaseName", hospitalVisit.DiseaseId);
			//ViewBag.DoctorId = new SelectList(db.MedicalStaffs.Include(ms => ms.Profile).ToList().Select(ms => new { ms.Id, Name = ms.MedicalStaffName }), "Id", "Name", hospitalVisit.DoctorId);
			//ViewBag.WardId = new SelectList(db.Wards.Include(w => w.HospitalDepartment).ToList().Select(w => new { w.Id, Name = w.WardName }), "Id", "Name", hospitalVisit.WardId);

			return View(hospitalVisit);
        }

        // POST: HospitalVisits/Edit/5
        // Чтобы защититься от атак чрезмерной передачи данных, включите определенные свойства, для которых следует установить привязку. Дополнительные 
        // сведения см. в статье https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit([Bind(Include = "Id,PatientId,DoctorId,VisitDate,DiseaseId,WardId,VisitEndDate,Temperature,PatientCondition")] HospitalVisit hospitalVisit)
        {
            if (ModelState.IsValid)
            {
                db.Entry(hospitalVisit).State = EntityState.Modified;
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }



            ViewBag.WardId = GetWardsByDisease(hospitalVisit.DiseaseId, hospitalVisit.WardId);
            ViewBag.DoctorId = GetDoctorsByDiseaseAndHospital(hospitalVisit.WardId, hospitalVisit.DiseaseId, hospitalVisit.VisitDate, hospitalVisit.VisitEndDate, hospitalVisit.DoctorId);

			//ViewBag.DoctorId = new SelectList(db.MedicalStaffs.Include(ms => ms.Profile).ToList().Select(ms => new { ms.Id, Name = ms.MedicalStaffName }), "Id", "Name", hospitalVisit.DoctorId);
			ViewBag.DiseaseId = new SelectList(db.Diseases, "Id", "DiseaseName", hospitalVisit.DiseaseId);
			ViewBag.PatientId = new SelectList(db.Patients.ToList().Select(p => new { p.Id, Name = p.FullName  }), "Id", "Name", hospitalVisit.PatientId); 
			//ViewBag.WardId = new SelectList(db.Wards.Include(w => w.HospitalDepartment).ToList().Select(w => new { w.Id, Name = w.WardName }), "Id", "Name", hospitalVisit.WardId);
			return View(hospitalVisit);
        }

        public JsonResult GetDoctorList(int? wardId, int? diseaseId, DateTime? visitDate, DateTime? visitEndDate)
        {
	        var medicalStaffId = GetDoctorsByDiseaseAndHospital(wardId, diseaseId, visitDate, visitEndDate, null);
	        return Json(medicalStaffId, JsonRequestBehavior.AllowGet);
        }

        private SelectList GetDoctorsByDiseaseAndHospital(int? wardId, int? diseaseId, DateTime? visitDate, DateTime? visitEndDate, int? doctorId)
        {
	        if (wardId == null || diseaseId == null || visitDate == null)
	        {
		        return null;
	        }

	        db.Configuration.ProxyCreationEnabled = false;

	        var disease = db.Diseases.First(d => d.Id == diseaseId);
	        var hospitalId = db.Wards
		        .Include(w => w.HospitalDepartment)
		        .Include(w => w.HospitalDepartment.HospitalBuilding)
		        .Where(w => w.Id == wardId)
		        .Select(w => w.HospitalDepartment.HospitalBuilding.HospitalId).First();

	        var medicalStaff = db.MedicalStaffs
		        .Include(ms => ms.Profile)
		        .Where(ms => ms.Profile.DiseaseGroupId == disease.DiseaseGroupId)
		        .Where(ms => ms.MedicalStaffEmployments
			        .Any(mse => mse.MedicalInstitutionId == hospitalId
			                    && mse.EmploymentDate <= visitDate
			                    && (mse.DischargeDate == null ||
			                        (visitEndDate != null && mse.DischargeDate >= visitEndDate))))
		        .ToList();

	        if (doctorId != null)
	        {
		        return new SelectList(medicalStaff.Select(ms => new { ms.Id, Name = ms.MedicalStaffName }), "Id", "Name", doctorId);
			}

			return new SelectList(medicalStaff.Select(ms => new { ms.Id, Name = ms.MedicalStaffName}), "Id", "Name");

        }

		public JsonResult GetWardsList(int? diseaseId)
		{
			var wardId = GetWardsByDisease(diseaseId, null);
			return Json(wardId, JsonRequestBehavior.AllowGet);
		}

		private SelectList GetWardsByDisease(int? diseaseId, int? wardId)
		{
			if (diseaseId == null)
			{
				return null;
			}

			db.Configuration.ProxyCreationEnabled = false;

			var disease = db.Diseases.First(d => d.Id == diseaseId);

			var wards = db.Wards
				.Include(w => w.HospitalDepartment)
				.Include(w => w.HospitalDepartment.HospitalBuilding)
				.Include(w => w.HospitalDepartment.HospitalBuilding.Hospital)
				.Where(w => w.HospitalDepartment.DiseaseGroupId == disease.DiseaseGroupId)
				.ToList();

			var resultWards = wards.Select(w => new { w.Id, Name = w.WardName });

			if (wardId != null)
			{
				return new SelectList(db.Wards
						.Include(w => w.HospitalDepartment)
						.Include(w => w.HospitalDepartment.DiseaseGroup)
						.Where(w => w.HospitalDepartment.DiseaseGroupId == disease.DiseaseGroupId)
						.ToList().Select(w => new { w.Id, Name = w.WardName }),
					"Id",
					"Name",
					wardId);
			}

			return new SelectList(db.Wards
					.Include(w => w.HospitalDepartment)
					.Include(w => w.HospitalDepartment.DiseaseGroup)
					.Where(w => w.HospitalDepartment.DiseaseGroupId == disease.DiseaseGroupId)
					.ToList().Select(w => new { w.Id, Name = w.WardName }),
				"Id", 
				"Name");
		}

		// GET: HospitalVisits/Delete/5
		public async Task<ActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            HospitalVisit hospitalVisit = await db.HospitalVisits.FindAsync(id);
            if (hospitalVisit == null)
            {
                return HttpNotFound();
            }
            return View(hospitalVisit);
        }

        // POST: HospitalVisits/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> DeleteConfirmed(int id)
        {
            HospitalVisit hospitalVisit = await db.HospitalVisits.FindAsync(id);
            db.HospitalVisits.Remove(hospitalVisit);
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
