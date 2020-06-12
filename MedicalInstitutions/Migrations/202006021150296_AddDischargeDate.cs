namespace MedicalInstitutions.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddDischargeDate : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.ServiceStaffEmployments", "DischargeDate", c => c.DateTime(nullable: false));
            AddColumn("dbo.MedicalStaffEmployments", "DischargeDate", c => c.DateTime(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.MedicalStaffEmployments", "DischargeDate");
            DropColumn("dbo.ServiceStaffEmployments", "DischargeDate");
        }
    }
}
