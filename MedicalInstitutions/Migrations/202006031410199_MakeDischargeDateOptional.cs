namespace MedicalInstitutions.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class MakeDischargeDateOptional : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.ServiceStaffEmployments", "DischargeDate", c => c.DateTime());
            AlterColumn("dbo.MedicalStaffEmployments", "DischargeDate", c => c.DateTime());
        }
        
        public override void Down()
        {
            AlterColumn("dbo.MedicalStaffEmployments", "DischargeDate", c => c.DateTime(nullable: false));
            AlterColumn("dbo.ServiceStaffEmployments", "DischargeDate", c => c.DateTime(nullable: false));
        }
    }
}
