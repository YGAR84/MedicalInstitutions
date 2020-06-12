using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Web.Mvc;
using MedicalInstitutions.Models.Diseases;

namespace MedicalInstitutions.Models.MedicalInstitutions.Hospitals
{
    public class HospitalDepartment
    {
		[DisplayName("Hospital department ID")]
		[HiddenInput(DisplayValue = false)]
		public int Id { get; set; }

		[DisplayName("Department name")]
		public string DepartmentName => DiseaseGroup.Name + " department" + " | " + HospitalBuilding.HospitalBuildingName;

		[Required]
		[DisplayName("Disease group")]
		[Index("Ix_DiseaseGroup_HospitalBuilding", 1, IsUnique = true)]
		public int DiseaseGroupId { get; set; }

		[Required]
		[DisplayName("Hospital building")]
		[Index("Ix_DiseaseGroup_HospitalBuilding", 2, IsUnique = true)]
		public int HospitalBuildingId { get; set; }

		public virtual DiseaseGroup DiseaseGroup { get; set; }
		public virtual HospitalBuilding HospitalBuilding { get; set; }
		public virtual ICollection<Ward> Wards { get; set; }
	}
}