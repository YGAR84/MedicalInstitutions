using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace MedicalInstitutions.Models.Staffs.ServiceStaffs
{
	public class ServiceStaffSpecialty
	{
		public enum ServiceStaffType
		{
			Janitor,
			Nurse,
			Orderly
		}

		public int Id { get; set; }

		[Required]
		[DisplayName("Specialty name")]
		[StringLength(30, MinimumLength = 5)]
		[Index("Ix_SpecialtyName", 1, IsUnique = true)]
		public string SpecialtyName { get; set; }
	}
}