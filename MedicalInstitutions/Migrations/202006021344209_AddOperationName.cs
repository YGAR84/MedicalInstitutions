namespace MedicalInstitutions.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddOperationName : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Operations", "OperationName", c => c.String(nullable: false, maxLength: 30));
        }
        
        public override void Down()
        {
            DropColumn("dbo.Operations", "OperationName");
        }
    }
}
