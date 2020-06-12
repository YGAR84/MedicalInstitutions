namespace MedicalInstitutions.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class moveListsToVirtual : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.LaboratorySpecializations", "Laboratory_Id", "dbo.Laboratories");
            DropIndex("dbo.LaboratorySpecializations", new[] { "Laboratory_Id" });
            CreateTable(
                "dbo.ServiceStaffMedicalInstitutions",
                c => new
                    {
                        ServiceStaff_Id = c.Int(nullable: false),
                        MedicalInstitution_Id = c.Int(nullable: false),
                    })
                .PrimaryKey(t => new { t.ServiceStaff_Id, t.MedicalInstitution_Id })
                .ForeignKey("dbo.ServiceStaffs", t => t.ServiceStaff_Id, cascadeDelete: true)
                .ForeignKey("dbo.MedicalInstitutions", t => t.MedicalInstitution_Id, cascadeDelete: true)
                .Index(t => t.ServiceStaff_Id)
                .Index(t => t.MedicalInstitution_Id);
            
            CreateTable(
                "dbo.LaboratorySpecializationLaboratories",
                c => new
                    {
                        LaboratorySpecialization_Id = c.Int(nullable: false),
                        Laboratory_Id = c.Int(nullable: false),
                    })
                .PrimaryKey(t => new { t.LaboratorySpecialization_Id, t.Laboratory_Id })
                .ForeignKey("dbo.LaboratorySpecializations", t => t.LaboratorySpecialization_Id, cascadeDelete: true)
                .ForeignKey("dbo.Laboratories", t => t.Laboratory_Id, cascadeDelete: true)
                .Index(t => t.LaboratorySpecialization_Id)
                .Index(t => t.Laboratory_Id);
            
            AddColumn("dbo.Analyses", "LaboratoryId", c => c.Int(nullable: false));
            CreateIndex("dbo.Analyses", "LaboratoryId");
            AddForeignKey("dbo.Analyses", "LaboratoryId", "dbo.Laboratories", "Id", cascadeDelete: true);
            DropColumn("dbo.LaboratorySpecializations", "Laboratory_Id");
        }
        
        public override void Down()
        {
            AddColumn("dbo.LaboratorySpecializations", "Laboratory_Id", c => c.Int());
            DropForeignKey("dbo.Analyses", "LaboratoryId", "dbo.Laboratories");
            DropForeignKey("dbo.LaboratorySpecializationLaboratories", "Laboratory_Id", "dbo.Laboratories");
            DropForeignKey("dbo.LaboratorySpecializationLaboratories", "LaboratorySpecialization_Id", "dbo.LaboratorySpecializations");
            DropForeignKey("dbo.ServiceStaffMedicalInstitutions", "MedicalInstitution_Id", "dbo.MedicalInstitutions");
            DropForeignKey("dbo.ServiceStaffMedicalInstitutions", "ServiceStaff_Id", "dbo.ServiceStaffs");
            DropIndex("dbo.LaboratorySpecializationLaboratories", new[] { "Laboratory_Id" });
            DropIndex("dbo.LaboratorySpecializationLaboratories", new[] { "LaboratorySpecialization_Id" });
            DropIndex("dbo.ServiceStaffMedicalInstitutions", new[] { "MedicalInstitution_Id" });
            DropIndex("dbo.ServiceStaffMedicalInstitutions", new[] { "ServiceStaff_Id" });
            DropIndex("dbo.Analyses", new[] { "LaboratoryId" });
            DropColumn("dbo.Analyses", "LaboratoryId");
            DropTable("dbo.LaboratorySpecializationLaboratories");
            DropTable("dbo.ServiceStaffMedicalInstitutions");
            CreateIndex("dbo.LaboratorySpecializations", "Laboratory_Id");
            AddForeignKey("dbo.LaboratorySpecializations", "Laboratory_Id", "dbo.Laboratories", "Id");
        }
    }
}
