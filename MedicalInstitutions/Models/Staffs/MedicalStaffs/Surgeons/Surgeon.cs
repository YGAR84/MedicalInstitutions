//using System.Collections.Generic;
//using System.ComponentModel;
//using System.ComponentModel.DataAnnotations;
//using System.Linq;
//using MedicalInstitutions.Models.Patients;
////using MedicalInstitutions.Models.Patient;

//namespace MedicalInstitutions.Models.Staffs.MedicalStaffs.Surgeons
//{
//	public enum SugreonType
//	{

//	}

//	public class Surgeon : MedicalStaff
//	{
//		[DisplayName("Number of operations")]
//		public int NumOfOperations => Operations.Count();

//		[DisplayName("Number of fatal operations")]
//		public int NumOfFatalOperations =>
//			Operations.Count(operation => operation.OperationResult == OperationResult.Lethal);

//		public virtual ICollection<Operation> Operations { get; set; }

//		public Surgeon()
//		{
//			Operations = new List<Operation>();
//		}

//	}
//}