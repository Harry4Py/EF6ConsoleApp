namespace EFCodeFirstConsoleApp2.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class EmpDBV2 : DbMigration
    {
        public override void Up()
        {
            CreateStoredProcedure(
                "dbo.Grade_Insert",
                p => new
                    {
                        GradeName = p.String(),
                        Section = p.String(),
                    },
                body:
                    @"INSERT [dbo].[Grades]([GradeName], [Section])
                      VALUES (@GradeName, @Section)
                      
                      DECLARE @GradeId int
                      SELECT @GradeId = [GradeId]
                      FROM [dbo].[Grades]
                      WHERE @@ROWCOUNT > 0 AND [GradeId] = scope_identity()
                      
                      SELECT t0.[GradeId]
                      FROM [dbo].[Grades] AS t0
                      WHERE @@ROWCOUNT > 0 AND t0.[GradeId] = @GradeId"
            );
            
            CreateStoredProcedure(
                "dbo.Grade_Update",
                p => new
                    {
                        GradeId = p.Int(),
                        GradeName = p.String(),
                        Section = p.String(),
                    },
                body:
                    @"UPDATE [dbo].[Grades]
                      SET [GradeName] = @GradeName, [Section] = @Section
                      WHERE ([GradeId] = @GradeId)"
            );
            
            CreateStoredProcedure(
                "dbo.Grade_Delete",
                p => new
                    {
                        GradeId = p.Int(),
                    },
                body:
                    @"DELETE [dbo].[Grades]
                      WHERE ([GradeId] = @GradeId)"
            );
            
            CreateStoredProcedure(
                "dbo.Student_Insert",
                p => new
                    {
                        StudentName = p.String(),
                        FirstName = p.String(),
                        LastName = p.String(),
                        DateOfBirth = p.DateTime(),
                        EnrollmentDate = p.DateTime(),
                        Photo = p.Binary(),
                        Height = p.Decimal(precision: 18, scale: 2),
                        Weight = p.Single(),
                        GradeId = p.Int(),
                    },
                body:
                    @"INSERT [dbo].[Students]([StudentName], [FirstName], [LastName], [DateOfBirth], [EnrollmentDate], [Photo], [Height], [Weight], [GradeId])
                      VALUES (@StudentName, @FirstName, @LastName, @DateOfBirth, @EnrollmentDate, @Photo, @Height, @Weight, @GradeId)
                      
                      DECLARE @StudentId int
                      SELECT @StudentId = [StudentId]
                      FROM [dbo].[Students]
                      WHERE @@ROWCOUNT > 0 AND [StudentId] = scope_identity()
                      
                      SELECT t0.[StudentId]
                      FROM [dbo].[Students] AS t0
                      WHERE @@ROWCOUNT > 0 AND t0.[StudentId] = @StudentId"
            );
            
            CreateStoredProcedure(
                "dbo.Student_Update",
                p => new
                    {
                        StudentId = p.Int(),
                        StudentName = p.String(),
                        FirstName = p.String(),
                        LastName = p.String(),
                        DateOfBirth = p.DateTime(),
                        EnrollmentDate = p.DateTime(),
                        Photo = p.Binary(),
                        Height = p.Decimal(precision: 18, scale: 2),
                        Weight = p.Single(),
                        GradeId = p.Int(),
                    },
                body:
                    @"UPDATE [dbo].[Students]
                      SET [StudentName] = @StudentName, [FirstName] = @FirstName, [LastName] = @LastName, [DateOfBirth] = @DateOfBirth, [EnrollmentDate] = @EnrollmentDate, [Photo] = @Photo, [Height] = @Height, [Weight] = @Weight, [GradeId] = @GradeId
                      WHERE ([StudentId] = @StudentId)"
            );
            
            CreateStoredProcedure(
                "dbo.Student_Delete",
                p => new
                    {
                        StudentId = p.Int(),
                    },
                body:
                    @"DELETE [dbo].[Students]
                      WHERE ([StudentId] = @StudentId)"
            );
            
            CreateStoredProcedure(
                "dbo.StudentAddress_Insert",
                p => new
                    {
                        StudentAddressId = p.Int(),
                        Address1 = p.String(),
                        Address2 = p.String(),
                        City = p.String(),
                        Zipcode = p.Int(),
                        State = p.String(),
                        Country = p.String(),
                    },
                body:
                    @"INSERT [dbo].[StudentAddresses]([StudentAddressId], [Address1], [Address2], [City], [Zipcode], [State], [Country])
                      VALUES (@StudentAddressId, @Address1, @Address2, @City, @Zipcode, @State, @Country)"
            );
            
            CreateStoredProcedure(
                "dbo.StudentAddress_Update",
                p => new
                    {
                        StudentAddressId = p.Int(),
                        Address1 = p.String(),
                        Address2 = p.String(),
                        City = p.String(),
                        Zipcode = p.Int(),
                        State = p.String(),
                        Country = p.String(),
                    },
                body:
                    @"UPDATE [dbo].[StudentAddresses]
                      SET [Address1] = @Address1, [Address2] = @Address2, [City] = @City, [Zipcode] = @Zipcode, [State] = @State, [Country] = @Country
                      WHERE ([StudentAddressId] = @StudentAddressId)"
            );
            
            CreateStoredProcedure(
                "dbo.StudentAddress_Delete",
                p => new
                    {
                        StudentAddressId = p.Int(),
                    },
                body:
                    @"DELETE [dbo].[StudentAddresses]
                      WHERE ([StudentAddressId] = @StudentAddressId)"
            );
            
        }
        
        public override void Down()
        {
            DropStoredProcedure("dbo.StudentAddress_Delete");
            DropStoredProcedure("dbo.StudentAddress_Update");
            DropStoredProcedure("dbo.StudentAddress_Insert");
            DropStoredProcedure("dbo.Student_Delete");
            DropStoredProcedure("dbo.Student_Update");
            DropStoredProcedure("dbo.Student_Insert");
            DropStoredProcedure("dbo.Grade_Delete");
            DropStoredProcedure("dbo.Grade_Update");
            DropStoredProcedure("dbo.Grade_Insert");
        }
    }
}
