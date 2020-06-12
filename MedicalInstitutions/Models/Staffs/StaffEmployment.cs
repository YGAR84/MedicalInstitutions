using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using MedicalInstitutions.Models.MedicalInstitutions;

namespace MedicalInstitutions.Models.Staffs
{
	public class StaffEmployment
	{
		[DisplayName("Staff employment id")]
		public int Id { get; set; }

		[Required]
		[DisplayName("Medical institution")]
		public int MedicalInstitutionId { get; set; }

		[Required]
		[DataType(DataType.Currency)]
		[Range(0, Int32.MaxValue)]
		public decimal Salary { get; set; }

		[Required]
		[DisplayName("Vacation duration")]
		[Range(0, Int32.MaxValue)]
		public int Vacation { get; set; }

		[Required]
		[DisplayName("Employment date")]
		[DataType(DataType.Date)]
		[DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
		public DateTime EmploymentDate { get; set; }

		public string EmploymentDateFormat => EmploymentDate.ToString("dd.MM.yyyy");

		[DisplayName("Discharge date")]
		[DataType(DataType.Date)]
		[DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true, ConvertEmptyStringToNull = true)]
		public DateTime? DischargeDate { get; set; }

		public string DischargeDateFormat => DischargeDate?.ToString("dd.MM.yyyy");

		public virtual MedicalInstitution MedicalInstitution { get; set; }

	}
}