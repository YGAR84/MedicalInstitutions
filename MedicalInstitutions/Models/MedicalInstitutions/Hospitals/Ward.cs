using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Web.Mvc;
using MedicalInstitutions.Models.Patients.Visit;

namespace MedicalInstitutions.Models.MedicalInstitutions.Hospitals
{
	public class Ward
	{
		[DisplayName("Ward ID")]
		[HiddenInput(DisplayValue = false)]
		public int Id { get; set; }

		[Required]
		[DisplayName("Ward number")]
		[Index("Ix_WardNumber_HospitalDepartment", 1, IsUnique = true)]
		public int Number { get; set; }

		public string WardName => HospitalDepartment.DepartmentName + " | " + "Ward number: " + Number;

		[Required]
		[DisplayName("Number of beds")]
		public int NumOfBeds { get; set; }

		//[Required]
		[DisplayName("Hospital department")]
		[Index("Ix_WardNumber_HospitalDepartment", 2, IsUnique = true)]
		public int? HospitalDepartmentId { get; set; }

		public virtual HospitalDepartment HospitalDepartment { get; set; }

		public virtual ICollection<HospitalVisit> HospitalVisits { get; set; }

	}
}