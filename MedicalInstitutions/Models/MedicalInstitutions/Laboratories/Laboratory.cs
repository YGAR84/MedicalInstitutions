using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using MedicalInstitutions.Models.MedicalInstitutions.Clinics;
using MedicalInstitutions.Models.MedicalInstitutions.Hospitals;

namespace MedicalInstitutions.Models.MedicalInstitutions.Laboratories
{

	public class Laboratory
	{
		[DisplayName("Laboratory ID")]
		[HiddenInput(DisplayValue = false)]
		public int Id { set; get; }

		[Required]
		[DisplayName("Address")]
		[RegularExpression(@"^[A-Z]+[a-zA-Z0-9,.'\s]*$")]
		[StringLength(100, MinimumLength = 1)]
		[Index("Ix_LaboratoryAddress", 1, IsUnique = true)]
		public string Address { get; set; }

		[DisplayName("Laboratory specializations")]
		public string LaboratorySpecialization => string.Join(" | ", LaboratorySpecializations.Select(sp => sp.Name).ToArray()); 

		public string LaboratoryName => Address + " | " + LaboratorySpecialization;

		public virtual ICollection<LaboratorySpecialization> LaboratorySpecializations { get; set; }

		public virtual ICollection<LaboratoryContract> LaboratoryContracts { get; set; }
	}
}