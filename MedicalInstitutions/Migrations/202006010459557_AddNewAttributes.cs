namespace MedicalInstitutions.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddNewAttributes : DbMigration
    {
        public override void Up()
        {
            DropIndex("dbo.HospitalDepartments", new[] { "DiseaseGroupId" });
            DropIndex("dbo.HospitalDepartments", new[] { "HospitalBuildingId" });
            DropIndex("dbo.Wards", new[] { "HospitalDepartmentId" });
            AddColumn("dbo.MedicalStaffs", "MedicalInstitution_Id", c => c.Int());
            AddColumn("dbo.HospitalBuildings", "Number", c => c.Int(nullable: false));
            AlterColumn("dbo.MedicalInstitutions", "MedicalInstitutionName", c => c.String(nullable: false, maxLength: 50));
            AlterColumn("dbo.LaboratorySpecializations", "OperationName", c => c.String(nullable: false, maxLength: 50));
            CreateIndex("dbo.MedicalInstitutions", "MedicalInstitutionName", unique: true, name: "Ix_MedicalInstitutionName");
            CreateIndex("dbo.MedicalStaffs", "MedicalInstitution_Id");
            CreateIndex("dbo.LaboratorySpecializations", "OperationName", unique: true, name: "Ix_LaboratoryName");
            CreateIndex("dbo.HospitalDepartments", new[] { "DiseaseGroupId", "HospitalBuildingId" }, unique: true, name: "Ix_DiseaseGroup_HospitalBuilding");
            CreateIndex("dbo.Wards", new[] { "Number", "HospitalDepartmentId" }, unique: true, name: "Ix_WardNumber_HospitalDepartment");
            AddForeignKey("dbo.MedicalStaffs", "MedicalInstitution_Id", "dbo.MedicalInstitutions", "Id");
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.MedicalStaffs", "MedicalInstitution_Id", "dbo.MedicalInstitutions");
            DropIndex("dbo.Wards", "Ix_WardNumber_HospitalDepartment");
            DropIndex("dbo.HospitalDepartments", "Ix_DiseaseGroup_HospitalBuilding");
            DropIndex("dbo.LaboratorySpecializations", "Ix_LaboratoryName");
            DropIndex("dbo.MedicalStaffs", new[] { "MedicalInstitution_Id" });
            DropIndex("dbo.MedicalInstitutions", "Ix_MedicalInstitutionName");
            AlterColumn("dbo.LaboratorySpecializations", "OperationName", c => c.String(nullable: false));
            AlterColumn("dbo.MedicalInstitutions", "MedicalInstitutionName", c => c.String(nullable: false));
            DropColumn("dbo.HospitalBuildings", "Number");
            DropColumn("dbo.MedicalStaffs", "MedicalInstitution_Id");
            CreateIndex("dbo.Wards", "HospitalDepartmentId");
            CreateIndex("dbo.HospitalDepartments", "HospitalBuildingId");
            CreateIndex("dbo.HospitalDepartments", "DiseaseGroupId");
        }
    }
}
