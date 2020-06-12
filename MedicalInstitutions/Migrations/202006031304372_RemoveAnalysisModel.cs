namespace MedicalInstitutions.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class RemoveAnalysisModel : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.Analyses", "LaboratoryId", "dbo.Laboratories");
            DropForeignKey("dbo.Analyses", "PatientId", "dbo.Patients");
            DropIndex("dbo.Analyses", new[] { "LaboratoryId" });
            DropIndex("dbo.Analyses", new[] { "PatientId" });
            DropTable("dbo.Analyses");
        }
        
        public override void Down()
        {
            CreateTable(
                "dbo.Analyses",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        AnalysisName = c.String(nullable: false),
                        LaboratoryId = c.Int(nullable: false),
                        PatientId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateIndex("dbo.Analyses", "PatientId");
            CreateIndex("dbo.Analyses", "LaboratoryId");
            AddForeignKey("dbo.Analyses", "PatientId", "dbo.Patients", "Id", cascadeDelete: true);
            AddForeignKey("dbo.Analyses", "LaboratoryId", "dbo.Laboratories", "Id", cascadeDelete: true);
        }
    }
}
