using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using MedicalInstitutions.Models.Diseases;
using MedicalInstitutions.Models.MedicalInstitutions;
using MedicalInstitutions.Models.MedicalInstitutions.Clinics;
using MedicalInstitutions.Models.MedicalInstitutions.Hospitals;
using MedicalInstitutions.Models.Patients;

namespace MedicalInstitutions.Models.Staffs.MedicalStaffs
{
	public enum MedicalDegree
	{
		Professor,
		Docent, 
	}

	public class MedicalStaff : Person
	{
		[DisplayName("Academic degree")]
		public MedicalDegree? Degree { get; set; }

		//[Required]
		[DisplayName("Profile")]
		public int? ProfileId { get; set; }

		public string MedicalStaffName => FullName + ((Profile == null) ? "" : " | " + Profile.ProfileName);

		public int NumOfOperations => Operations?.Count() ?? 0;

		public virtual MedicalStaffProfile Profile { get; set; }
		public virtual ICollection<MedicalStaffEmployment>  MedicalStaffEmployments { get; set; }
		public virtual ICollection<Operation> Operations { get; set; }
	}
}