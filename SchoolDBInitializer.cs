using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.Entity;

namespace EFCodeFirstConsoleApp2
{
    class SchoolDBInitializer : DropCreateDatabaseIfModelChanges<SchoolDBContext>
    {
        protected override void Seed(SchoolDBContext context)
        {
            var grades = new List<Grade>
            {
                new Grade { GradeName = "LKG", Section = "A"},
                new Grade { GradeName = "LKG", Section = "B"},
                new Grade { GradeName = "UKG", Section = "A"},
                new Grade { GradeName = "UKG", Section = "B"}
            };
            grades.ForEach(g => context.Grades.Add(g));
            context.SaveChanges();
            Console.WriteLine(context.Database.Connection.ConnectionString);

            var stud = new Student() { StudentName = "Bill", EnrollmentDate = DateTime.Parse("2000/01/01"), Height = 04, GradeId = 1 };
            context.Students.Add(stud);
            context.SaveChanges();
            var students = new List<Student>
            {
                new Student{StudentName="Bill Jason",DateOfBirth = DateTime.Parse("2001/09/01"), EnrollmentDate=DateTime.Parse("2005/09/01"), Height = 04,GradeId=1},
                new Student{StudentName="Mohan Reddy",DateOfBirth = DateTime.Parse("2002/08/02"),EnrollmentDate=DateTime.Parse("2006/10/07") ,Height = 04, GradeId=2 },
                new Student{StudentName="Tom Kumar",DateOfBirth = DateTime.Parse("2003/05/04"), EnrollmentDate=DateTime.Parse("2007/08/05") ,Height = 04, GradeId=3},
                new Student{StudentName="Ryan Rohan",DateOfBirth = DateTime.Parse("2004/04/07"),EnrollmentDate=DateTime.Parse("2008/06/02"), Height = 04, GradeId=4}
            };
            students.ForEach(s => context.Students.Add(s));
            context.SaveChanges();
        }

    }
}
