namespace MedicalInstitutions.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Initial1 : DbMigration
    {
        public override void Up()
        {
            RenameTable(name: "dbo.Аnalysis", newName: "Analyses");
        }
        
        public override void Down()
        {
            RenameTable(name: "dbo.Analyses", newName: "Аnalysis");
        }
    }
}
