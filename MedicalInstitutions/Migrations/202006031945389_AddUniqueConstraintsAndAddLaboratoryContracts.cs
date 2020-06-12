namespace MedicalInstitutions.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddUniqueConstraintsAndAddLaboratoryContracts : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.MedicalInstitutionLaboratories", "MedicalInstitution_Id", "dbo.MedicalInstitutions");
            DropForeignKey("dbo.MedicalInstitutionLaboratories", "Laboratory_Id", "dbo.Laboratories");
            DropIndex("dbo.Cabinets", new[] { "ClinicId" });
            DropIndex("dbo.HospitalBuildings", new[] { "HospitalId" });
            DropIndex("dbo.MedicalInstitutionLaboratories", new[] { "MedicalInstitution_Id" });
            DropIndex("dbo.MedicalInstitutionLaboratories", new[] { "Laboratory_Id" });
            CreateTable(
                "dbo.LaboratoryContracts",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        LaboratoryId = c.Int(nullable: false),
                        MedicalInstitutionId = c.Int(nullable: false),
                        ContractPrice = c.Decimal(nullable: false, precision: 18, scale: 2),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Laboratories", t => t.LaboratoryId, cascadeDelete: true)
                .ForeignKey("dbo.MedicalInstitutions", t => t.MedicalInstitutionId, cascadeDelete: true)
                .Index(t => new { t.LaboratoryId, t.MedicalInstitutionId }, unique: true, name: "Ix_LaboratoryMedicalInstitution");
            
            AlterColumn("dbo.Laboratories", "Address", c => c.String(nullable: false, maxLength: 100));
            AlterColumn("dbo.DiseaseGroups", "Name", c => c.String(nullable: false, maxLength: 50));
            AlterColumn("dbo.Operations", "OperationName", c => c.String(nullable: false, maxLength: 30));
            CreateIndex("dbo.Laboratories", "Address", unique: true, name: "Ix_LaboratoryAddress");
            CreateIndex("dbo.ServiceStaffSpecialties", "SpecialtyName", unique: true, name: "Ix_SpecialtyName");
            CreateIndex("dbo.Diseases", "DiseaseName", unique: true, name: "Ix_DiseaseName");
            CreateIndex("dbo.DiseaseGroups", "Name", unique: true, name: "Ix_DiseaseGroupName");
            CreateIndex("dbo.MedicalStaffProfiles", "ProfileName", unique: true, name: "Ix_ProfileName");
            CreateIndex("dbo.Cabinets", new[] { "Number", "ClinicId" }, unique: true, name: "Ix_CabinetNumber_Clinic");
            CreateIndex("dbo.HospitalBuildings", new[] { "Number", "HospitalId" }, unique: true, name: "Ix_BuildingNumber_Hospital");
            DropTable("dbo.MedicalInstitutionLaboratories");
        }
        
        public override void Down()
        {
            CreateTable(
                "dbo.MedicalInstitutionLaboratories",
                c => new
                    {
                        MedicalInstitution_Id = c.Int(nullable: false),
                        Laboratory_Id = c.Int(nullable: false),
                    })
                .PrimaryKey(t => new { t.MedicalInstitution_Id, t.Laboratory_Id });
            
            DropForeignKey("dbo.LaboratoryContracts", "MedicalInstitutionId", "dbo.MedicalInstitutions");
            DropForeignKey("dbo.LaboratoryContracts", "LaboratoryId", "dbo.Laboratories");
            DropIndex("dbo.HospitalBuildings", "Ix_BuildingNumber_Hospital");
            DropIndex("dbo.Cabinets", "Ix_CabinetNumber_Clinic");
            DropIndex("dbo.MedicalStaffProfiles", "Ix_ProfileName");
            DropIndex("dbo.DiseaseGroups", "Ix_DiseaseGroupName");
            DropIndex("dbo.Diseases", "Ix_DiseaseName");
            DropIndex("dbo.ServiceStaffSpecialties", "Ix_SpecialtyName");
            DropIndex("dbo.LaboratoryContracts", "Ix_LaboratoryMedicalInstitution");
            DropIndex("dbo.Laboratories", "Ix_LaboratoryAddress");
            AlterColumn("dbo.Operations", "OperationName", c => c.String(maxLength: 30));
            AlterColumn("dbo.DiseaseGroups", "Name", c => c.String(nullable: false));
            AlterColumn("dbo.Laboratories", "Address", c => c.String(nullable: false));
            DropTable("dbo.LaboratoryContracts");
            CreateIndex("dbo.MedicalInstitutionLaboratories", "Laboratory_Id");
            CreateIndex("dbo.MedicalInstitutionLaboratories", "MedicalInstitution_Id");
            CreateIndex("dbo.HospitalBuildings", "HospitalId");
            CreateIndex("dbo.Cabinets", "ClinicId");
            AddForeignKey("dbo.MedicalInstitutionLaboratories", "Laboratory_Id", "dbo.Laboratories", "Id", cascadeDelete: true);
            AddForeignKey("dbo.MedicalInstitutionLaboratories", "MedicalInstitution_Id", "dbo.MedicalInstitutions", "Id", cascadeDelete: true);
        }
    }
}
