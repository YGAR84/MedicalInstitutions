namespace MedicalInstitutions.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class ChagneSomeEntities : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Wards", "HospitalDepartment_Id", c => c.Int());
            CreateIndex("dbo.Wards", "HospitalDepartment_Id");
            AddForeignKey("dbo.Wards", "HospitalDepartment_Id", "dbo.HospitalDepartments", "Id");
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Wards", "HospitalDepartment_Id", "dbo.HospitalDepartments");
            DropIndex("dbo.Wards", new[] { "HospitalDepartment_Id" });
            DropColumn("dbo.Wards", "HospitalDepartment_Id");
        }
    }
}
