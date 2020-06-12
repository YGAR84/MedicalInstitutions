using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Data.Entity;
using MedicalInstitutions.Models.MedicalInstitutions.Clinics;
using MedicalInstitutions.Models.MedicalInstitutions.Hospitals;

namespace MedicalInstitutions.Models.Staffs.MedicalStaffs
{
	public enum MedicalStaffEmploymentType
	{
		Consultant,
		StaffDoctor,
	}

	public class MedicalStaffEmployment : StaffEmployment, IValidatableObject
	{
		[DisplayName("Medical staff")]
		public int MedicalStaffId { get; set; }

		[Required]
		[DisplayName("Employment type")]
		public MedicalStaffEmploymentType EmploymentType { get; set; }

		public virtual MedicalStaff MedicalStaff { get; set; }

		private MedicalInstitutionsContext db = new MedicalInstitutionsContext();

		public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
		{

			if (this.EmploymentType == MedicalStaffEmploymentType.Consultant)
			{
				var consultant = db.MedicalStaffs.First(ms => ms.Id == this.MedicalStaffId);
				if (consultant.Degree == null)
				{
					yield return new ValidationResult("Must have medical degree", new List<string> { "MedicalStaffId" });
				}
			}
			else if (this.EmploymentType == MedicalStaffEmploymentType.StaffDoctor)
			{
				var employments = db.MedicalStaffEmployments
					.Include(mse => mse.MedicalInstitution)
					.Where(mse => mse.MedicalStaffId == this.MedicalStaffId)
					.Where(mse =>
						mse.EmploymentType == MedicalStaffEmploymentType.StaffDoctor &&
						(mse.DischargeDate == null || mse.DischargeDate > this.EmploymentDate));

				var numOfClinics = 0;
				var numOfHospitals = 0;
				foreach (var employment in employments)
				{
					if (employment.MedicalInstitution is Hospital)
					{
						numOfHospitals++;
					}
					else if (employment.MedicalInstitution is Clinic)
					{
						numOfClinics++;
					}
				}
				var medicalInstitution = db.MedicalInstitutions.First(mi => mi.Id == this.MedicalInstitutionId);

				if (medicalInstitution is Hospital hospital)
				{
					if (numOfHospitals == 1)
					{
						yield return new ValidationResult("Must be employed only in one hospital", new List<string> { "MedicalInstitutionId" });
					}
				}
				else if (medicalInstitution is Clinic clinic)
				{
					if (numOfClinics == 1)
					{
						yield return new ValidationResult("Must be employed only in one clinic", new List<string> { "MedicalInstitutionId" });
					}
				}
			}
		}
	}
}