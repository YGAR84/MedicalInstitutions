using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace MedicalInstitutions.Models.MedicalInstitutions.Laboratories
{
	public class LaboratoryContract
	{
		[DisplayName("Laboratory contract ID")]
		[HiddenInput(DisplayValue = false)]
		public int Id { set; get; }

		[Required]
		[DisplayName("Laboratory")]
		[Index("Ix_LaboratoryMedicalInstitution", 1, IsUnique = true)]
		public int LaboratoryId { get; set; }

		[Required]
		[DisplayName("Medical institution")]
		[Index("Ix_LaboratoryMedicalInstitution", 2, IsUnique = true)]
		public int MedicalInstitutionId { get; set; }

		[Required]
		[DisplayName("Contract price")]
		[DataType(DataType.Currency)]
		[Range(0, Int32.MaxValue)]
		public decimal ContractPrice { get; set; }

		public virtual Laboratory Laboratory { get; set; }
		public virtual MedicalInstitution MedicalInstitution { get; set; }
	}
}