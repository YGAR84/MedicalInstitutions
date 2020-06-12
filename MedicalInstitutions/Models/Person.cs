using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace MedicalInstitutions.Models
{
	public class Person
	{
		[DisplayName("Person ID")]
		public int Id { get; set; }

		[Required]
		[DisplayName("First name")]
		public string FirstName { get; set; }

		[Required]
		[DisplayName("Second name")]
		public string SecondName { get; set; }

		//[Required]
		//[DisplayName("Document")]
		//[Index("Ix_Document", 1, IsUnique = true)]
		//public string Document { get; set; }

		//[Required]
		//[DisplayName("BirthDate")]
		//public DateTime BirthDate { get; set; }

		public string FullName => FirstName + " " + SecondName;
	}
}