using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using FluentValidation;
using FluentValidation.Attributes;
using MedicalInstitutions.Models.MedicalInstitutions.Hospitals;
using System.Data.Entity;
using ValidationContext = System.ComponentModel.DataAnnotations.ValidationContext;

namespace MedicalInstitutions.Models.Patients.Visit
{
	public enum PatientCondition
	{
		Serious,
		Stable,
		Good
	}

	public class HospitalVisit : MedicalInstitutionVisit, IValidatableObject
	{
		[DisplayName("Ward")]
		public int WardId { get; set; }

		[DataType(DataType.Date)]
		[DisplayName("Visit end date")]
		[DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
		public DateTime? VisitEndDate { get; set; }

		public string VisitEndDateFormat => VisitEndDate?.ToString("dd.MM.yyyy");

		public override string MedicalInstitutionVisitName =>
			Patient.FullName + " | " + Ward.WardName + " | " + base.MedicalInstitutionVisitName + ((VisitEndDate == null)? "" : " - " + VisitEndDateFormat);

		[Required]
		[DisplayName("Temperature")]
		[Range(30, 43)]

		public decimal Temperature { get; set; }

		[Required]
		[DisplayName("Condition")]
		public PatientCondition PatientCondition { get; set; }

		public virtual Ward Ward { get; set; }

		private MedicalInstitutionsContext db = new MedicalInstitutionsContext();

		public new IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
		{
			var validationResults = base.Validate(validationContext).ToList();
			if (validationResults.Any())
			{
				yield return validationResults.First();
			}
			if (this.VisitEndDate < this.VisitDate)
			{
				yield return new ValidationResult("Visit end date must be more or equals than visit date", new List<string> { "VisitEndDate"});
			}

			var totalBeds = db.Wards.First(w => w.Id == this.WardId).NumOfBeds;
			var busyBeds = db.HospitalVisits
				.Where(hv => hv.WardId == this.WardId)
				.Count(hv =>
					!((hv.VisitEndDate == null && this.VisitEndDate != null && hv.VisitDate <= this.VisitEndDate) ||
					  (hv.VisitEndDate != null && this.VisitDate >= hv.VisitEndDate)));

			if (totalBeds <= busyBeds)
			{
				yield return new ValidationResult("Not empty beds", new List<string> { "WardId" });
			}

			var intersectionWithClnics = db.ClinicVisits
				.Where(cv => cv.PatientId == this.PatientId).Count(cv =>
					!((cv.VisitDate <= this.VisitDate) ||
					  (this.VisitEndDate != null && cv.VisitDate >= this.VisitEndDate)));
			if (intersectionWithClnics != 0)
			{
				yield return new ValidationResult("Visit can not be, because patient has visit to clinic on this dates", new List<string> { "VisitEndDate" });
			}

			var intersectionWithHospitals = db.HospitalVisits
				.Where(hv => hv.Id != this.Id)
				.Where(hv => hv.PatientId == this.PatientId).Count(hv =>
					!((hv.VisitEndDate == null && this.VisitEndDate != null && hv.VisitDate <= this.VisitEndDate) ||
					  (hv.VisitEndDate != null && this.VisitDate >= hv.VisitEndDate)));

			if (intersectionWithHospitals != 0)
			{
				yield return new ValidationResult("Visit can not be, because patient has visit to hospital on this dates", new List<string> { "VisitEndDate" });
			}


			var disease = db.Diseases.First(d => d.Id == this.DiseaseId);
			var doctor = db.MedicalStaffs.Include(d => d.Profile).First(d => d.Id == this.DoctorId);
			if (disease.DiseaseGroupId != doctor.Profile.DiseaseGroupId)
			{
				yield return new ValidationResult("Doctor profile must correlate with patient's disease", new List<string> { "DoctorId" });
			}
			else
			{
				var ward = db.Wards.Include(w => w.HospitalDepartment).Include(w => w.HospitalDepartment.HospitalBuilding).First(w => w.Id == this.WardId);
				if (ward.HospitalDepartment.DiseaseGroupId != disease.DiseaseGroupId)
				{
					yield return new ValidationResult("Hospital department must correlate with patient's disease", new List<string> { "WardId" });
				}
				else
				{
					var doctorEmployment = db.MedicalStaffEmployments
						.Where(mse => mse.MedicalInstitutionId == ward.HospitalDepartment.HospitalBuilding.HospitalId && mse.MedicalStaffId == doctor.Id)
						.Count(mse => mse.EmploymentDate <= this.VisitDate && (mse.DischargeDate == null || mse.DischargeDate >= this.VisitEndDate));

					if (doctorEmployment == 0)
					{
						yield return new ValidationResult("Doctor must be employed in hospital", new List<string> { "DoctorId" });
					}
				}
			}

		}
	}
}