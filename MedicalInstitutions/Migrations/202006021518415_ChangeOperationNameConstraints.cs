namespace MedicalInstitutions.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class ChangeOperationNameConstraints : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.Operations", "OperationName", c => c.String());
        }
        
        public override void Down()
        {
            AlterColumn("dbo.Operations", "OperationName", c => c.String(nullable: false, maxLength: 30));
        }
    }
}
