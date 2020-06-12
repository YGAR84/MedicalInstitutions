using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;
using MedicalInstitutions.Models.MedicalInstitutions.Laboratories;
using MedicalInstitutions.Models.Staffs.MedicalStaffs;
using MedicalInstitutions.Models.Staffs.ServiceStaffs;

namespace MedicalInstitutions.Models.MedicalInstitutions
{
	public class MedicalInstitution
	{
		[DisplayName("Medical institution ID")]
		public int Id { get; set; }

		[Required]
		[DisplayName("Name")]
		[RegularExpression(@"^[A-Z]+[a-zA-Z0-9№'\s]*$")]
		[StringLength(100, MinimumLength = 5)]
		[Index("Ix_MedicalInstitutionName", 1, IsUnique = true)]
		public string MedicalInstitutionName { get; set; }

		[Required]
		[DisplayName("Address")]
		[StringLength(100, MinimumLength = 5)]
		[RegularExpression(@"^[A-Z]+[a-zA-Z0-9,.'\s]*$")]
		public string Address { get; set; }

		public virtual ICollection<ServiceStaffEmployment> ServiceStaffEmployments { get; set; }

		public virtual ICollection<LaboratoryContract> LaboratoryContracts { get; set; }

	}
}