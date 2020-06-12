namespace MedicalInstitutions.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddLengthAttributeToMedicalInstitutionAdress : DbMigration
    {
        public override void Up()
        {
            DropIndex("dbo.MedicalInstitutions", "Ix_MedicalInstitutionName");
            AlterColumn("dbo.MedicalInstitutions", "MedicalInstitutionName", c => c.String(nullable: false, maxLength: 100));
            AlterColumn("dbo.MedicalInstitutions", "Address", c => c.String(nullable: false, maxLength: 100));
            CreateIndex("dbo.MedicalInstitutions", "MedicalInstitutionName", unique: true, name: "Ix_MedicalInstitutionName");
        }
        
        public override void Down()
        {
            DropIndex("dbo.MedicalInstitutions", "Ix_MedicalInstitutionName");
            AlterColumn("dbo.MedicalInstitutions", "Address", c => c.String(nullable: false));
            AlterColumn("dbo.MedicalInstitutions", "MedicalInstitutionName", c => c.String(nullable: false, maxLength: 50));
            CreateIndex("dbo.MedicalInstitutions", "MedicalInstitutionName", unique: true, name: "Ix_MedicalInstitutionName");
        }
    }
}
