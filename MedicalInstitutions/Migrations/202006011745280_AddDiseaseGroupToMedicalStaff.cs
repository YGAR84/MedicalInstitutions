namespace MedicalInstitutions.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddDiseaseGroupToMedicalStaff : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.MedicalStaffs", "DiseaseGroupId", c => c.Int());
            CreateIndex("dbo.MedicalStaffs", "DiseaseGroupId");
            AddForeignKey("dbo.MedicalStaffs", "DiseaseGroupId", "dbo.DiseaseGroups", "Id");
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.MedicalStaffs", "DiseaseGroupId", "dbo.DiseaseGroups");
            DropIndex("dbo.MedicalStaffs", new[] { "DiseaseGroupId" });
            DropColumn("dbo.MedicalStaffs", "DiseaseGroupId");
        }
    }
}
