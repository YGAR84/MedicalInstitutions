using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace MedicalInstitutions.Models.Diseases
{
	public class DiseaseGroup
	{
		[DisplayName("Disease group ID")]
		[HiddenInput(DisplayValue = false)]
		public int Id { get; set; }

		[Required]
		[DisplayName("Disease group name")]
		[RegularExpression(@"^[A-Z]+[a-zA-Z0-9'\s]*$")]
		[StringLength(50, MinimumLength = 1)]
		[Index("Ix_DiseaseGroupName", 1, IsUnique = true)]
		public string Name { get; set; }
	}
}