using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using MedicalInstitutions.Models.MedicalInstitutions;
using MedicalInstitutions.Models.MedicalInstitutions.Clinics;
using System.Data.Entity;

namespace MedicalInstitutions.Models.Patients.Visit
{
	public class ClinicVisit : MedicalInstitutionVisit, IValidatableObject
	{
		[Required]
		[DisplayName("Cabinet")]
		public int CabinetId { get; set; }

		public override string MedicalInstitutionVisitName =>
			Patient.FullName + " | " + Cabinet.CabinetName + " | " + base.MedicalInstitutionVisitName;
		
		public virtual Cabinet Cabinet { get; set; }

		private MedicalInstitutionsContext db = new MedicalInstitutionsContext();

		public new IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
		{
			var validationResults = base.Validate(validationContext).ToList();
			if (validationResults.Any())
			{
				yield return validationResults.First();
			}

			var intersectionWithClnics = db.ClinicVisits
				.Where(сv => сv.Id != this.Id)
				.Where(cv => cv.PatientId == this.PatientId).Count(cv =>
					!(cv.VisitDate != this.VisitDate));
			if (intersectionWithClnics != 0)
			{
				yield return new ValidationResult("Visit can not be, because patient has visit to clinic on this dates", new List<string> { "VisitDate" });
			}

			var intersectionWithHospitals = db.HospitalVisits
				.Where(hv => hv.PatientId == this.PatientId).Count(hv =>
					!((hv.VisitEndDate != null && hv.VisitEndDate <= this.VisitDate) ||
					  (hv.VisitDate >= this.VisitDate)));

			if (intersectionWithHospitals != 0)
			{
				yield return new ValidationResult("Visit can not be, because patient has visit to hospital on this dates", new List<string> { "VisitDate" });
			}

			var disease = db.Diseases.First(d => d.Id == this.DiseaseId);
			var doctor = db.MedicalStaffs.Include(d => d.Profile).First(d => d.Id == this.DoctorId);
			if (disease.DiseaseGroupId != doctor.Profile.DiseaseGroupId)
			{
				yield return new ValidationResult("Doctor profile must correlate with patient's disease", new List<string> { "DoctorId" });
			}
			else
			{
				var cabinet = db.Cabinets.First(c => c.Id == this.CabinetId);

				var doctorEmployment = db.MedicalStaffEmployments
					.Where(mse => mse.MedicalInstitutionId == cabinet.ClinicId && mse.MedicalStaffId == doctor.Id)
					.Count(mse => mse.EmploymentDate <= this.VisitDate && (mse.DischargeDate == null || mse.DischargeDate >= this.VisitDate));


				if (doctorEmployment == 0)
				{
					yield return new ValidationResult("Doctor must be employed in clinic", new List<string> { "ClinicId" });
				}
			}


		}
	}
}