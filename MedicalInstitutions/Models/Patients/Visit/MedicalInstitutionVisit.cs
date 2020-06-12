using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Web.Mvc.Routing.Constraints;
using MedicalInstitutions.Models.Staffs.MedicalStaffs;
using MedicalInstitutions.Models.Diseases;

namespace MedicalInstitutions.Models.Patients.Visit
{
	public class MedicalInstitutionVisit : IValidatableObject
	{
		[DisplayName("Visit ID")]
		public int Id { get; set; }

		[Required]
		[DisplayName("Patient")]
		public int PatientId { get; set; }
		
		[DisplayName("Doctor")]
		public int? DoctorId { get; set; }

		[Required]
		[DisplayName("Visit date")]
		[DataType(DataType.Date)]
		[DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
		public DateTime VisitDate { get; set; }

		public virtual string MedicalInstitutionVisitName => ((Doctor == null) ? "" : Doctor.MedicalStaffName + " | ") + VisitDate.ToString("dd.MM.yyyy");

		public string VisitDateFormat => VisitDate.ToString("dd.MM.yyyy");

		[Required]
		[DisplayName("Disease")]
		public int DiseaseId { get; set; }

		public virtual ICollection<Operation> Operations { get; set; }

		public virtual Disease Disease { get; set; }
		public virtual Patient Patient { get; set; }
		public virtual MedicalStaff Doctor { get; set; }


		public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
		{
			if (this.VisitDate > DateTime.Today)
			{
				yield return new ValidationResult("Visit date must be not in future", new List<string> { "VisitDate" });
			}
		}
	}
}