using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Data.Entity;
using FluentValidation;
using FluentValidation.Attributes;
using MedicalInstitutions.Models.Patients.Visit;
using MedicalInstitutions.Models.Staffs.MedicalStaffs;
using ValidationContext = System.ComponentModel.DataAnnotations.ValidationContext;

namespace MedicalInstitutions.Models.Patients
{
	public enum OperationResult
	{
		Success,
		Lethal
	}

	public class Operation : IValidatableObject
	{
		[DisplayName("Operation ID")]
		public int Id { get; set; }

		[Required]
		[DisplayName("Visit")]
		public int MedicalInstitutionVisitId { get; set; }

		[Required]
		[DisplayName("Operation name")]
		[StringLength(30, MinimumLength = 3)]
		public string OperationName { get; set; }

		[Required]
		[DisplayName("Surgeon")]
		public int SurgeonId { get; set; }

		[Required]
		[DisplayName("Operation date")]
		[DataType(DataType.Date)]
		[DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
		public DateTime OperationDate { get; set; }

		public string OperationDateFormat => OperationDate.ToString("dd.MM.yyyy");

		[Required]
		[DisplayName("Operation result")]
		public OperationResult OperationResult { get; set; }

		public virtual MedicalInstitutionVisit MedicalInstitutionVisit { get; set; }
		public virtual MedicalStaff Surgeon { get; set; }

		private MedicalInstitutionsContext db = new MedicalInstitutionsContext();

		public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
		{
			var medicalInstitutionVisit =
				db.MedicalInstitutionVisits.First(miv => miv.Id == this.MedicalInstitutionVisitId);
			if (medicalInstitutionVisit != null)
			{
				if (medicalInstitutionVisit is HospitalVisit hospitalVisit)
				{
					if (this.OperationDate < hospitalVisit.VisitDate || this.OperationDate > hospitalVisit.VisitEndDate)
					{
						yield return new ValidationResult("Operation date must be between visit date and visit end date", new List<string> { "OperationDate" });
					}
					var ward = db.Wards.Include(w => w.HospitalDepartment).Include(w => w.HospitalDepartment.HospitalBuilding).First(w => w.Id == hospitalVisit.WardId);
					var surgeonEmployemnt = db.MedicalStaffEmployments
						.Where(mse => mse.MedicalInstitutionId == ward.HospitalDepartment.HospitalBuilding.HospitalId && mse.MedicalStaffId == this.SurgeonId).
						Count(mse => mse.EmploymentDate <= this.OperationDate && (mse.DischargeDate == null || mse.DischargeDate >= this.OperationDate));
					if (surgeonEmployemnt == 0)
					{
						yield return new ValidationResult("Surgeon must be employed in hospital", new List<string> { "SurgeonId" });

					}
				}
				else if (medicalInstitutionVisit is ClinicVisit clinicVisit)
				{
					if (this.OperationDate != clinicVisit.VisitDate)
					{
						yield return new ValidationResult("Operation date must be in visit date", new List<string> { "OperationDate" });
					}

					var cabinet = db.Cabinets.First(c => c.Id == clinicVisit.CabinetId);

					var surgeonEmployment = db.MedicalStaffEmployments
						.Where(mse => mse.MedicalInstitutionId == cabinet.ClinicId && mse.MedicalStaffId == this.SurgeonId)
						.Count(mse => mse.EmploymentDate <= this.OperationDate && (mse.DischargeDate == null || mse.DischargeDate >= this.OperationDate));


					if (surgeonEmployment == 0)
					{
						yield return new ValidationResult("Surgeon must be employed in clinic", new List<string> { "SurgeonId" });
					}
				}
			}

			var medicalStaff = db.MedicalStaffs.Include(ms => ms.Profile).First(ms => ms.Id == this.SurgeonId);
			if (medicalStaff != null)
			{
				if (!medicalStaff.Profile.IsSurgeon)
				{
					yield return new ValidationResult("Doctor must be surgeon", new List<string> { "SurgeonId" });
				}
			}

			
		}
	}

}