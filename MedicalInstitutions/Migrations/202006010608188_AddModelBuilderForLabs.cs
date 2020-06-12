namespace MedicalInstitutions.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddModelBuilderForLabs : DbMigration
    {
        public override void Up()
        {
            RenameTable(name: "dbo.LaboratorySpecializationLaboratories", newName: "LaboratoryLaboratorySpecializations");
            DropPrimaryKey("dbo.LaboratoryLaboratorySpecializations");
            AddPrimaryKey("dbo.LaboratoryLaboratorySpecializations", new[] { "Laboratory_Id", "LaboratorySpecialization_Id" });
        }
        
        public override void Down()
        {
            DropPrimaryKey("dbo.LaboratoryLaboratorySpecializations");
            AddPrimaryKey("dbo.LaboratoryLaboratorySpecializations", new[] { "LaboratorySpecialization_Id", "Laboratory_Id" });
            RenameTable(name: "dbo.LaboratoryLaboratorySpecializations", newName: "LaboratorySpecializationLaboratories");
        }
    }
}
