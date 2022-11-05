using System;
using System.Collections.Generic;
using System.Collections;
using System.Linq;
using System.Data.Entity.Infrastructure;

namespace EFCodeFirstConsoleApp2
{

    class Program
    {
        List<Student> stdList = new List<Student>();
        List<Grade> grdList = new List<Grade>();
        EFDataAccess efda = new EFDataAccess();
        static void Main(string[] args)
        {
            Program pg = new Program();
            try
            {
                // AddStudents();
                // Exercise 1
                // Get all students and grades
                    //pg.stdList = pg.efda.GetAllStudents();
                    //DisplayAllStudents(pg.stdList);

                // Exercise 2 - Get all students by  Grade 
                Console.WriteLine("Enter Grade:");
                int grdId = Convert.ToInt32(Console.ReadLine());
                pg.stdList = pg.efda.GetAllStudentsByGrade(grdId);
                DisplayAllStudents(pg.stdList);

                // Exercise 3 - Add an address using StudentId
                // Get the Student Id from the User, then get all the address details into a StudentAddress object
             

                // Exercise 4 - Update a student record, change the name
                // Get the Student Id from user, then get the new name
                // call UpdateStudent(int stdId, Student s1)

                // Exercise 5 - Deleting a record
                // Get the Student ID (Primary Key)
                // Call DeleteStudent(int stdId)

                // Exercises with Grades table
                // Add a record to the Grades table
                // - Update a record in the Grades table
                // - Deleted a record in the Grades table
                // Display all students belonging to a particular grade, using the Grade Object
                
                // Exercise with the StudentAddress table
                // Add an address for all the students, that are in the Students table, one at a time
                // Update and Delete a record from the StudentAddress table

                // Add a Student record, and the address, using only the Student object


                // Get students   with a Where clause
                //using (var ctx = new SchoolDBContext())
                //{   // Get all the student and grade records 
                //    pg.stdList = ctx.Students.Where(s => s.GradeId == 1).ToList();
                //    // pg.stdList = ctx.Students.Where(s => s.GradeId == 1 && s.StudentId == 2).ToList();
                //    DisplayAllStudents(pg.stdList);
                //}

                // Get students Orderby Grade
                Console.WriteLine("Height = 4 and Order by Grade");
                using (var ctx = new SchoolDBContext())
                {   // Get all the student and grade records 
                    pg.stdList = ctx.Students.Where(s => s.Height == 4).OrderBy(s => s.GradeId).ToList();
                    //pg.stdList = ctx.Students.Where(s => s.Height == 4).OrderBy(s => s.GradeId).ThenBy(s => s.StudentName).ToList();
                    DisplayAllStudents(pg.stdList);
                }

                Console.WriteLine("\nDisplaying only student names");
                DisplayOnlyStudentNames();

                // Updating records in a Connected architecture
                //Console.WriteLine($"Records updated: {UpdateStudent()}");

                // Adding/Updating records in a disconnected architecture
                int StdID = 0;
                //Console.WriteLine("Enter Student Id to update ");
                // StdID = Convert.ToInt32(Console.ReadLine());
                //var stud = new Student() { StudentName = "Vishwas ABCD", EnrollmentDate = DateTime.Parse("2000/01/01"), Height = 04, GradeId = 1 };
                //Console.WriteLine($"Records updated: {AddStudent(stud)}");

                //Console.WriteLine("Enter Student Id to update ");
                //StdID = Convert.ToInt32(Console.ReadLine());
                //var stud = new Student() { StudentId = 4, StudentName = "Vishwas ABCD", EnrollmentDate = DateTime.Parse("2000/01/01"), Height = 04, GradeId = 1 };
                //var sA = new StudentAddress() { StudentAddressId = 4, Address1 = "New Street ",
                //    Address2 = "Davis Road",City = "Bengaluru",Zipcode = 550067, State = "New State",
                //    Country = "India" };
                //stud.Address = sA;
                //Console.WriteLine($"Records updated: {UpdateStudent(stud)}");

                // Get students with a Where clause
                //using (var ctx = new SchoolDBContext())
                //{   // Get all the student and grade records 
                //    pg.stdList = ctx.Students.Where(s => s.StudentId == StdID).ToList();
                //    if (pg.stdList.Count > 0)
                //        DisplayAllStudents(pg.stdList);
                //    else
                //        Console.WriteLine("No Students found with Id: " + StdID);
                //}

                // Deleting record
                //Console.WriteLine("Enter Student Id to delete ");
                //StdID = Convert.ToInt32(Console.ReadLine());
                //Console.WriteLine($"Record Deleted: {DeleteStudent(StdID)}");

                // Add a Student Address
                //Console.WriteLine("Enter Student Id to add address ");
                //StdID = Convert.ToInt32(Console.ReadLine());
                //Console.WriteLine($"Student Address Record Added: {AddStudentAddress(StdID)}");
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }

            Console.ReadKey();
        }

        // Connected architecture
        static int UpdateStudent()
        {
            int retValue;

            using (var ctx = new SchoolDBContext())
            {
                // Updating records in a disconnected architecture
                Console.WriteLine("Enter Student Id to update ");
                int stdId = Convert.ToInt32(Console.ReadLine());
                Student std = ctx.Students.Where(s => s.StudentId == stdId).SingleOrDefault();
                Console.WriteLine("Student Details:");
                Console.WriteLine(std.StudentId + "\t" + std.StudentName + "\t" + std.DateOfBirth);
                Console.WriteLine("Edit Details: Student Name: ");
                std.StudentName = Console.ReadLine();
                Console.WriteLine("Date of Birth: ");
                std.DateOfBirth = Convert.ToDateTime(Console.ReadLine());
                Console.WriteLine("Height: ");
                std.Height =Convert.ToDecimal(Console.ReadLine());
                retValue = ctx.SaveChanges();
            }
            return retValue;
        }

        static int UpdateStudent(Student S1)
        {
            int retValue;

            using (var ctx = new SchoolDBContext())
            {
                Student std = ctx.Students.Where(s => s.StudentId == S1.StudentId).SingleOrDefault();
                // Update this object with the details sent
                std.StudentName = S1.StudentName;
                std.Height = S1.Height;
                std.DateOfBirth = S1.DateOfBirth;
                std.EnrollmentDate = S1.EnrollmentDate;
                std.GradeId = S1.GradeId;
                std.Address = S1.Address;
                
                ctx.Students.Attach(std);
                ctx.Entry(std).State = System.Data.Entity.EntityState.Modified;
                ctx.Entry(std.Address).State = System.Data.Entity.EntityState.Modified;
                
                retValue = ctx.SaveChanges();
            }
            return retValue;
        }

        static int AddStudent(Student S1)
        {
            int retValue;

            using (var ctx = new SchoolDBContext())
            {
                ctx.Students.Add(S1);
                retValue = ctx.SaveChanges();
            }
            return retValue;
        }

        static int AddStudentAddress(int sId)
        {
            int retValue=0;
            var sA = new StudentAddress()
            {
                StudentAddressId = sId,
                Address1 = "New Street ",
                Address2 = "Newer Street",
                City = "New City",
                Zipcode = 550067,
                State = "New State",
                Country = "India"
            };

            using (var ctx = new SchoolDBContext())
            {
                ctx.StudentAddresses.Add(sA);
                retValue = ctx.SaveChanges();
            }
            return retValue;
        }
        static int DeleteStudent(int stdId)
        {
            int retValue =0;

            try
            {
                using (var ctx = new SchoolDBContext())
                {
                    //Student std = ctx.Students.Where(s => s.StudentId == stdId).SingleOrDefault();
                    Student std = ctx.Students.Find(stdId);
                    if (std != null)
                    {
                        ctx.Entry(std).State = System.Data.Entity.EntityState.Deleted;
                        retValue = ctx.SaveChanges();
                    }
                }
            }
            catch (DbUpdateConcurrencyException ex)
            {
                throw new Exception("Record does not exist in the database");
            }
            catch (Exception ex)
            {
                throw;
            }

            // entity to be deleted
            //var student = new Student()
            //{
            //Setting the StudentId to an existing value tells Context to treat this as an existing object to perform update or delete
            //    StudentId = 1
            //};
            //using (var context = new SchoolContext())
            //{
            //    context.Remove<Student>(student);
            //    context.SaveChanges();
            //}
            // or the followings are also valid
            // context.RemoveRange(student);
            //context.Students.Remove(student);
            //context.Students.RemoveRange(student);
            //context.Attach<Student>(student).State = EntityState.Deleted;
            //context.Entry<Student>(student).State = EntityState.Deleted;
            return retValue;
        }

        static void DisplayAllStudents(List<Student> sList)
        {
            // Get the column names
            var propNames = typeof(Student).GetProperties();
            ArrayList ColNames = new ArrayList();
            foreach (var property in typeof(Student).GetProperties())
            {
                ColNames.Add(property.Name);
                Console.Write(property.Name + "\t");
            }
            Console.WriteLine();

            // Display all the Student records
            var sList = ctx.Students.ToList();
            foreach (Student s in sList)
            {
                Console.WriteLine(s.StudentId + "\t\t" + s.StudentName + "\t" +
                   s.DateOfBirth + "\t" + s.EnrollmentDate + "\t" + s.Height + "\t" + s.GradeId);
                Console.WriteLine("Student Address");
                if (s.Address != null && s.Address.StudentAddressId == s.StudentId)
                    Console.WriteLine(s.Address.Address1 + "\t" + s.Address.Address2 + "\t" + s.Address.City);
            }
            using (var ctx = new SchoolDBContext())
            { var stdAddress = ctx.StudentAddresses.ToList();
                foreach (StudentAddress sa in stdAddress)
                {
                    Console.WriteLine(sa.Address1 + "\t" + sa.Address2 + "\t" + sa.City);
                    Console.WriteLine("Student Details"); 
                    if (sa.Student != null && sa.Student.StudentId == sa.StudentAddressId) 
                        Console.WriteLine(sa.Student.StudentId + "\t\t" + sa.Student.StudentName + "\t" +
                       sa.Student.DateOfBirth + "\t" + sa.Student.EnrollmentDate + "\t" + sa.Student.Height + "\t" + sa.Student.GradeId);
                        
                }
            }
        }

        static void DisplayAllGradesAndStudents(List<Grade> gList)
        {
            // Display all Grades and Student records
            Console.WriteLine("\nGrades and Students");
            foreach (Grade g in gList)
            {
                Console.WriteLine(g.GradeId + "\t" + g.GradeName + "\t" + g.Section);
                if (g.Students.Count > 0)
                {
                    Console.WriteLine("Students in this grade");
                    foreach (var std in g.Students)
                    {
                        Console.WriteLine(std.StudentId + "\t\t" + std.StudentName);
                    }
                    Console.WriteLine();
                }
            }
        }

        static void DisplayOnlyStudentNames()
        {
            // Display only student names from annonymous object list
            Console.WriteLine("\nStudent Names using Annonymous Object");
            using (var ctx = new SchoolDBContext())
            {
                var sList = ctx.Students.Select(s => new { StudentId = s.StudentId, StudentName = s.StudentName }).ToList();
                foreach (var name in sList)
                    Console.WriteLine(name.StudentId + "\t" + name.StudentName);
            }

            // Using a Non-Entity Class to get only required fields from a table
            Console.WriteLine("\nStudent Names using a Non-Entity Object");
            using (var ctx = new SchoolDBContext())
            {
                List< StdIdAndName> sList = ctx.Students.Select(s => new StdIdAndName { StudentId = s.StudentId, StudentName = s.StudentName }).ToList();
                foreach (StdIdAndName sN in sList)
                    Console.WriteLine(sN.StudentId + "\t" + sN.StudentName);
            }

        }

        static void AddStudents()
        {
            var students = new List<Student>
            {
                new Student{StudentName="Ashwija AB",DateOfBirth = DateTime.Parse("2001/09/01"), EnrollmentDate=DateTime.Parse("2005/09/01"), Height = 04,GradeId=1},
                new Student{StudentName="Kavya Umesh Shetty",DateOfBirth = DateTime.Parse("2002/08/02"),EnrollmentDate=DateTime.Parse("2006/10/07") ,Height = 04, GradeId=2 },
                new Student{StudentName="Vaishnavi Shetty",DateOfBirth = DateTime.Parse("2003/05/04"), EnrollmentDate=DateTime.Parse("2007/08/05") ,Height = 04, GradeId=3},
                new Student{StudentName="Nilesh K H",DateOfBirth = DateTime.Parse("2004/04/07"),EnrollmentDate=DateTime.Parse("2008/06/02"), Height = 04, GradeId=4},
                 new Student{StudentName="Srinidhi I N ",DateOfBirth = DateTime.Parse("2001/09/01"), EnrollmentDate=DateTime.Parse("2005/09/01"), Height = 04,GradeId=1},
                new Student{StudentName="Prathiksha B S",DateOfBirth = DateTime.Parse("2002/08/02"),EnrollmentDate=DateTime.Parse("2006/10/07") ,Height = 04, GradeId=2 },
                new Student{StudentName="Prajnya kalmane ",DateOfBirth = DateTime.Parse("2003/05/04"), EnrollmentDate=DateTime.Parse("2007/08/05") ,Height = 04, GradeId=3},
                new Student{StudentName="Vaishnavi Setty",DateOfBirth = DateTime.Parse("2004/04/07"),EnrollmentDate=DateTime.Parse("2008/06/02"), Height = 04, GradeId=4},
                 new Student{StudentName="Vidyashree S",DateOfBirth = DateTime.Parse("2001/09/01"), EnrollmentDate=DateTime.Parse("2005/09/01"), Height = 04,GradeId=1},
                new Student{StudentName="Swathi S",DateOfBirth = DateTime.Parse("2002/08/02"),EnrollmentDate=DateTime.Parse("2006/10/07") ,Height = 04, GradeId=2 },
                new Student{StudentName="Krishana Kumar",DateOfBirth = DateTime.Parse("2003/05/04"), EnrollmentDate=DateTime.Parse("2007/08/05") ,Height = 04, GradeId=3}
            };
            using (var ctx = new SchoolDBContext())
            {
                students.ForEach(s => ctx.Students.Add(s));
                ctx.SaveChanges();
            }
        }
    }
}
