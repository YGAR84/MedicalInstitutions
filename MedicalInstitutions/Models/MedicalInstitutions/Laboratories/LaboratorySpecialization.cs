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
	public class LaboratorySpecialization
	{
		[DisplayName("Laboratory specialization ID")]
		[HiddenInput(DisplayValue = false)]
		public int Id { get; set; }

		[Required]
		[DisplayName("Laboratory specialization name")]
		[RegularExpression(@"^[A-Z]+[a-zA-Z0-9'\s]*$")]
		[StringLength(50, MinimumLength = 5)]
		[Index("Ix_LaboratorySpecializationName", 1, IsUnique = true)]
		public string Name { get; set; }

		public virtual ICollection<Laboratory> Laboratories { get; set; }

		public LaboratorySpecialization()
		{
			Laboratories = new List<Laboratory>();
		}
	}
}