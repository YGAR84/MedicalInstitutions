using System;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Web.Mvc;

namespace MedicalInstitutions.Models.MedicalInstitutions.Hospitals
{
	public class HospitalBuilding
	{
		[DisplayName("Hospital building ID")]
		[HiddenInput(DisplayValue = false)]
		public int Id { get; set; }

		[Required]
		[Range(1, Int32.MaxValue)]
		[Index("Ix_BuildingNumber_Hospital", 1, IsUnique = true)]
		public int Number { get; set; }

		[DisplayName("Hospital building name")]
		public string HospitalBuildingName => Number + " building" + " | " + Hospital.MedicalInstitutionName;

		[Required]
		[DisplayName("Hospital")]
		[Index("Ix_BuildingNumber_Hospital", 2, IsUnique = true)]
		public int HospitalId { get; set; }

		public virtual Hospital Hospital { get; set; }
	}
}
