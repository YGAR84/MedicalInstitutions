using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Web.Mvc;
using MedicalInstitutions.Models.Patients.Visit;

namespace MedicalInstitutions.Models.MedicalInstitutions.Clinics
{
	public class Cabinet
	{
		[DisplayName("Cabinet ID")]
		[HiddenInput(DisplayValue = false)]
		public int Id { get; set; }

		[Required]
		[DisplayName("Cabinet number")]
		[Index("Ix_CabinetNumber_Clinic", 1, IsUnique = true)]
		public int Number { get; set; }

		public string CabinetName => "Cabinet number: " + Number + " | " + Clinic.MedicalInstitutionName;

		[Required]
		[DisplayName("Clinic")]
		[Index("Ix_CabinetNumber_Clinic", 2, IsUnique = true)]
		public int ClinicId { get; set; }

		public virtual Clinic Clinic { get; set; }

		public virtual ICollection<ClinicVisit> ClinicVisits { get; set; }
	}
}