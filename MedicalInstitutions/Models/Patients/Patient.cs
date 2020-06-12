using System.Collections.Generic;
using MedicalInstitutions.Models.Patients.Visit;

namespace MedicalInstitutions.Models.Patients
{
	public class Patient : Person
	{
		public virtual ICollection<MedicalInstitutionVisit> Visits { get; set; }
	}
}