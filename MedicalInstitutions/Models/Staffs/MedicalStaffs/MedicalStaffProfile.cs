using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;
using MedicalInstitutions.Models.Diseases;

namespace MedicalInstitutions.Models.Staffs.MedicalStaffs
{
	public class MedicalStaffProfile
	{
		[DisplayName("Profile ID")]
		public int Id { get; set; }

		[Required]
		[StringLength(30, MinimumLength = 3)]
		[DisplayName("Profile name")]
		[Index("Ix_ProfileName", 1, IsUnique = true)]
		public string ProfileName { get; set; }

		[Required]
		[DisplayName("Disease group")]
		public int DiseaseGroupId { get; set; }

		[Required]
		[DisplayName("Salary addition")]
		[DataType(DataType.Currency)]
		[Range(0, Int32.MaxValue)]
		public decimal SalaryAddition { get; set; }

		[Required]
		[DisplayName("Vacation addition")]
		[Range(0, 20)]
		public int VacationAddition { get; set; }

		[Required]
		[DisplayName("Is surgeon?")]
		public bool IsSurgeon { get; set; }

		public virtual DiseaseGroup DiseaseGroup { get; set; }

		public MedicalStaffProfile()
		{
			SalaryAddition = 0M;
			VacationAddition = 0;
			IsSurgeon = false;
		}
	}
}