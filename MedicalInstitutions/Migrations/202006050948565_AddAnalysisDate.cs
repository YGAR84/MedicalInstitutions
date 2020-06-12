namespace MedicalInstitutions.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddAnalysisDate : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Analyses", "AnalysisDate", c => c.DateTime(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.Analyses", "AnalysisDate");
        }
    }
}
