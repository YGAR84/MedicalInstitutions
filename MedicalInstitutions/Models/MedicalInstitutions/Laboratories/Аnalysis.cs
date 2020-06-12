using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using MedicalInstitutions.Models.Patients;
using MedicalInstitutions.Models.Patients.Visit;
using System.Data.Entity;

namespace MedicalInstitutions.Models.MedicalInstitutions.Laboratories
{
	public class Analysis : IValidatableObject
	{
		[DisplayName("Analysis ID")]
		[HiddenInput(DisplayValue = false)]
		public int Id { get; set; }

		[Required]
		[DisplayName("Analysis name")]
		[RegularExpression(@"^[A-Z]+[a-zA-Z0-9'\s]*$")]
		public string AnalysisName { get; set; }

		[Required]
		[DisplayName("Analysis date")]
		[DataType(DataType.Date)]
		public DateTime AnalysisDate { get; set; }

		public string AnalysisDateFormat => AnalysisDate.ToString("dd.MM.yyyy");

		[Required]
		[DisplayName("Laboratory")]
		public int LaboratoryId { get; set; }

		[DisplayName("Medical institution visit")]
		public int? MedicalInstitutionVisitId { get; set; }

		public virtual MedicalInstitutionVisit MedicalInstitutionVisit { get; set; }
		public virtual Laboratory Laboratory { get; set; }

		private MedicalInstitutionsContext db = new MedicalInstitutionsContext();

		public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
		{
			
			var medicalInstitutionVisit =
				db.MedicalInstitutionVisits.First(miv => miv.Id == this.MedicalInstitutionVisitId);

			if (medicalInstitutionVisit is HospitalVisit hospitalVisit)
			{
				var ward = db.Wards.Include(w => w.HospitalDepartment)
					.Include(w => w.HospitalDepartment.HospitalBuilding).First(w=>w.Id == hospitalVisit.WardId);
				var hospitalId = ward.HospitalDepartment.HospitalBuilding.HospitalId;
				var contractsCount = db.LaboratoryContracts
					.Where(lc => lc.LaboratoryId == this.LaboratoryId && lc.MedicalInstitutionId == hospitalId)
					.Count(lc => true);

				if (contractsCount == 0)
				{
					yield return new ValidationResult("Must be contract between medical institution and laboratory", new List<string> { "LaboratoryId" });
				}

				if (this.AnalysisDate <= hospitalVisit.VisitDate ||
				    (hospitalVisit.VisitEndDate != null && this.AnalysisDate >= hospitalVisit.VisitEndDate))
				{
					yield return new ValidationResult("Analysis must be in visit", new List<string> { "LaboratoryId" });
				}

			}
			else if (medicalInstitutionVisit is ClinicVisit clinicVisit)
			{
				var cabinet = db.Cabinets.First(c => c.Id == clinicVisit.CabinetId);
				var clinicId = cabinet.ClinicId;
				var contractsCount = db.LaboratoryContracts
					.Where(lc => lc.LaboratoryId == this.LaboratoryId && lc.MedicalInstitutionId == clinicId)
					.Count(lc => true);

				if (contractsCount == 0)
				{
					yield return new ValidationResult("Must be contract between medical institution and laboratory", new List<string> { "AnalysisDate" });
				}

				if (this.AnalysisDate != clinicVisit.VisitDate)
				{
					yield return new ValidationResult("Analysis must be in visit", new List<string> { "AnalysisDate" });
				}
			}
		}
	}
}