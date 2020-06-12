using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Web;

namespace MedicalInstitutions.Models.Staffs.ServiceStaffs
{
	public class ServiceStaffEmployment : StaffEmployment
	{
		[DisplayName("Service staff")]
		public int ServiceStaffId { get; set; }


		public virtual ServiceStaff ServiceStaff { get; set; }
	}
}