namespace MedicalInstitutions.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class ChangeSchema : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.MedicalStaffs", "ClinicId", "dbo.MedicalInstitutions");
            DropForeignKey("dbo.MedicalInstitutionMedicalStaffs", "MedicalInstitution_Id", "dbo.MedicalInstitutions");
            DropForeignKey("dbo.MedicalInstitutionMedicalStaffs", "MedicalStaff_Id", "dbo.MedicalStaffs");
            DropForeignKey("dbo.MedicalStaffs", "MedicalInstitution_Id", "dbo.MedicalInstitutions");
            DropForeignKey("dbo.Laboratories", "MedicalInstitution_Id", "dbo.MedicalInstitutions");
            DropForeignKey("dbo.ServiceStaffMedicalInstitutions", "ServiceStaff_Id", "dbo.ServiceStaffs");
            DropForeignKey("dbo.ServiceStaffMedicalInstitutions", "MedicalInstitution_Id", "dbo.MedicalInstitutions");
            DropForeignKey("dbo.MedicalStaffs", "DiseaseGroupId", "dbo.DiseaseGroups");
            DropForeignKey("dbo.MedicalStaffs", "HospitalDepartmentId", "dbo.HospitalDepartments");
            DropForeignKey("dbo.MedicalInstitutions", "Laboratory_Id", "dbo.Laboratories");
            DropForeignKey("dbo.MedicalInstitutions", "Laboratory_Id1", "dbo.Laboratories");
            DropIndex("dbo.Laboratories", new[] { "MedicalInstitution_Id" });
            DropIndex("dbo.MedicalInstitutions", new[] { "Laboratory_Id" });
            DropIndex("dbo.MedicalInstitutions", new[] { "Laboratory_Id1" });
            DropIndex("dbo.MedicalStaffs", new[] { "HospitalDepartmentId" });
            DropIndex("dbo.MedicalStaffs", new[] { "ClinicId" });
            DropIndex("dbo.MedicalStaffs", new[] { "DiseaseGroupId" });
            DropIndex("dbo.MedicalStaffs", new[] { "MedicalInstitution_Id" });
            DropIndex("dbo.MedicalInstitutionMedicalStaffs", new[] { "MedicalInstitution_Id" });
            DropIndex("dbo.MedicalInstitutionMedicalStaffs", new[] { "MedicalStaff_Id" });
            DropIndex("dbo.ServiceStaffMedicalInstitutions", new[] { "ServiceStaff_Id" });
            DropIndex("dbo.ServiceStaffMedicalInstitutions", new[] { "MedicalInstitution_Id" });
            CreateTable(
                "dbo.ServiceStaffEmployments",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        ServiceStaffId = c.Int(nullable: false),
                        MedicalInstitutionId = c.Int(nullable: false),
                        Salary = c.Decimal(nullable: false, precision: 18, scale: 2),
                        Vacation = c.Int(nullable: false),
                        EmploymentDate = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.MedicalInstitutions", t => t.MedicalInstitutionId, cascadeDelete: true)
                .ForeignKey("dbo.ServiceStaffs", t => t.ServiceStaffId, cascadeDelete: true)
                .Index(t => t.ServiceStaffId)
                .Index(t => t.MedicalInstitutionId);
            
            CreateTable(
                "dbo.MedicalStaffEmployments",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        MedicalStaffId = c.Int(nullable: false),
                        EmploymentType = c.Int(nullable: false),
                        MedicalInstitutionId = c.Int(nullable: false),
                        Salary = c.Decimal(nullable: false, precision: 18, scale: 2),
                        Vacation = c.Int(nullable: false),
                        EmploymentDate = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.MedicalInstitutions", t => t.MedicalInstitutionId, cascadeDelete: true)
                .ForeignKey("dbo.MedicalStaffs", t => t.MedicalStaffId, cascadeDelete: true)
                .Index(t => t.MedicalStaffId)
                .Index(t => t.MedicalInstitutionId);
            
            CreateTable(
                "dbo.MedicalStaffProfiles",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        ProfileName = c.String(nullable: false, maxLength: 30),
                        DiseaseGroupId = c.Int(nullable: false),
                        SalaryAddition = c.Decimal(nullable: false, precision: 18, scale: 2),
                        VacationAddition = c.Int(nullable: false),
                        IsSurgeon = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.DiseaseGroups", t => t.DiseaseGroupId, cascadeDelete: true)
                .Index(t => t.DiseaseGroupId);
            
            CreateTable(
                "dbo.ServiceStaffSpecialties",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        SpecialtyName = c.String(nullable: false, maxLength: 30),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.MedicalInstitutionLaboratories",
                c => new
                    {
                        MedicalInstitution_Id = c.Int(nullable: false),
                        Laboratory_Id = c.Int(nullable: false),
                    })
                .PrimaryKey(t => new { t.MedicalInstitution_Id, t.Laboratory_Id })
                .ForeignKey("dbo.MedicalInstitutions", t => t.MedicalInstitution_Id, cascadeDelete: true)
                .ForeignKey("dbo.Laboratories", t => t.Laboratory_Id, cascadeDelete: true)
                .Index(t => t.MedicalInstitution_Id)
                .Index(t => t.Laboratory_Id);
            
            AddColumn("dbo.MedicalStaffs", "ProfileId", c => c.Int());
            AddColumn("dbo.ServiceStaffs", "SpecialtyId", c => c.Int(nullable: false));
            AlterColumn("dbo.MedicalStaffs", "Degree", c => c.Int());
            CreateIndex("dbo.MedicalStaffs", "ProfileId");
            CreateIndex("dbo.ServiceStaffs", "SpecialtyId");
            AddForeignKey("dbo.MedicalStaffs", "ProfileId", "dbo.MedicalStaffProfiles", "Id");
            AddForeignKey("dbo.ServiceStaffs", "SpecialtyId", "dbo.ServiceStaffSpecialties", "Id", cascadeDelete: true);
            DropColumn("dbo.Laboratories", "MedicalInstitution_Id");
            DropColumn("dbo.MedicalInstitutions", "Laboratory_Id");
            DropColumn("dbo.MedicalInstitutions", "Laboratory_Id1");
            DropColumn("dbo.MedicalStaffs", "HospitalDepartmentId");
            DropColumn("dbo.MedicalStaffs", "ClinicId");
            DropColumn("dbo.MedicalStaffs", "DiseaseGroupId");
            DropColumn("dbo.MedicalStaffs", "Salary");
            DropColumn("dbo.MedicalStaffs", "Vacation");
            DropColumn("dbo.MedicalStaffs", "EmploymentDate");
            DropColumn("dbo.MedicalStaffs", "SalaryAddition");
            DropColumn("dbo.MedicalStaffs", "VacationAddition");
            DropColumn("dbo.MedicalStaffs", "Discriminator");
            DropColumn("dbo.MedicalStaffs", "MedicalInstitution_Id");
            DropColumn("dbo.ServiceStaffs", "Salary");
            DropColumn("dbo.ServiceStaffs", "Vacation");
            DropColumn("dbo.ServiceStaffs", "EmploymentDate");
            DropColumn("dbo.ServiceStaffs", "SalaryAddition");
            DropColumn("dbo.ServiceStaffs", "VacationAddition");
            DropColumn("dbo.ServiceStaffs", "Discriminator");
            DropTable("dbo.MedicalInstitutionMedicalStaffs");
            DropTable("dbo.ServiceStaffMedicalInstitutions");
        }
        
        public override void Down()
        {
            CreateTable(
                "dbo.ServiceStaffMedicalInstitutions",
                c => new
                    {
                        ServiceStaff_Id = c.Int(nullable: false),
                        MedicalInstitution_Id = c.Int(nullable: false),
                    })
                .PrimaryKey(t => new { t.ServiceStaff_Id, t.MedicalInstitution_Id });
            
            CreateTable(
                "dbo.MedicalInstitutionMedicalStaffs",
                c => new
                    {
                        MedicalInstitution_Id = c.Int(nullable: false),
                        MedicalStaff_Id = c.Int(nullable: false),
                    })
                .PrimaryKey(t => new { t.MedicalInstitution_Id, t.MedicalStaff_Id });
            
            AddColumn("dbo.ServiceStaffs", "Discriminator", c => c.String(nullable: false, maxLength: 128));
            AddColumn("dbo.ServiceStaffs", "VacationAddition", c => c.Int(nullable: false));
            AddColumn("dbo.ServiceStaffs", "SalaryAddition", c => c.Decimal(nullable: false, precision: 18, scale: 2));
            AddColumn("dbo.ServiceStaffs", "EmploymentDate", c => c.DateTime(nullable: false));
            AddColumn("dbo.ServiceStaffs", "Vacation", c => c.Int(nullable: false));
            AddColumn("dbo.ServiceStaffs", "Salary", c => c.Decimal(nullable: false, precision: 18, scale: 2));
            AddColumn("dbo.MedicalStaffs", "MedicalInstitution_Id", c => c.Int());
            AddColumn("dbo.MedicalStaffs", "Discriminator", c => c.String(nullable: false, maxLength: 128));
            AddColumn("dbo.MedicalStaffs", "VacationAddition", c => c.Int(nullable: false));
            AddColumn("dbo.MedicalStaffs", "SalaryAddition", c => c.Decimal(nullable: false, precision: 18, scale: 2));
            AddColumn("dbo.MedicalStaffs", "EmploymentDate", c => c.DateTime(nullable: false));
            AddColumn("dbo.MedicalStaffs", "Vacation", c => c.Int(nullable: false));
            AddColumn("dbo.MedicalStaffs", "Salary", c => c.Decimal(nullable: false, precision: 18, scale: 2));
            AddColumn("dbo.MedicalStaffs", "DiseaseGroupId", c => c.Int());
            AddColumn("dbo.MedicalStaffs", "ClinicId", c => c.Int());
            AddColumn("dbo.MedicalStaffs", "HospitalDepartmentId", c => c.Int());
            AddColumn("dbo.MedicalInstitutions", "Laboratory_Id1", c => c.Int());
            AddColumn("dbo.MedicalInstitutions", "Laboratory_Id", c => c.Int());
            AddColumn("dbo.Laboratories", "MedicalInstitution_Id", c => c.Int());
            DropForeignKey("dbo.ServiceStaffs", "SpecialtyId", "dbo.ServiceStaffSpecialties");
            DropForeignKey("dbo.ServiceStaffEmployments", "ServiceStaffId", "dbo.ServiceStaffs");
            DropForeignKey("dbo.MedicalStaffs", "ProfileId", "dbo.MedicalStaffProfiles");
            DropForeignKey("dbo.MedicalStaffProfiles", "DiseaseGroupId", "dbo.DiseaseGroups");
            DropForeignKey("dbo.MedicalStaffEmployments", "MedicalStaffId", "dbo.MedicalStaffs");
            DropForeignKey("dbo.MedicalStaffEmployments", "MedicalInstitutionId", "dbo.MedicalInstitutions");
            DropForeignKey("dbo.ServiceStaffEmployments", "MedicalInstitutionId", "dbo.MedicalInstitutions");
            DropForeignKey("dbo.MedicalInstitutionLaboratories", "Laboratory_Id", "dbo.Laboratories");
            DropForeignKey("dbo.MedicalInstitutionLaboratories", "MedicalInstitution_Id", "dbo.MedicalInstitutions");
            DropIndex("dbo.MedicalInstitutionLaboratories", new[] { "Laboratory_Id" });
            DropIndex("dbo.MedicalInstitutionLaboratories", new[] { "MedicalInstitution_Id" });
            DropIndex("dbo.ServiceStaffs", new[] { "SpecialtyId" });
            DropIndex("dbo.MedicalStaffProfiles", new[] { "DiseaseGroupId" });
            DropIndex("dbo.MedicalStaffEmployments", new[] { "MedicalInstitutionId" });
            DropIndex("dbo.MedicalStaffEmployments", new[] { "MedicalStaffId" });
            DropIndex("dbo.MedicalStaffs", new[] { "ProfileId" });
            DropIndex("dbo.ServiceStaffEmployments", new[] { "MedicalInstitutionId" });
            DropIndex("dbo.ServiceStaffEmployments", new[] { "ServiceStaffId" });
            AlterColumn("dbo.MedicalStaffs", "Degree", c => c.Int(nullable: false));
            DropColumn("dbo.ServiceStaffs", "SpecialtyId");
            DropColumn("dbo.MedicalStaffs", "ProfileId");
            DropTable("dbo.MedicalInstitutionLaboratories");
            DropTable("dbo.ServiceStaffSpecialties");
            DropTable("dbo.MedicalStaffProfiles");
            DropTable("dbo.MedicalStaffEmployments");
            DropTable("dbo.ServiceStaffEmployments");
            CreateIndex("dbo.ServiceStaffMedicalInstitutions", "MedicalInstitution_Id");
            CreateIndex("dbo.ServiceStaffMedicalInstitutions", "ServiceStaff_Id");
            CreateIndex("dbo.MedicalInstitutionMedicalStaffs", "MedicalStaff_Id");
            CreateIndex("dbo.MedicalInstitutionMedicalStaffs", "MedicalInstitution_Id");
            CreateIndex("dbo.MedicalStaffs", "MedicalInstitution_Id");
            CreateIndex("dbo.MedicalStaffs", "DiseaseGroupId");
            CreateIndex("dbo.MedicalStaffs", "ClinicId");
            CreateIndex("dbo.MedicalStaffs", "HospitalDepartmentId");
            CreateIndex("dbo.MedicalInstitutions", "Laboratory_Id1");
            CreateIndex("dbo.MedicalInstitutions", "Laboratory_Id");
            CreateIndex("dbo.Laboratories", "MedicalInstitution_Id");
            AddForeignKey("dbo.MedicalInstitutions", "Laboratory_Id1", "dbo.Laboratories", "Id");
            AddForeignKey("dbo.MedicalInstitutions", "Laboratory_Id", "dbo.Laboratories", "Id");
            AddForeignKey("dbo.MedicalStaffs", "HospitalDepartmentId", "dbo.HospitalDepartments", "Id");
            AddForeignKey("dbo.MedicalStaffs", "DiseaseGroupId", "dbo.DiseaseGroups", "Id");
            AddForeignKey("dbo.ServiceStaffMedicalInstitutions", "MedicalInstitution_Id", "dbo.MedicalInstitutions", "Id", cascadeDelete: true);
            AddForeignKey("dbo.ServiceStaffMedicalInstitutions", "ServiceStaff_Id", "dbo.ServiceStaffs", "Id", cascadeDelete: true);
            AddForeignKey("dbo.Laboratories", "MedicalInstitution_Id", "dbo.MedicalInstitutions", "Id");
            AddForeignKey("dbo.MedicalStaffs", "MedicalInstitution_Id", "dbo.MedicalInstitutions", "Id");
            AddForeignKey("dbo.MedicalInstitutionMedicalStaffs", "MedicalStaff_Id", "dbo.MedicalStaffs", "Id", cascadeDelete: true);
            AddForeignKey("dbo.MedicalInstitutionMedicalStaffs", "MedicalInstitution_Id", "dbo.MedicalInstitutions", "Id", cascadeDelete: true);
            AddForeignKey("dbo.MedicalStaffs", "ClinicId", "dbo.MedicalInstitutions", "Id");
        }
    }
}
