namespace MedicalInstitutions.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class ChangeLaboratorySepcializationIndex : DbMigration
    {
        public override void Up()
        {
            RenameIndex(table: "dbo.LaboratorySpecializations", name: "Ix_LaboratoryName", newName: "Ix_LaboratorySpecializationName");
        }
        
        public override void Down()
        {
            RenameIndex(table: "dbo.LaboratorySpecializations", name: "Ix_LaboratorySpecializationName", newName: "Ix_LaboratoryName");
        }
    }
}
