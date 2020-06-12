//using System;
//using System.Collections.Generic;
//using System.Linq;
//using System.Web;
//using MedicalInstitutions.Models.Staffs.MedicalStaffs;

//namespace MedicalInstitutions.Models.Staffs.MedicalStaffs
//{
//	public class Radiologist : MedicalStaff, IHarmfulConditions, ILongerVacation
//	{
//		public new decimal SalaryAddition { get; set; }

//		private decimal _salary;

//		public override decimal Salary
//		{
//			get => _salary;
//			set => _salary = value + SalaryAddition;
//		}

//		public new int VacationAddition { get; set; }

//		private int _vacation;

//		public override int Vacation
//		{
//			get => _vacation;
//			set => _vacation = value + VacationAddition;
//		}

//		public Radiologist()
//		{
//			SalaryAddition = 10000.00M;
//			VacationAddition = 3;

//		}
		
//	}
//}