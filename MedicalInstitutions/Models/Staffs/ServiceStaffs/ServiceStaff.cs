using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using MedicalInstitutions.Models.MedicalInstitutions;

namespace MedicalInstitutions.Models.Staffs.ServiceStaffs
{
	public class ServiceStaff : Person
	{
		[Required]
		[DisplayName("Specialty")]
		public int SpecialtyId { get; set; }

		public string ServiceStaffName => FullName + " | " + Specialty.SpecialtyName;

		public virtual ServiceStaffSpecialty Specialty { get; set; }
		public virtual ICollection<ServiceStaffEmployment> ServiceStaffEmployments { get; set; }
	}
}