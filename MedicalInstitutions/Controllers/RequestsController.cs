using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;
using System.Net;
using System.Web;
using System.Web.Mvc;
using System.Web.UI;
using System.Web.UI.WebControls;
using MedicalInstitutions.Models;
using MedicalInstitutions.Models.MedicalInstitutions;
using MedicalInstitutions.Models.MedicalInstitutions.Clinics;
using MedicalInstitutions.Models.MedicalInstitutions.Hospitals;
using MedicalInstitutions.Models.MedicalInstitutions.Laboratories;
using MedicalInstitutions.Models.Patients;
using MedicalInstitutions.Models.Patients.Visit;
using MedicalInstitutions.Models.Staffs.MedicalStaffs;
using MedicalInstitutions.Models.Staffs.ServiceStaffs;
using PagedList;

namespace MedicalInstitutions.Controllers
{
	[Authorize(Roles = "Admin,Doctor")]
	[OutputCache(CacheProfile = "defaultCacheProfile")]
	public class RequestsController : Controller
	{
		private int PageSize = 4;
		private MedicalInstitutionsContext db = new MedicalInstitutionsContext();

		public class Request1ViewModel
		{
			public IPagedList<MedicalStaff> MedicalStaffs { get; set; }
			public SelectList Profiles { get; set; }
			public SelectList MedicalInstitutions { get; set; }
			public int? ProfileId { get; set; }
			public int? MedicalInstitutionId { get; set; }
		}

		public ActionResult Request1([Bind(Include = "ProfileId,MedicalInstitutionId")] Request1ViewModel r1vm, int? pageNum, int? profileId, int? medicalInstitutionId)
		{
			IQueryable<MedicalStaff> medicalStaffs = db.MedicalStaffs
				.Include(ms => ms.Profile)
				.Where(ms => ms.MedicalStaffEmployments
					.Any(me => me.EmploymentType == MedicalStaffEmploymentType.StaffDoctor &&
					           (me.DischargeDate == null || me.DischargeDate <= DateTime.Today)));

			if (r1vm == null)
			{
				r1vm = new Request1ViewModel();
			}

			r1vm.ProfileId = profileId;
			r1vm.MedicalInstitutionId = medicalInstitutionId;

			if (r1vm.MedicalInstitutionId != null && r1vm.MedicalInstitutionId != 0)
			{
				medicalStaffs = medicalStaffs.Where(ms =>
					ms.MedicalStaffEmployments.Any(me => me.MedicalInstitutionId == r1vm.MedicalInstitutionId));
			}

			if (r1vm.ProfileId != null && r1vm.ProfileId != 0)
			{
				medicalStaffs = medicalStaffs.Where(ms => ms.ProfileId == r1vm.ProfileId);
			}
		


			List<MedicalStaffProfile> profiles = db.MedicalStaffProfiles.ToList();
			profiles.Insert(0, new MedicalStaffProfile {ProfileName = "All", Id = 0});

			List<MedicalInstitution> medicalInstitutions = db.MedicalInstitutions.ToList();
			medicalInstitutions.Insert(0, new MedicalInstitution() {MedicalInstitutionName = "All", Id = 0});

			int numOfPage = pageNum ?? 1;



			r1vm.MedicalStaffs = medicalStaffs.OrderBy(ms => ms.FirstName).ToPagedList(numOfPage, PageSize);
			r1vm.Profiles = new SelectList(profiles, "Id", "ProfileName");
			r1vm.MedicalInstitutions = new SelectList(medicalInstitutions, "Id", "MedicalInstitutionName");

			return View(r1vm);
		}

		public class Request2ViewModel
		{
			public IPagedList<ServiceStaff> ServiceStaffs { get; set; }
			public SelectList Specialties { get; set; }
			public SelectList MedicalInstitutions { get; set; }
			public int? MedicalInstitutionId { get; set; }
			public int? SpecialtyId { get; set; }
		}

		public ActionResult Request2([Bind(Include = "SpecialtyId,MedicalInstitutionId")] Request2ViewModel r2vm, int? medicalInstitutionId, int? specialtyId, int ? pageNum)
		{
			IQueryable<ServiceStaff> serviceStaffs = db.ServiceStaffs
				.Include(ss => ss.Specialty)
				.Where(ss => ss.ServiceStaffEmployments
					.Any(se => (se.DischargeDate == null || se.DischargeDate == DateTime.Today)));

			if (r2vm == null)
			{
				r2vm = new Request2ViewModel();
			}

			r2vm.MedicalInstitutionId = medicalInstitutionId;
			r2vm.SpecialtyId = specialtyId;

			if (medicalInstitutionId != null && medicalInstitutionId != 0)
			{
				serviceStaffs = serviceStaffs.Where(ss =>
					ss.ServiceStaffEmployments.Any(se => se.MedicalInstitutionId == medicalInstitutionId));
			}

			if (specialtyId != null && specialtyId != 0)
			{
				serviceStaffs = serviceStaffs.Where(ss => ss.SpecialtyId == specialtyId);
			}

			List<ServiceStaffSpecialty> specialties = db.ServiceStaffSpecialties.ToList();
			specialties.Insert(0, new ServiceStaffSpecialty { SpecialtyName = "All", Id = 0 });

			List<MedicalInstitution> medicalInstitutions = db.MedicalInstitutions.ToList();
			medicalInstitutions.Insert(0, new MedicalInstitution() { MedicalInstitutionName = "All", Id = 0 });

			int numOfPage = (pageNum ?? 1);


			r2vm.ServiceStaffs = serviceStaffs.OrderBy(ss => ss.FirstName).ToPagedList(numOfPage, PageSize);
			r2vm.Specialties = new SelectList(specialties, "Id", "SpecialtyName");
			r2vm.MedicalInstitutions = new SelectList(medicalInstitutions, "Id", "MedicalInstitutionName");


			return View(r2vm);
		}


		public class Request3ViewModel
		{
			public IPagedList<MedicalStaff> MedicalStaffs { get; set; }
			public SelectList Profiles { get; set; }
			public SelectList MedicalInstitutions { get; set; }
			public int? MedicalInstitutionId { get; set; }
			public int? ProfileId { get; set; }
			public int? MinNumOfOperations { get; set; }
		}

		public ActionResult Request3([Bind(Include = "ProfileId,MedicalInstitutionId,MinNumOfOperations")] Request3ViewModel r3vm, int? medicalInstitutionId, int? profileId, int? minNumOfOperations, int? pageNum)
		{
			IQueryable<MedicalStaff> medicalStaffs = db.MedicalStaffs
				.Include(ms => ms.Profile)
				.Include(ms => ms.Operations)
				.Where(ms => ms.MedicalStaffEmployments
					.Any(me => me.EmploymentType == MedicalStaffEmploymentType.StaffDoctor &&
					           (me.DischargeDate == null || me.DischargeDate == DateTime.Today)));

			if (r3vm == null)
			{
				r3vm = new Request3ViewModel();
			}

			r3vm.MedicalInstitutionId = medicalInstitutionId;
			r3vm.MinNumOfOperations = minNumOfOperations;
			r3vm.ProfileId = profileId;

			if (medicalInstitutionId != null && medicalInstitutionId != 0)
			{
				medicalStaffs = medicalStaffs.Where(ms =>
					ms.MedicalStaffEmployments.Any(me => me.MedicalInstitutionId == medicalInstitutionId));
			}

			if (profileId != null && profileId != 0)
			{
				medicalStaffs = medicalStaffs.Where(ms => ms.ProfileId == profileId);
			}

			if (minNumOfOperations != null && minNumOfOperations > 0)
			{
				medicalStaffs = medicalStaffs.Where(ms => ms.Profile.IsSurgeon)
					.Where(ms => ms.Operations.Count() >= minNumOfOperations);
			}

			List<MedicalStaffProfile> profiles = db.MedicalStaffProfiles.Where(p => p.IsSurgeon).ToList();
			profiles.Insert(0, new MedicalStaffProfile { ProfileName = "All", Id = 0 });

			List<MedicalInstitution> medicalInstitutions = db.MedicalInstitutions.ToList();
			medicalInstitutions.Insert(0, new MedicalInstitution() { MedicalInstitutionName = "All", Id = 0 });

			int numOfPage = (pageNum ?? 1);

			r3vm.MedicalStaffs = medicalStaffs.OrderBy(ms => ms.FirstName).ToPagedList(numOfPage, PageSize);
			r3vm.Profiles = new SelectList(profiles, "Id", "ProfileName");
			r3vm.MedicalInstitutions = new SelectList(medicalInstitutions, "Id", "MedicalInstitutionName");


			return View(r3vm);
		}

		public class Request4ViewModel
		{
			public IPagedList<MedicalStaff> MedicalStaffs { get; set; }
			public SelectList Profiles { get; set; }
			public SelectList MedicalInstitutions { get; set; }
			public int TotalNumOfMedicalStaff { get; set; }
			public int? MedicalInstitutionId { get; set; }
			public int? ProfileId { get; set; }
			public int? MinimumWorkExperience { get; set; }
		}

		public ActionResult Request4([Bind(Include = "ProfileId,MedicalInstitutionId,MinimumWorkExperience")] Request4ViewModel r4vm, int? medicalInstitutionId, int? profileId, int? minimumWorkExperience, int? pageNum)
		{
			IQueryable<MedicalStaff> medicalStaffs = db.MedicalStaffs
				.Include(ms => ms.Profile)
				.Where(ms => ms.MedicalStaffEmployments
					.Any(me => me.EmploymentType == MedicalStaffEmploymentType.StaffDoctor &&
					           (me.DischargeDate == null || me.DischargeDate == DateTime.Today)));

			if (r4vm == null)
			{
				r4vm = new Request4ViewModel {MinimumWorkExperience = 0 };
			}

			r4vm.MedicalInstitutionId = medicalInstitutionId;
			r4vm.ProfileId = profileId;
			r4vm.MinimumWorkExperience = minimumWorkExperience;

			if (medicalInstitutionId != null && medicalInstitutionId != 0)
			{
				medicalStaffs = medicalStaffs.Where(ms =>
					ms.MedicalStaffEmployments.Any(me => me.MedicalInstitutionId == medicalInstitutionId));
			}

			if (profileId != null && profileId != 0)
			{
				medicalStaffs = medicalStaffs.Where(ms => ms.ProfileId == profileId);
			}

			if (minimumWorkExperience != null && minimumWorkExperience >= 0)
			{

				medicalStaffs = medicalStaffs.Where(ms => ms.MedicalStaffEmployments
					.Any(mse => DateTime.Today >= DbFunctions.AddYears(mse.EmploymentDate, minimumWorkExperience)));
			}

			List<MedicalStaffProfile> profiles = db.MedicalStaffProfiles.Where(p => p.IsSurgeon).ToList();
			profiles.Insert(0, new MedicalStaffProfile { Id = 0, ProfileName = "Any"});

			List<MedicalInstitution> medicalInstitutions = db.MedicalInstitutions.ToList();
			medicalInstitutions.Insert( 0, new MedicalInstitution { Id = 0, MedicalInstitutionName = "All" } );

			int numOfPage = (pageNum ?? 1);

			r4vm.TotalNumOfMedicalStaff = medicalStaffs.Count();
			r4vm.MedicalStaffs = medicalStaffs.OrderBy(ms => ms.FirstName).ToPagedList(numOfPage, PageSize);
			r4vm.Profiles = new SelectList(profiles, "Id", "ProfileName", 0);
			r4vm.MedicalInstitutions = new SelectList(medicalInstitutions, "Id", "MedicalInstitutionName", 0);


			return View(r4vm);
		}

		public class Request5ViewModel
		{
			public IPagedList<MedicalStaff> MedicalStaffs { get; set; }
			public SelectList Profiles { get; set; }
			public SelectList MedicalInstitutions { get; set; }
			public int? MedicalInstitutionId { get; set; }
			public int? ProfileId { get; set; } 
			public MedicalDegree? MedicalDegree { get; set; }
		}

		public ActionResult Request5([Bind(Include = "ProfileId,MedicalInstitutionId,MedicalDegree")] Request5ViewModel r5vm, int? medicalInstitutionId, int? profileId, MedicalDegree? medicalDegree, int? pageNum)
		{
			IQueryable<MedicalStaff> medicalStaffs = db.MedicalStaffs
				.Include(ms => ms.Profile)
				.Where(ms => ms.MedicalStaffEmployments
					.Any(me => (me.DischargeDate == null)));

			if (r5vm == null)
			{
				r5vm = new Request5ViewModel();
			}

			r5vm.MedicalDegree = medicalDegree;
			r5vm.MedicalInstitutionId = medicalInstitutionId;
			r5vm.ProfileId = profileId;

			if (medicalInstitutionId != null && medicalInstitutionId != 0)
			{
				medicalStaffs = medicalStaffs.Where(ms =>
					ms.MedicalStaffEmployments.Any(me => me.MedicalInstitutionId == medicalInstitutionId));
			}

			if (profileId != null && profileId != 0)
			{
				medicalStaffs = medicalStaffs.Where(ms => ms.ProfileId == profileId);
			}

			if (medicalDegree != null)
			{

				medicalStaffs = medicalStaffs.Where(ms => ms.Degree == medicalDegree);
			}

			List<MedicalStaffProfile> profiles = db.MedicalStaffProfiles.ToList();
			profiles.Insert(0, new MedicalStaffProfile { Id = 0, ProfileName = "Any" });

			List<MedicalInstitution> medicalInstitutions = db.MedicalInstitutions.ToList();
			medicalInstitutions.Insert(0, new MedicalInstitution { Id = 0, MedicalInstitutionName = "All" });

			int numOfPage = (pageNum ?? 1);


			r5vm.MedicalStaffs = medicalStaffs.OrderBy(ms => ms.FirstName).ToPagedList(numOfPage, PageSize);
			r5vm.Profiles = new SelectList(profiles, "Id", "ProfileName", 0);
			r5vm.MedicalInstitutions = new SelectList(medicalInstitutions, "Id", "MedicalInstitutionName", 0);

			return View(r5vm);
		}


		public class Request6ViewModel
		{
			public IPagedList<HospitalVisit> HospitalVisits { get; set; }
			public SelectList Hospitals { get; set; }
			public SelectList HospitalDepartments { get; set; }
			public SelectList Wards { get; set; }
			public int? HospitalId { get; set; }
			public int? HospitalDepartmentId { get; set; }
			public int? WardId { get; set; }
		}

		public ActionResult Request6([Bind(Include = "ProfileId,MedicalInstitutionId,MedicalDegree")] Request6ViewModel r6vm, int? hospitalId, int? hospitalDepartmentId, int? wardId, int? pageNum)
		{
			IQueryable<HospitalVisit> hospitalVisits = db.HospitalVisits
				.Include(hv => hv.Patient);

			int numOfPage = (pageNum ?? 1);

			IPagedList<HospitalVisit> resultHospitalVisits = new List<HospitalVisit>().ToPagedList(numOfPage, PageSize);

			if (r6vm == null)
			{
				r6vm = new Request6ViewModel();
			}

			r6vm.HospitalId = hospitalId;
			r6vm.WardId = wardId;
			r6vm.HospitalDepartmentId = hospitalDepartmentId;

			if (hospitalId != null && hospitalId != 0)
			{
				if (hospitalDepartmentId != null && hospitalDepartmentId != 0)
				{
					if (wardId != null && wardId != 0)
					{
						hospitalVisits = hospitalVisits
							.Include(hv => hv.Ward.HospitalDepartment)
							.Where(hv => hv.WardId == wardId)
							.Where(hv => hv.Ward.HospitalDepartmentId == hospitalDepartmentId)
							.Where(hv => hv.Ward.HospitalDepartment.HospitalBuilding.HospitalId == hospitalId);
					}
					else
					{
						hospitalVisits = hospitalVisits
							.Include(hv => hv.Ward)
							.Include(hv => hv.Ward.HospitalDepartment)
							.Where(hv => hv.Ward.HospitalDepartmentId == hospitalDepartmentId)
							.Where(hv => hv.Ward.HospitalDepartment.HospitalBuilding.HospitalId == hospitalId);
					}
				}
				else
				{
					hospitalVisits = hospitalVisits
						.Include(hv => hv.Ward)
						.Include(hv => hv.Ward.HospitalDepartment)
						.Where(hv => hv.Ward.HospitalDepartment.HospitalBuilding.HospitalId == hospitalId);
				}

				resultHospitalVisits = hospitalVisits.OrderBy(hv => hv.Id).ToPagedList(numOfPage, PageSize);
			}


			List<Hospital> hospitals = db.Hospitals.ToList();
			hospitals.Insert(0, new Hospital { MedicalInstitutionName = "All", Id = 0 });


			var hospitalDepartments = db.HospitalDepartments.ToList()
				.Select(hd => new { hd.Id, Name = hd.DepartmentName })
				.Concat(new[] { new { Id = 0, Name = "Any" } });


			var wards = db.Wards
				.Include(w => w.HospitalDepartment)
				.Include(w => w.HospitalDepartment.HospitalBuilding)
				.Include(w => w.HospitalDepartment.HospitalBuilding.Hospital)
				.ToList()
				.Select(w => new { w.Id, Name = w.WardName })
				.Concat(new[] { new { Id = 0, Name = "Any" } });


			r6vm.HospitalVisits = resultHospitalVisits;
			r6vm.Hospitals = new SelectList(hospitals, "Id", "MedicalInstitutionName");
			r6vm.HospitalDepartments = new SelectList(hospitalDepartments, "Id", "Name", 0);
			r6vm.Wards = new SelectList(wards, "Id", "Name", 0);

				return View(r6vm);
		}

		public class Request7ViewModel
		{
			public IPagedList<Patient> Patients { get; set; }
			public SelectList Hospitals { get; set; }
			public SelectList MedicalStaffs { get; set; }
			public int? HospitalId { get; set; }
			public int? MedicalStaffId { get; set; }

			[DataType(DataType.Date)]
			[DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
			public DateTime? Begin { get; set; }

			[DataType(DataType.Date)]
			[DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
			public DateTime? End { get; set; }
		}

		public ActionResult Request7([Bind(Include = "HospitalId,MedicalStaffId,Begin,End")] Request7ViewModel r7vm, int? hospitalId, int? medicalStaffId, DateTime? begin, DateTime? end, int? pageNum)
		{
			IQueryable<HospitalVisit> hospitalVisits = db.HospitalVisits
				.Include(hv => hv.Patient);

			int numOfPage = (pageNum ?? 1);

			IPagedList<Patient> resultPatients = new List<Patient>().ToPagedList(numOfPage, PageSize);

			if (r7vm == null)
			{
				r7vm = new Request7ViewModel();
			}

			r7vm.HospitalId = hospitalId;
			r7vm.Begin = begin;
			r7vm.End = end;
			r7vm.MedicalStaffId = medicalStaffId;


			if (begin != null && end != null)
			{
				hospitalVisits = hospitalVisits
					.Where(hv => hv.VisitDate >= begin && hv.VisitEndDate != null && hv.VisitEndDate <= end);

				if (medicalStaffId != null && medicalStaffId != 0)
				{
					hospitalVisits = hospitalVisits
						.Where(hv => hv.DoctorId == medicalStaffId);
				}

				if (hospitalId != null && hospitalId != 0)
				{
					hospitalVisits = hospitalVisits
						.Include(hv => hv.Ward)
						.Include(hv => hv.Ward)
						.Include(hv => hv.Ward.HospitalDepartment.HospitalBuilding)
						.Where(hv => hv.Ward.HospitalDepartment.HospitalBuilding.HospitalId == hospitalId);
				}

				resultPatients = hospitalVisits
					.Select(hv => hv.Patient)
					.OrderBy(hv => hv.FirstName)
					.ToPagedList(numOfPage, PageSize);
			}


			List<Hospital> hospitals = db.Hospitals.ToList();

			var doctors = db.MedicalStaffs
				.Include(ms => ms.Profile)
				.Include(ms => ms.MedicalStaffEmployments)
				.Where(ms => ms.MedicalStaffEmployments
					.Any(mse => db.Hospitals.Select(h => h.Id).Contains(mse.MedicalInstitutionId)))
				.ToList()
				.Select(ms => new { ms.Id, Name = ms.MedicalStaffName })
				.Concat(new[] { new { Id = 0, Name = "Any" } }); ;

			r7vm.Patients = resultPatients;
			r7vm.Hospitals = new SelectList(hospitals, "Id", "MedicalInstitutionName");
			r7vm.MedicalStaffs = new SelectList(doctors, "Id", "Name", 0);


			return View(r7vm);
		}

		public class Request8ViewModel
		{
			public IPagedList<Patient> Patients { get; set; }
			public SelectList Clinics { get; set; }
			public SelectList Profiles { get; set; }
			public int? ClinicId { get; set; }
			public int? ProfileId { get; set; }
		}

		public ActionResult Request8([Bind(Include = "ClinicId,ProfileId")] Request8ViewModel r8vm, int? clinicId, int? profileId, int? pageNum)
		{
			IQueryable<ClinicVisit> clinicVisits = db.ClinicVisits.Include( cv => cv.Patient);

			int numOfPage = (pageNum ?? 1);

			IPagedList<Patient> resultPatients = new List<Patient>().ToPagedList(numOfPage, PageSize);

			if (r8vm == null)
			{
				r8vm = new Request8ViewModel();
			}

			r8vm.ClinicId = clinicId;
			r8vm.ProfileId = profileId;

			if (clinicId != null && clinicId != 0)
			{
				clinicVisits = clinicVisits
					.Include(cv => cv.Cabinet)
					.Where(cv => cv.Cabinet.ClinicId == clinicId);
				if (profileId != null && profileId != 0)
				{
					clinicVisits = clinicVisits
						.Include(cv => cv.Doctor)
						.Where(cv => cv.Doctor.ProfileId == profileId);
				}

				resultPatients = clinicVisits
					.Select(cv => cv.Patient)
					.Distinct()
					.OrderBy(p => p.FirstName)
					.ToPagedList(numOfPage, PageSize);
			}


			List<Clinic> clinics = db.Clinics.ToList();

			var profiles = db.MedicalStaffProfiles.ToList()
				.Select(p => new { p.Id, Name = p.ProfileName })
				.Concat(new[] { new { Id = 0, Name = "Any" } });


			r8vm.Patients = resultPatients;
			r8vm.Clinics = new SelectList(clinics, "Id", "MedicalInstitutionName");
			r8vm.Profiles = new SelectList(profiles, "Id", "Name", 0);


			return View(r8vm);
		}

		public class Request9ViewModel
		{
			public IPagedList<WardAndBedsStatForHospital> HospitalDepartmentsStats { get; set; }
			public WardAndBedsStatForHospital HospitalStats { get; set; }
			public SelectList Hospitals { get; set; }
			public int? HospitalId { get; set; }
		}

		public class WardAndBedsStatForHospital
		{
			public string Name { get; set; }
			public int TotalWards { get; set; }
			public int TotalBeds { get; set; }
			public int NumOfFreeBeds { get; set; }
			public int NumOfFullyFreeWards { get; set; }
		}

		public ActionResult Request9([Bind(Include = "HospitalId")] Request9ViewModel r9vm, int? hospitalId, int? pageNum)
		{
			WardAndBedsStatForHospital hospitalStats = new WardAndBedsStatForHospital();

			List<WardAndBedsStatForHospital> hospitalDepartmentsStats = new List<WardAndBedsStatForHospital>();

			if (r9vm == null)
			{
				r9vm = new Request9ViewModel();
			}

			r9vm.HospitalId = hospitalId;

			if (hospitalId != null && hospitalId != 0)
			{

				var departments = db.HospitalDepartments
					.Include(hd => hd.HospitalBuilding)
					.Include(hd => hd.HospitalBuilding.Hospital)
					.Where(hd => hd.HospitalBuilding.HospitalId == hospitalId).ToList();

				foreach (var hospitalDepartment in departments)
				{
					var wardsForDepartmentStat = db.Wards
						.Include(w => w.HospitalDepartment)
						.Where(w => w.HospitalDepartmentId == hospitalDepartment.Id)
						.Include(w => w.HospitalVisits)
						.ToList()
						.Select(w => new WardAndBedsStatForHospital
						{
							TotalBeds = w.NumOfBeds,
							NumOfFreeBeds = w.NumOfBeds - w.HospitalVisits.Count(hv => hv.VisitEndDate == null || hv.VisitEndDate == DateTime.Today),
							NumOfFullyFreeWards = w.HospitalVisits.Count(hv => hv.VisitEndDate == null || hv.VisitEndDate == DateTime.Today) == 0 ? 1 : 0
						});
						

					WardAndBedsStatForHospital departmentStat = new WardAndBedsStatForHospital();
					foreach (var wardStat in wardsForDepartmentStat)
					{
						departmentStat.TotalBeds += wardStat.TotalBeds;
						departmentStat.NumOfFreeBeds += wardStat.NumOfFreeBeds;
						departmentStat.NumOfFullyFreeWards += wardStat.NumOfFullyFreeWards;
					}

					departmentStat.Name = hospitalDepartment.DepartmentName;
					departmentStat.TotalWards = wardsForDepartmentStat.Count();

					hospitalDepartmentsStats.Add(departmentStat);
				}

				foreach (var hds in hospitalDepartmentsStats)
				{
					hospitalStats.TotalBeds += hds.TotalBeds;
					hospitalStats.NumOfFreeBeds += hds.NumOfFreeBeds;
					hospitalStats.NumOfFullyFreeWards += hds.NumOfFullyFreeWards;
					hospitalStats.TotalWards += hds.TotalWards;
				}

				hospitalStats.Name = "Total for " + db.Hospitals.First(h => h.Id == hospitalId).MedicalInstitutionName;
			}


			List<Hospital> hospitals = db.Hospitals.ToList();

			int numOfPage = (pageNum ?? 1);



			r9vm.Hospitals = new SelectList(hospitals, "Id", "MedicalInstitutionName");
			r9vm.HospitalDepartmentsStats = hospitalDepartmentsStats.ToPagedList(numOfPage, PageSize);
			r9vm.HospitalStats = hospitalStats;


			return View(r9vm);
		}

		public class Request10ViewModel
		{
			public IPagedList<CabinetStat> CabinetStats { get; set; }
			public SelectList Clinics { get; set; }
			public int NumberOfCabinets { get; set; }
			public int? ClinicId { get; set; }

			[DataType(DataType.Date)]
			[DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
			public DateTime? Begin { get; set; }

			[DataType(DataType.Date)]
			[DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
			public DateTime? End { get; set; }
		}

		public class CabinetStat
		{
			public string CabinetName { get; set; }
			public int NumOfVisits { get; set; }
		}

		public ActionResult Request10([Bind(Include = "ClinicId,Begin,End")] Request10ViewModel r10vm, int? clinicId, DateTime? begin, DateTime? end, int? pageNum)
		{

			List<CabinetStat> cabinetStats = new List<CabinetStat>();
			var numberOfCabinets = 0;

			if (r10vm == null)
			{
				r10vm = new Request10ViewModel();
			}

			r10vm.ClinicId = clinicId;
			r10vm.Begin = begin;
			r10vm.End = end;

			if (clinicId != null && clinicId != 0)
			{
				numberOfCabinets = db.Cabinets.Count(c => c.ClinicId == clinicId);
				if (begin != null && end != null)
				{
					foreach (var cabinet in db.Cabinets.Include(c => c.Clinic).Where(c => c.ClinicId == clinicId).ToList())
					{
						var numberOfVisits = db.ClinicVisits
							.Count(c => c.VisitDate >= begin && c.VisitDate <= end && c.CabinetId == cabinet.Id);

						cabinetStats.Add(new CabinetStat
						{
							CabinetName = cabinet.CabinetName,
							NumOfVisits = numberOfVisits,
						});
					}
				}
			}

			List<Clinic> clinics = db.Clinics.ToList();
			clinics.Insert( 0, new Clinic { MedicalInstitutionName = "----Select clinic----", Id = 0});

			int numOfPage = (pageNum ?? 1);


			r10vm.CabinetStats = cabinetStats.ToPagedList(numOfPage, PageSize);
			r10vm.Clinics = new SelectList(clinics, "Id", "MedicalInstitutionName");
			r10vm.NumberOfCabinets = numberOfCabinets;


			return View(r10vm);
		}

		public class Request11ViewModel
		{
			public IPagedList<MedicalStaffProductionForClinic> MedicalStaffProductions { get; set; }
			public SelectList Clinics { get; set; }
			public SelectList Doctors { get; set; }
			public SelectList Profiles { get; set; }
			public int? ClinicId { get; set; }
			public int? MedicalStaffId { get; set; }
			public int? ProfileId { get; set; }

			[DataType(DataType.Date)]
			[DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
			public DateTime? Begin { get; set; }

			[DataType(DataType.Date)]
			[DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
			public DateTime? End { get; set; }
		}

		public class MedicalStaffProductionForClinic
		{
			public string MedicalStaffName { get; set; }
			public double AvgNumOfPatients { get; set; }
		}

		public ActionResult Request11([Bind(Include = "ClinicId,MedicalStaffId,ProfileId,Begin,End")] Request11ViewModel r11vm, int? clinicId, int? medicalStaffId, int? profileId, DateTime? begin, DateTime? end, int? pageNum)
		{
			IQueryable<MedicalStaff> medicalStaff = db.MedicalStaffs.Include(ms => ms.Profile);

			int numOfPage = (pageNum ?? 1);


			if (r11vm == null)
			{
				r11vm = new Request11ViewModel();
			}

			r11vm.MedicalStaffProductions = new List<MedicalStaffProductionForClinic>().ToPagedList(numOfPage, PageSize);
			r11vm.ClinicId = clinicId;
			r11vm.Begin = begin;
			r11vm.End = end;
			r11vm.ProfileId = profileId;
			r11vm.MedicalStaffId = medicalStaffId;

			if (begin != null && end != null)
			{
				if (medicalStaffId != null && medicalStaffId != 0)
				{
					medicalStaff = medicalStaff.Where(ms => ms.Id == medicalStaffId);
				}

				if (profileId != null && profileId != 0)
				{
					medicalStaff = medicalStaff
						.Where(ms => ms.ProfileId == profileId);
				}

				if (clinicId != null && clinicId != 0)
				{
					medicalStaff = medicalStaff
						.Include(ms => ms.MedicalStaffEmployments)
						.Where(ms => ms.MedicalStaffEmployments
							.Any(mse => mse.MedicalInstitutionId == clinicId));
				}
				else
				{
					medicalStaff = medicalStaff
						.Include(ms => ms.MedicalStaffEmployments)
						.Where(ms => ms.MedicalStaffEmployments
							.Any(mse => db.Clinics.Select(c => c.Id).Contains(mse.MedicalInstitutionId)));
				}

				List<MedicalStaffProductionForClinic> medicalStaffProductions = new List<MedicalStaffProductionForClinic>();
				foreach (var doc in medicalStaff.ToList())
				{
					medicalStaffProductions.Add(new MedicalStaffProductionForClinic
					{
						MedicalStaffName = doc.MedicalStaffName,
						AvgNumOfPatients = GetMedicalStaffProductionForClinic(doc.Id, begin.Value, end.Value)
					});
				}

				r11vm.MedicalStaffProductions = medicalStaffProductions.ToPagedList(numOfPage, PageSize);
			}

			List<Clinic> clinics = db.Clinics.ToList();
			clinics.Insert(0, new Clinic { MedicalInstitutionName = "All", Id = 0 });

			var doctors = db.MedicalStaffs
				.Include(ms => ms.Profile)
				.Include(ms => ms.MedicalStaffEmployments)
				.Where(ms => ms.MedicalStaffEmployments
					.Any(mse => db.Clinics.Select(h => h.Id).Contains(mse.MedicalInstitutionId)))
				.ToList()
				.Select(ms => new { ms.Id, Name = ms.MedicalStaffName })
				.Concat(new[] { new { Id = 0, Name = "Any" } }); ;

			var profiles = db.MedicalStaffProfiles.ToList()
				.Select(p => new { p.Id, Name = p.ProfileName })
				.Concat(new[] { new { Id = 0, Name = "Any" } });

			

			r11vm.Clinics = new SelectList(clinics, "Id", "MedicalInstitutionName", 0);
			r11vm.Doctors = new SelectList(doctors, "Id", "Name", 0);
			r11vm.Profiles = new SelectList(profiles, "Id", "Name", 0);


			return View(r11vm);
		}


		private double GetMedicalStaffProductionForClinic(int medicalStaffId, DateTime begin, DateTime end)
		{
			var numOfPatients = db.ClinicVisits
				.Where(cv => cv.DoctorId == medicalStaffId)
				.Where(cv => cv.VisitDate <= end && cv.VisitDate >= begin)
				.ToList()
				.Select(hv => hv.PatientId)
				.Count();

			double result = ((double) numOfPatients) / (end - begin).Days;
			return result;
		}

		public class Request12ViewModel
		{
			public IPagedList<MedicalStaffProductionForHospital> MedicalStaffProductions { get; set; }
			public SelectList Hospitals { get; set; }
			public SelectList Doctors { get; set; }
			public SelectList Profiles { get; set; }
			public int? HospitalId { get; set; }
			public int? MedicalStaffId { get; set; }
			public int? ProfileId { get; set; }
		}

		public ActionResult Request12([Bind(Include = "HospitalId,MedicalStaffId,ProfileId")] Request12ViewModel r12vm, int? hospitalId, int? medicalStaffId, int? profileId, int? pageNum)
		{
			IQueryable<MedicalStaff> medicalStaff = db.MedicalStaffs.Include(ms => ms.Profile);

			if (r12vm == null)
			{
				r12vm = new Request12ViewModel();
			}

			r12vm.HospitalId = hospitalId;
			r12vm.MedicalStaffId = medicalStaffId;
			r12vm.ProfileId = profileId;

			if (medicalStaffId != null && medicalStaffId != 0)
			{
				medicalStaff = medicalStaff.Where(ms => ms.Id == medicalStaffId);
			}

			if (profileId != null && profileId != 0)
			{
				medicalStaff = medicalStaff
					.Where(ms => ms.ProfileId == profileId);
			}

			if (hospitalId != null && hospitalId != 0)
			{
				medicalStaff = medicalStaff
					.Include(ms => ms.MedicalStaffEmployments)
					.Where(ms => ms.MedicalStaffEmployments
						.Any(mse => mse.MedicalInstitutionId == hospitalId));
			}
			else
			{
				medicalStaff = medicalStaff
					.Include(ms => ms.MedicalStaffEmployments)
					.Where(ms => ms.MedicalStaffEmployments
						.Any(mse => db.Hospitals.Select(h => h.Id).Contains(mse.MedicalInstitutionId)));
			}

			int numOfPage = (pageNum ?? 1);

			List<MedicalStaffProductionForHospital> medicalStaffProductions = new List<MedicalStaffProductionForHospital>();

			foreach (var doc in medicalStaff.ToList())
			{
				medicalStaffProductions.Add( new MedicalStaffProductionForHospital
				{
					MedicalStaffName = doc.MedicalStaffName,
					NumOfPatients = GetMedicalStaffProductionForHospital(doc.Id)
				});
			}

			List<Hospital> hospitals = db.Hospitals.ToList();
			hospitals.Insert(0, new Hospital { MedicalInstitutionName = "All", Id = 0 });

			var doctors = db.MedicalStaffs
				.Include(ms => ms.Profile)
				.Include(ms => ms.MedicalStaffEmployments)
				.Where(ms => ms.MedicalStaffEmployments
					.Any(mse => db.Hospitals.Select(h => h.Id).Contains(mse.MedicalInstitutionId)))
				.ToList()
				.Select(ms => new { ms.Id, Name = ms.MedicalStaffName })
				.Concat(new[] { new { Id = 0, Name = "Any" } }); ;

			var profiles = db.MedicalStaffProfiles.ToList()
				.Select(p => new { p.Id, Name = p.ProfileName })
				.Concat(new[] { new { Id = 0, Name = "Any" } }); ;


			r12vm.MedicalStaffProductions = medicalStaffProductions.ToPagedList(numOfPage, PageSize);
			r12vm.Hospitals = new SelectList(hospitals, "Id", "MedicalInstitutionName", 0);
			r12vm.Doctors = new SelectList(doctors, "Id", "Name", 0);
			r12vm.Profiles = new SelectList(profiles, "Id", "Name", 0);


			return View(r12vm);
		}

		public class MedicalStaffProductionForHospital
		{
			public string MedicalStaffName { get; set; }
			public int NumOfPatients { get; set; }
		}

		private int GetMedicalStaffProductionForHospital(int medicalStaffId)
		{
			var numOfPatients = db.HospitalVisits
				.Where(hv => hv.DoctorId == medicalStaffId)
				.Where(hv => hv.VisitEndDate == null || hv.VisitEndDate == DateTime.Today)
				.ToList()
				.Select(hv => hv.PatientId)
				.Count();

			return numOfPatients;
		}


		public class Request13ViewModel
		{
			public IPagedList<Patient> Patients { get; set; }
			public SelectList MedicalInstitutions { get; set; }
			public SelectList Surgeons { get; set; }
			public int? MedicalInstitutionId { get; set; }
			public int? SurgeonId { get; set; }

			[DataType(DataType.Date)]
			[DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
			public DateTime? Begin { get; set; }

			[DataType(DataType.Date)]
			[DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
			public DateTime? End { get; set; }
		}

		public ActionResult Request13([Bind(Include = "MedicalInstitutionId,SurgeonId,Begin,End")] Request13ViewModel r13vm, int? medicalInstitutionId, int? surgeonId, DateTime? begin, DateTime? end, int? pageNum)
		{
			IQueryable<Patient> patients = db.Patients;

			int numOfPage = (pageNum ?? 1);

			var surgeons = db.MedicalStaffs
				.Include(ms => ms.Profile)
				.Where(ms => ms.Profile.IsSurgeon).ToList()
				.Select(ms => new { ms.Id, Name = ms.MedicalStaffName })
				.Concat(new[] { new { Id = 0, Name = "All" } }); ;


			if (r13vm == null)
			{
				r13vm = new Request13ViewModel();
			}

			r13vm.MedicalInstitutionId = medicalInstitutionId;
			r13vm.SurgeonId = surgeonId;
			r13vm.Begin = begin;
			r13vm.End = end;
			r13vm.Patients = new List<Patient>().ToPagedList(numOfPage, PageSize);
			r13vm.MedicalInstitutions = new SelectList(db.MedicalInstitutions.ToList(), "Id", "MedicalInstitutionName");
			r13vm.Surgeons = new SelectList(surgeons, "Id", "Name", 0);

			if (medicalInstitutionId != null && medicalInstitutionId != 0 && begin != null && end != null)
			{
				var medicalInstitution = db.MedicalInstitutions.First(mi => mi.Id == medicalInstitutionId);
				if (medicalInstitution is Hospital)
				{
					var hospitalVisitPatients = db.HospitalVisits
						.Include(hv => hv.Operations)
						.Include(hv => hv.Ward)
						.Include(hv => hv.Ward.HospitalDepartment)
						.Include(hv => hv.Ward.HospitalDepartment.HospitalBuilding)
						.Where(hv => hv.Ward.HospitalDepartment.HospitalBuilding.HospitalId == medicalInstitutionId)
						.Where(hv => hv.Operations
							.Any(o => o.OperationDate >= begin && o.OperationDate <= end && 
							          (surgeonId == null || surgeonId == 0 || o.SurgeonId == surgeonId)))
						.ToList()
						.Select(hv => hv.PatientId);

					patients = patients
						.Where(p => hospitalVisitPatients.Contains(p.Id));
				}
				else if (medicalInstitution is Clinic)
				{
					var clinicVisitPatients = db.ClinicVisits
						.Include(cv => cv.Operations)
						.Include(cv => cv.Cabinet)
						.Where(cv => cv.Cabinet.ClinicId == medicalInstitutionId)
						.Where(cv => cv.Operations
							.Any(o => o.OperationDate >= begin && o.OperationDate <= end &&
							          (surgeonId == null || surgeonId == 0 || o.SurgeonId == surgeonId)))
						.ToList()
						.Select(cv => cv.PatientId);

					patients = patients
						.Where(p => clinicVisitPatients.Contains(p.Id));
				}

				r13vm.Patients = patients.OrderBy(p => p.FirstName).ToPagedList(numOfPage, PageSize);
			}

			return View(r13vm);
		}

		public class Request14ViewModel
		{
			public IPagedList<ProductionData> ProductionDatas { get; set; }
			public SelectList MedicalInstitutions { get; set; }
			public SelectList Laboratories { get; set; }
			public int? LaboratoryId { get; set; }
			public int? MedicalInstitutionId { get; set; }

			[DataType(DataType.Date)]
			[DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
			public DateTime? Begin { get; set; }

			[DataType(DataType.Date)]

			public DateTime? End { get; set; }
 		}

		public ActionResult Request14([Bind(Include = "LaboratoryId,MedicalInstitutionId,Begin,End")] Request14ViewModel r14vm, int? laboratoryId, int? medicalInstitutionId, DateTime? begin, DateTime? end, int? pageNum)
		{
			int numOfPage = (pageNum ?? 1);
			List<ProductionData> prodData = new List<ProductionData>();

			if (r14vm == null)
			{
				r14vm = new Request14ViewModel();
			}

			r14vm.MedicalInstitutionId = medicalInstitutionId;
			r14vm.LaboratoryId = laboratoryId;
			r14vm.Begin = begin;
			r14vm.End = end;


			if (begin != null && end != null)
			{
				if (laboratoryId != null && laboratoryId != 0)
				{
					prodData.Add(GetAverageProductionForLabAndMedicalInstitution(medicalInstitutionId,
								laboratoryId.Value, begin.Value, end.Value));
				}
				else
				{
					foreach (var labId in db.Laboratories.Select(l => l.Id).ToList())
					{
						prodData.Add(GetAverageProductionForLabAndMedicalInstitution(medicalInstitutionId,
							labId, begin.Value, end.Value));
					}
				}
			}

			var medicalInstitutions = db.MedicalInstitutions.ToList()
				.Select( mi => new { mi.Id, Name = mi.MedicalInstitutionName})
				.Concat(new[] { new { Id = 0, Name = "All" } });


			var laboratories = db.Laboratories.ToList()
				.Select(l => new { l.Id, Name = l.LaboratoryName })
				.Concat(new[] { new { Id = 0, Name = "All" } });;


			r14vm.ProductionDatas = prodData.ToPagedList(numOfPage, PageSize);
			r14vm.MedicalInstitutions = new SelectList(medicalInstitutions, "Id", "Name", 0);
			r14vm.Laboratories = new SelectList(laboratories, "Id", "Name", 0);


			return View(r14vm);
		}

		public class ProductionData
		{
			public string MedicalInstitutionName { get; set; }
			public string LaboratoryName { get; set; }
			public double AverageProduction { get; set; }
		}

		private ProductionData GetAverageProductionForLabAndMedicalInstitution(int? medicalInstitutionId, int labId, DateTime begin, DateTime end)
		{
			IQueryable <Analysis> analyzes = db.Analyzes;


			analyzes = analyzes.Where(a => a.LaboratoryId == labId).Where(a => a.AnalysisDate <= end && a.AnalysisDate >= begin);


			var medicalInstitutionName = "All";
			var numOfAnalyzes = 0;
			if (medicalInstitutionId != null && medicalInstitutionId != 0)
			{
				var medicalInstitution = db.MedicalInstitutions.First(mi => mi.Id == medicalInstitutionId);
				medicalInstitutionName = medicalInstitution.MedicalInstitutionName;
				if (medicalInstitution is Hospital)
				{
					var hospitalVisits = db.HospitalVisits
						.Include(hv => hv.Ward)
						.Include(hv => hv.Ward.HospitalDepartment)
						.Include(hv => hv.Ward.HospitalDepartment.HospitalBuilding)
						.Where(hv => hv.Ward.HospitalDepartment.HospitalBuilding.HospitalId == medicalInstitutionId)
						.ToList()
						.Select(hv => hv.Id);

					analyzes = analyzes
						.Where(a => hospitalVisits.Contains(a.MedicalInstitutionVisitId.Value));

					numOfAnalyzes = analyzes.ToList().Count;
				}
				else if (medicalInstitution is Clinic)
				{
					var clinicVisits = db.ClinicVisits
						.Include(cv => cv.Cabinet)
						.Where(cv => cv.Cabinet.ClinicId == medicalInstitutionId)
						.ToList()
						.Select(cv => cv.Id);

					analyzes = analyzes
						.Where(a => clinicVisits.Contains(a.MedicalInstitutionVisitId.Value));

					numOfAnalyzes = analyzes.ToList().Count;
				}
			}
			else
			{
				numOfAnalyzes = analyzes.Count();
			}

			double averageProduction = ((double)numOfAnalyzes) / ((end - begin).Days);

			var laboratory = db.Laboratories.First(l => l.Id == labId);

			return new ProductionData
			{
				MedicalInstitutionName = medicalInstitutionName,
				LaboratoryName = laboratory.LaboratoryName,
				AverageProduction = averageProduction,
			};

		}
	}
}
