using System.Collections.Generic;

namespace MedicalInstitutions.Models.MedicalInstitutions.Hospitals
{
	public class Hospital : MedicalInstitution
	{
		public virtual ICollection<HospitalBuilding> HospitalBuildings { get; set; }
	}
}