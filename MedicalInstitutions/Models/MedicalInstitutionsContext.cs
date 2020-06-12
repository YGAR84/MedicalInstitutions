using System.Collections.Generic;
using System.Data.Entity;
using MedicalInstitutions.Models.Patients;
using MedicalInstitutions.Models.Staffs.MedicalStaffs;
using MedicalInstitutions.Models.Diseases;
using MedicalInstitutions.Models.MedicalInstitutions;
using MedicalInstitutions.Models.MedicalInstitutions.Clinics;
using MedicalInstitutions.Models.MedicalInstitutions.Hospitals;
using MedicalInstitutions.Models.MedicalInstitutions.Laboratories;

using MedicalInstitutions.Models.Staffs.ServiceStaffs;
using MedicalInstitutions.Models.Patients.Visit;
using Microsoft.AspNet.Identity.EntityFramework;

namespace MedicalInstitutions.Models
{
    public class MedicalInstitutionsContext : IdentityDbContext<ApplicationUser>
	{
		public MedicalInstitutionsContext() : base("MedicalInstitutionsContext") { }

		public DbSet<Disease> Diseases { get; set; }
		public DbSet<DiseaseGroup> DiseaseGroups { get; set; }

		public DbSet<MedicalInstitution> MedicalInstitutions { get; set; }
		public DbSet<Hospital> Hospitals { get; set; }
		public DbSet<HospitalBuilding> HospitalBuildings { get; set; }
		public DbSet<HospitalDepartment> HospitalDepartments { get; set; }
		public DbSet<Ward> Wards { get; set; }

		public DbSet<Clinic> Clinics { get; set; }
		public DbSet<Cabinet> Cabinets { get; set; }

		public DbSet<Laboratory> Laboratories { get; set; }
		public DbSet<LaboratorySpecialization> LaboratorySpecializations { get; set; }
		public DbSet<Analysis> Analyzes { get; set; }

		public DbSet<LaboratoryContract> LaboratoryContracts { get; set; }

		public DbSet<Patient> Patients { get; set; }
		public DbSet<Operation> Operations { get; set; }

		public DbSet<MedicalInstitutionVisit> MedicalInstitutionVisits { get; set; }
		public DbSet<ClinicVisit> ClinicVisits { get; set; }
		public DbSet<HospitalVisit> HospitalVisits { get; set; }

		public DbSet<MedicalStaff> MedicalStaffs { get; set; }
		public DbSet<MedicalStaffProfile> MedicalStaffProfiles { get; set; }
		public DbSet<MedicalStaffEmployment> MedicalStaffEmployments { get; set; }

		public DbSet<ServiceStaff> ServiceStaffs { get; set; }
		public DbSet<ServiceStaffSpecialty> ServiceStaffSpecialties { get; set; }
		public DbSet<ServiceStaffEmployment> ServiceStaffEmployments { get; set; }

		//public DbSet<Surgeon> Surgeons { get; set; }

		//public DbSet<Dentist> Dentists { get; set; }
		//public DbSet<Gynecologist> Gynecologists { get; set; }
		//public DbSet<GeneralSurgeon> GeneralSurgeons { get; set; }

		//public DbSet<Neuropathologist> Neuropathologists { get; set; }
		//public DbSet<Oculist> Oculists { get; set; }
		//public DbSet<Radiologist> Radiologists { get; set; }
		//public DbSet<Therapist> Therapists { get; set; }

		//public DbSet<ServiceStaff> ServiceStaffs { get; set; }
		//public DbSet<Janitor> Janitors { get; set; }
		//public DbSet<Nurse> Nurses { get; set; }
		//public DbSet<Orderly> Orderlies { get; set; }


		protected override void OnModelCreating(DbModelBuilder modelBuilder)
		{
			base.OnModelCreating(modelBuilder);
			//Database.SetInitializer<MedicalInstitutionsContext>(null);
			//base.OnModelCreating(modelBuilder);

			//modelBuilder.Entity<MedicalInstitution>()
			//	.HasMany(m => m.Consultants)
			//	.WithMany(i => i.ConsultedMedicalInstitutions);

			modelBuilder.Entity<Laboratory>()
				.HasMany(l => l.LaboratorySpecializations)
				.WithMany(ls => ls.Laboratories);

			modelBuilder.Entity<MedicalStaff>()
				.HasOptional(ms => ms.Profile)
				.WithMany()
				.WillCascadeOnDelete(false);

			modelBuilder.Entity<MedicalInstitutionVisit>()
				.HasOptional(miv => miv.Doctor)
				.WithMany()
				.WillCascadeOnDelete(false);

			modelBuilder.Entity<Ward>()
				.HasOptional(w => w.HospitalDepartment)
				.WithMany()
				.WillCascadeOnDelete(false);

			//modelBuilder.Entity<Analysis>()
			//	.HasOptional(a => a.MedicalInstitutionVisit)
			//	.WithMany()
			//	.WillCascadeOnDelete(false);


			//modelBuilder.Entity<MedicalStaff>()
			//	.HasOptional(ms => ms.DiseaseGroup)
			//	.WithMany()
			//	.WillCascadeOnDelete(false);


			//modelBuilder.Entity<MedicalInstitutions.MedicalInstitution>()
			//	.HasMany(m => m.Doctors)
			//	.WithMany(i => i.Cons);

			//modelBuilder.Entity<Hospital>()
			//	.HasMany(medicalStaff => medicalStaff.Consultants)
			//	.WithMany(i => i.ConsultedHospitals);


		}

		public static MedicalInstitutionsContext Create()
		{
			return new MedicalInstitutionsContext();
		}

		//public System.Data.Entity.DbSet<MedicalInstitutions.Models.ApplicationUser> ApplicationUsers { get; set; }

		//public DbSet<ApplicationUser> ApplicationUsers { get; set; }
	}

	public class MedicalInstitutionsDbInitializer : DropCreateDatabaseIfModelChanges<MedicalInstitutionsContext>//
                                                    //DropCreateDatabaseAlways<MedicalInstitutionsContext>
	{
	    protected override void Seed(MedicalInstitutionsContext context)
	    {
		    //context.Dentists.AddRange(_dentistsToAdd);
			//context.Hospitals.AddRange(_hospitalsToAdd);
			context.SaveChanges();
            base.Seed(context);
        }

	    //private readonly IEnumerable<Dentist> _dentistsToAdd = new List<Dentist>
	    //{
		   // new Dentist
		   // {
			  //  FirstName = "ALALA",
			  //  SecondName = "BBB",
			  //  Degree = MedicalDegree.Docent,
			  //  Salary = 45000.00M,
			  //  Vacation = 45
		   // },
		   // new Dentist
		   // {
			  //  FirstName = "ALALA",
			  //  SecondName = "BBB",
			  //  Degree = MedicalDegree.Docent,
			  //  Salary = 45000.00M,
			  //  Vacation = 45
		   // }
	    //};

	    private readonly IEnumerable<Hospital> _hospitalsToAdd = new List<Hospital>
	    {
		    new Hospital { MedicalInstitutionName = "chi" },
		    new Hospital { MedicalInstitutionName = "da" },
		};

	}


    public enum DiseaseAndConditionGroup
    {
	    InfectiousDiseases,
	    ToothDiseases,
	    Pregnancy,

	    Injuries,

    }
}