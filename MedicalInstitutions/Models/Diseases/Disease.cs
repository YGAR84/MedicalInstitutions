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
	public class Disease
	{
		[DisplayName("Disease ID")]
		[HiddenInput(DisplayValue = false)]
		public int Id { get; set; }

		[Required]
		[DisplayName("Disease name")]
		[StringLength(30, MinimumLength = 3)]
		[RegularExpression(@"^[A-Z]+[a-zA-Z0-9'\s]*$")]
		[Index("Ix_DiseaseName", 1, IsUnique = true)]
		public string DiseaseName { get; set; }

		[Required]
		[DisplayName("Disease group")]
		public int DiseaseGroupId { get; set; }

		public virtual DiseaseGroup DiseaseGroup { get; set; }
	}
}