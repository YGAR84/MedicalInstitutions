namespace MedicalInstitutions.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class InitialCreate : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Cabinets",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Number = c.Int(nullable: false),
                        ClinicId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.MedicalInstitutions", t => t.ClinicId, cascadeDelete: true)
                .Index(t => t.ClinicId);
            
            CreateTable(
                "dbo.MedicalInstitutions",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        MedicalInstitutionName = c.String(nullable: false),
                        Address = c.String(nullable: false),
                        HospitalId = c.Int(),
                        Discriminator = c.String(nullable: false, maxLength: 128),
                        Laboratory_Id = c.Int(),
                        Laboratory_Id1 = c.Int(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Laboratories", t => t.Laboratory_Id)
                .ForeignKey("dbo.Laboratories", t => t.Laboratory_Id1)
                .ForeignKey("dbo.MedicalInstitutions", t => t.HospitalId)
                .Index(t => t.HospitalId)
                .Index(t => t.Laboratory_Id)
                .Index(t => t.Laboratory_Id1);
            
            CreateTable(
                "dbo.MedicalStaffs",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Degree = c.Int(nullable: false),
                        HospitalDepartmentId = c.Int(),
                        ClinicId = c.Int(),
                        Salary = c.Decimal(nullable: false, precision: 18, scale: 2),
                        Vacation = c.Int(nullable: false),
                        EmploymentDate = c.DateTime(nullable: false),
                        SalaryAddition = c.Decimal(nullable: false, precision: 18, scale: 2),
                        VacationAddition = c.Int(nullable: false),
                        FirstName = c.String(nullable: false),
                        SecondName = c.String(nullable: false),
                        Discriminator = c.String(nullable: false, maxLength: 128),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.MedicalInstitutions", t => t.ClinicId)
                .ForeignKey("dbo.HospitalDepartments", t => t.HospitalDepartmentId)
                .Index(t => t.HospitalDepartmentId)
                .Index(t => t.ClinicId);
            
            CreateTable(
                "dbo.Laboratories",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Address = c.String(nullable: false),
                        MedicalInstitution_Id = c.Int(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.MedicalInstitutions", t => t.MedicalInstitution_Id)
                .Index(t => t.MedicalInstitution_Id);
            
            CreateTable(
                "dbo.LaboratorySpecializations",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(nullable: false),
                        Laboratory_Id = c.Int(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Laboratories", t => t.Laboratory_Id)
                .Index(t => t.Laboratory_Id);
            
            CreateTable(
                "dbo.HospitalDepartments",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        DiseaseGroupId = c.Int(nullable: false),
                        HospitalBuildingId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.DiseaseGroups", t => t.DiseaseGroupId, cascadeDelete: true)
                .ForeignKey("dbo.HospitalBuildings", t => t.HospitalBuildingId, cascadeDelete: true)
                .Index(t => t.DiseaseGroupId)
                .Index(t => t.HospitalBuildingId);
            
            CreateTable(
                "dbo.DiseaseGroups",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.HospitalBuildings",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        HospitalId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.MedicalInstitutions", t => t.HospitalId, cascadeDelete: true)
                .Index(t => t.HospitalId);
            
            CreateTable(
                "dbo.Operations",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        MedicalInstitutionVisitId = c.Int(nullable: false),
                        SurgeonId = c.Int(nullable: false),
                        OperationDate = c.DateTime(nullable: false),
                        OperationResult = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.MedicalInstitutionVisits", t => t.MedicalInstitutionVisitId, cascadeDelete: true)
                .ForeignKey("dbo.MedicalStaffs", t => t.SurgeonId, cascadeDelete: true)
                .Index(t => t.MedicalInstitutionVisitId)
                .Index(t => t.SurgeonId);
            
            CreateTable(
                "dbo.MedicalInstitutionVisits",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        PatientId = c.Int(nullable: false),
                        DoctorId = c.Int(),
                        VisitDate = c.DateTime(nullable: false),
                        DiseaseId = c.Int(nullable: false),
                        CabinetId = c.Int(),
                        WardId = c.Int(),
                        VisitEndDate = c.DateTime(),
                        Temperature = c.Decimal(precision: 18, scale: 2),
                        PatientCondition = c.Int(),
                        Discriminator = c.String(nullable: false, maxLength: 128),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Diseases", t => t.DiseaseId, cascadeDelete: true)
                .ForeignKey("dbo.MedicalStaffs", t => t.DoctorId)
                .ForeignKey("dbo.Patients", t => t.PatientId, cascadeDelete: true)
                .ForeignKey("dbo.Cabinets", t => t.CabinetId, cascadeDelete: true)
                .ForeignKey("dbo.Wards", t => t.WardId, cascadeDelete: true)
                .Index(t => t.PatientId)
                .Index(t => t.DoctorId)
                .Index(t => t.DiseaseId)
                .Index(t => t.CabinetId)
                .Index(t => t.WardId);
            
            CreateTable(
                "dbo.Diseases",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        DiseaseName = c.String(nullable: false),
                        DiseaseGroupId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.DiseaseGroups", t => t.DiseaseGroupId, cascadeDelete: true)
                .Index(t => t.DiseaseGroupId);
            
            CreateTable(
                "dbo.Patients",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        FirstName = c.String(nullable: false),
                        SecondName = c.String(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.Wards",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Number = c.Int(nullable: false),
                        NumOfBeds = c.Int(nullable: false),
                        HospitalDepartmentId = c.Int(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.HospitalDepartments", t => t.HospitalDepartmentId)
                .Index(t => t.HospitalDepartmentId);
            
            CreateTable(
                "dbo.ServiceStaffs",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Salary = c.Decimal(nullable: false, precision: 18, scale: 2),
                        Vacation = c.Int(nullable: false),
                        EmploymentDate = c.DateTime(nullable: false),
                        SalaryAddition = c.Decimal(nullable: false, precision: 18, scale: 2),
                        VacationAddition = c.Int(nullable: false),
                        FirstName = c.String(nullable: false),
                        SecondName = c.String(nullable: false),
                        Discriminator = c.String(nullable: false, maxLength: 128),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.AspNetRoles",
                c => new
                    {
                        Id = c.String(nullable: false, maxLength: 128),
                        Name = c.String(nullable: false, maxLength: 256),
                    })
                .PrimaryKey(t => t.Id)
                .Index(t => t.Name, unique: true, name: "RoleNameIndex");
            
            CreateTable(
                "dbo.AspNetUserRoles",
                c => new
                    {
                        UserId = c.String(nullable: false, maxLength: 128),
                        RoleId = c.String(nullable: false, maxLength: 128),
                    })
                .PrimaryKey(t => new { t.UserId, t.RoleId })
                .ForeignKey("dbo.AspNetRoles", t => t.RoleId, cascadeDelete: true)
                .ForeignKey("dbo.AspNetUsers", t => t.UserId, cascadeDelete: true)
                .Index(t => t.UserId)
                .Index(t => t.RoleId);
            
            CreateTable(
                "dbo.AspNetUsers",
                c => new
                    {
                        Id = c.String(nullable: false, maxLength: 128),
                        Email = c.String(maxLength: 256),
                        EmailConfirmed = c.Boolean(nullable: false),
                        PasswordHash = c.String(),
                        SecurityStamp = c.String(),
                        PhoneNumber = c.String(),
                        PhoneNumberConfirmed = c.Boolean(nullable: false),
                        TwoFactorEnabled = c.Boolean(nullable: false),
                        LockoutEndDateUtc = c.DateTime(),
                        LockoutEnabled = c.Boolean(nullable: false),
                        AccessFailedCount = c.Int(nullable: false),
                        UserName = c.String(nullable: false, maxLength: 256),
                    })
                .PrimaryKey(t => t.Id)
                .Index(t => t.UserName, unique: true, name: "UserNameIndex");
            
            CreateTable(
                "dbo.AspNetUserClaims",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        UserId = c.String(nullable: false, maxLength: 128),
                        ClaimType = c.String(),
                        ClaimValue = c.String(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.AspNetUsers", t => t.UserId, cascadeDelete: true)
                .Index(t => t.UserId);
            
            CreateTable(
                "dbo.AspNetUserLogins",
                c => new
                    {
                        LoginProvider = c.String(nullable: false, maxLength: 128),
                        ProviderKey = c.String(nullable: false, maxLength: 128),
                        UserId = c.String(nullable: false, maxLength: 128),
                    })
                .PrimaryKey(t => new { t.LoginProvider, t.ProviderKey, t.UserId })
                .ForeignKey("dbo.AspNetUsers", t => t.UserId, cascadeDelete: true)
                .Index(t => t.UserId);
            
            CreateTable(
                "dbo.Аnalysis",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        AnalysisName = c.String(nullable: false),
                        PatientId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Patients", t => t.PatientId, cascadeDelete: true)
                .Index(t => t.PatientId);
            
            CreateTable(
                "dbo.MedicalInstitutionMedicalStaffs",
                c => new
                    {
                        MedicalInstitution_Id = c.Int(nullable: false),
                        MedicalStaff_Id = c.Int(nullable: false),
                    })
                .PrimaryKey(t => new { t.MedicalInstitution_Id, t.MedicalStaff_Id })
                .ForeignKey("dbo.MedicalInstitutions", t => t.MedicalInstitution_Id, cascadeDelete: true)
                .ForeignKey("dbo.MedicalStaffs", t => t.MedicalStaff_Id, cascadeDelete: true)
                .Index(t => t.MedicalInstitution_Id)
                .Index(t => t.MedicalStaff_Id);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Аnalysis", "PatientId", "dbo.Patients");
            DropForeignKey("dbo.AspNetUserRoles", "UserId", "dbo.AspNetUsers");
            DropForeignKey("dbo.AspNetUserLogins", "UserId", "dbo.AspNetUsers");
            DropForeignKey("dbo.AspNetUserClaims", "UserId", "dbo.AspNetUsers");
            DropForeignKey("dbo.AspNetUserRoles", "RoleId", "dbo.AspNetRoles");
            DropForeignKey("dbo.Cabinets", "ClinicId", "dbo.MedicalInstitutions");
            DropForeignKey("dbo.MedicalInstitutions", "HospitalId", "dbo.MedicalInstitutions");
            DropForeignKey("dbo.Operations", "SurgeonId", "dbo.MedicalStaffs");
            DropForeignKey("dbo.MedicalInstitutionVisits", "WardId", "dbo.Wards");
            DropForeignKey("dbo.Wards", "HospitalDepartmentId", "dbo.HospitalDepartments");
            DropForeignKey("dbo.MedicalInstitutionVisits", "CabinetId", "dbo.Cabinets");
            DropForeignKey("dbo.MedicalInstitutionVisits", "PatientId", "dbo.Patients");
            DropForeignKey("dbo.Operations", "MedicalInstitutionVisitId", "dbo.MedicalInstitutionVisits");
            DropForeignKey("dbo.MedicalInstitutionVisits", "DoctorId", "dbo.MedicalStaffs");
            DropForeignKey("dbo.MedicalInstitutionVisits", "DiseaseId", "dbo.Diseases");
            DropForeignKey("dbo.Diseases", "DiseaseGroupId", "dbo.DiseaseGroups");
            DropForeignKey("dbo.MedicalStaffs", "HospitalDepartmentId", "dbo.HospitalDepartments");
            DropForeignKey("dbo.HospitalDepartments", "HospitalBuildingId", "dbo.HospitalBuildings");
            DropForeignKey("dbo.HospitalBuildings", "HospitalId", "dbo.MedicalInstitutions");
            DropForeignKey("dbo.HospitalDepartments", "DiseaseGroupId", "dbo.DiseaseGroups");
            DropForeignKey("dbo.Laboratories", "MedicalInstitution_Id", "dbo.MedicalInstitutions");
            DropForeignKey("dbo.LaboratorySpecializations", "Laboratory_Id", "dbo.Laboratories");
            DropForeignKey("dbo.MedicalInstitutions", "Laboratory_Id1", "dbo.Laboratories");
            DropForeignKey("dbo.MedicalInstitutions", "Laboratory_Id", "dbo.Laboratories");
            DropForeignKey("dbo.MedicalInstitutionMedicalStaffs", "MedicalStaff_Id", "dbo.MedicalStaffs");
            DropForeignKey("dbo.MedicalInstitutionMedicalStaffs", "MedicalInstitution_Id", "dbo.MedicalInstitutions");
            DropForeignKey("dbo.MedicalStaffs", "ClinicId", "dbo.MedicalInstitutions");
            DropIndex("dbo.MedicalInstitutionMedicalStaffs", new[] { "MedicalStaff_Id" });
            DropIndex("dbo.MedicalInstitutionMedicalStaffs", new[] { "MedicalInstitution_Id" });
            DropIndex("dbo.Аnalysis", new[] { "PatientId" });
            DropIndex("dbo.AspNetUserLogins", new[] { "UserId" });
            DropIndex("dbo.AspNetUserClaims", new[] { "UserId" });
            DropIndex("dbo.AspNetUsers", "UserNameIndex");
            DropIndex("dbo.AspNetUserRoles", new[] { "RoleId" });
            DropIndex("dbo.AspNetUserRoles", new[] { "UserId" });
            DropIndex("dbo.AspNetRoles", "RoleNameIndex");
            DropIndex("dbo.Wards", new[] { "HospitalDepartmentId" });
            DropIndex("dbo.Diseases", new[] { "DiseaseGroupId" });
            DropIndex("dbo.MedicalInstitutionVisits", new[] { "WardId" });
            DropIndex("dbo.MedicalInstitutionVisits", new[] { "CabinetId" });
            DropIndex("dbo.MedicalInstitutionVisits", new[] { "DiseaseId" });
            DropIndex("dbo.MedicalInstitutionVisits", new[] { "DoctorId" });
            DropIndex("dbo.MedicalInstitutionVisits", new[] { "PatientId" });
            DropIndex("dbo.Operations", new[] { "SurgeonId" });
            DropIndex("dbo.Operations", new[] { "MedicalInstitutionVisitId" });
            DropIndex("dbo.HospitalBuildings", new[] { "HospitalId" });
            DropIndex("dbo.HospitalDepartments", new[] { "HospitalBuildingId" });
            DropIndex("dbo.HospitalDepartments", new[] { "DiseaseGroupId" });
            DropIndex("dbo.LaboratorySpecializations", new[] { "Laboratory_Id" });
            DropIndex("dbo.Laboratories", new[] { "MedicalInstitution_Id" });
            DropIndex("dbo.MedicalStaffs", new[] { "ClinicId" });
            DropIndex("dbo.MedicalStaffs", new[] { "HospitalDepartmentId" });
            DropIndex("dbo.MedicalInstitutions", new[] { "Laboratory_Id1" });
            DropIndex("dbo.MedicalInstitutions", new[] { "Laboratory_Id" });
            DropIndex("dbo.MedicalInstitutions", new[] { "HospitalId" });
            DropIndex("dbo.Cabinets", new[] { "ClinicId" });
            DropTable("dbo.MedicalInstitutionMedicalStaffs");
            DropTable("dbo.Аnalysis");
            DropTable("dbo.AspNetUserLogins");
            DropTable("dbo.AspNetUserClaims");
            DropTable("dbo.AspNetUsers");
            DropTable("dbo.AspNetUserRoles");
            DropTable("dbo.AspNetRoles");
            DropTable("dbo.ServiceStaffs");
            DropTable("dbo.Wards");
            DropTable("dbo.Patients");
            DropTable("dbo.Diseases");
            DropTable("dbo.MedicalInstitutionVisits");
            DropTable("dbo.Operations");
            DropTable("dbo.HospitalBuildings");
            DropTable("dbo.DiseaseGroups");
            DropTable("dbo.HospitalDepartments");
            DropTable("dbo.LaboratorySpecializations");
            DropTable("dbo.Laboratories");
            DropTable("dbo.MedicalStaffs");
            DropTable("dbo.MedicalInstitutions");
            DropTable("dbo.Cabinets");
        }
    }
}
