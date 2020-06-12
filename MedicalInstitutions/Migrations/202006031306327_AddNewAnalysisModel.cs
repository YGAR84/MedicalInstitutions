namespace MedicalInstitutions.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddNewAnalysisModel : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Analyses",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        AnalysisName = c.String(nullable: false),
                        LaboratoryId = c.Int(nullable: false),
                        MedicalInstitutionVisitId = c.Int(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Laboratories", t => t.LaboratoryId, cascadeDelete: true)
                .ForeignKey("dbo.MedicalInstitutionVisits", t => t.MedicalInstitutionVisitId)
                .Index(t => t.LaboratoryId)
                .Index(t => t.MedicalInstitutionVisitId);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Analyses", "MedicalInstitutionVisitId", "dbo.MedicalInstitutionVisits");
            DropForeignKey("dbo.Analyses", "LaboratoryId", "dbo.Laboratories");
            DropIndex("dbo.Analyses", new[] { "MedicalInstitutionVisitId" });
            DropIndex("dbo.Analyses", new[] { "LaboratoryId" });
            DropTable("dbo.Analyses");
        }
    }
}
