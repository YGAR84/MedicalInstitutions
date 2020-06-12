namespace MedicalInstitutions.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class ChangeOperationNameToOperationName : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Operations", "OperationName", c => c.String(nullable: false, maxLength: 30));
            DropColumn("dbo.Operations", "Name");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Operations", "Name", c => c.String(nullable: false, maxLength: 30));
            DropColumn("dbo.Operations", "OperationName");
        }
    }
}
