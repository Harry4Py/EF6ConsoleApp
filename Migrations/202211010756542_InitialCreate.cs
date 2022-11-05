namespace EFCodeFirstConsoleApp2.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class InitialCreate : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Grades",
                c => new
                    {
                        GradeId = c.Int(nullable: false, identity: true),
                        GradeName = c.String(),
                        Section = c.String(),
                    })
                .PrimaryKey(t => t.GradeId);
            
            CreateTable(
                "dbo.Students",
                c => new
                    {
                        StudentId = c.Int(nullable: false, identity: true),
                        StudentName = c.String(),
                        DateOfBirth = c.DateTime(),
                        EnrollmentDate = c.DateTime(),
                        Photo = c.Binary(),
                        Height = c.Decimal(nullable: false, precision: 18, scale: 2),
                        Weight = c.Single(nullable: false),
                        GradeId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.StudentId)
                .ForeignKey("dbo.Grades", t => t.GradeId, cascadeDelete: true)
                .Index(t => t.GradeId);
            
            CreateTable(
                "dbo.StudentAddresses",
                c => new
                    {
                        StudentAddressId = c.Int(nullable: false),
                        Address1 = c.String(),
                        Address2 = c.String(),
                        City = c.String(),
                        Zipcode = c.Int(nullable: false),
                        State = c.String(),
                        Country = c.String(),
                    })
                .PrimaryKey(t => t.StudentAddressId)
                .ForeignKey("dbo.Students", t => t.StudentAddressId)
                .Index(t => t.StudentAddressId);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Students", "GradeId", "dbo.Grades");
            DropForeignKey("dbo.StudentAddresses", "StudentAddressId", "dbo.Students");
            DropIndex("dbo.StudentAddresses", new[] { "StudentAddressId" });
            DropIndex("dbo.Students", new[] { "GradeId" });
            DropTable("dbo.StudentAddresses");
            DropTable("dbo.Students");
            DropTable("dbo.Grades");
        }
    }
}
