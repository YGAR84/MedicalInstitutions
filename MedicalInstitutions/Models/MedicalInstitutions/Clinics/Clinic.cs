using System.ComponentModel;
using System.Web.Mvc;
using MedicalInstitutions.Models.MedicalInstitutions.Hospitals;

namespace MedicalInstitutions.Models.MedicalInstitutions.Clinics
{
	public class Clinic : MedicalInstitution
	{
		[DisplayName("Hospital")]
		public int? HospitalId { get; set; }

		public virtual Hospital Hospital { get; set; }
		
	}
}