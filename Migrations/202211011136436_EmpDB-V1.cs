namespace EFCodeFirstConsoleApp2.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class EmpDBV1 : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Students", "FirstName", c => c.String());
            AddColumn("dbo.Students", "LastName", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.Students", "LastName");
            DropColumn("dbo.Students", "FirstName");
        }
    }
}
