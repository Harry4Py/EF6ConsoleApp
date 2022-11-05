using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data.Entity.Infrastructure;

namespace EFCodeFirstConsoleApp2
{
   public class StdIdAndName
    {
        public int StudentId { get; set; }
        public string StudentName { get; set; }
    }
    public class EFDataAccess
    {
        List<Student> stdList = new List<Student>();
        List<Grade> grdList = new List<Grade>();
        public List<Student> GetAllStudents()
        {
            using (var ctx = new SchoolDBContext())
            {   // Get all the student records 
                stdList = ctx.Students.ToList();
                //stdList = ctx.Students.OrderBy(s => s.StudentName).ToList();
            }
            return stdList;
        }

        public Student GetStudentById(int stdId)
        {
            Student std = new Student();
            // Get students with a Where clause
            using (var ctx = new SchoolDBContext())
            {
                std = ctx.Students.Where(s => s.StudentId == stdId).Single();
            }
            return std;
        }

        public List<Student> GetAllStudentsByGrade(int grdId)
        {
            // Get students Orderby Grade or by grade
            using (var ctx = new SchoolDBContext())
            {   // Get all the student and grade records 
                stdList = ctx.Students.Where(s => s.GradeId == grdId).ToList();
                //stdList = ctx.Students.Where(s => s.GradeId == grdId).OrderBy(s => s.StudentName).ToList();
                //stdList = ctx.Students.OrderBy(s => s.GradeId).ThenBy(s => s.StudentName).ToList();
            }
            return stdList;
        }

        public List<StdIdAndName> GetStudentIdAndName()
        {
            List<StdIdAndName> sList = new List<StdIdAndName>();
            try
            {
                using (var ctx = new SchoolDBContext())
                {
                    sList = ctx.Students.Select(s => new StdIdAndName
                    {
                        StudentId = s.StudentId,
                        StudentName = s.StudentName
                    }).ToList();
                }
            }
            catch (Exception ex)
            {   // You can make an entry in a log file here.
                throw ex;
            }
            return sList;
        }

        public int UpdateStudent(Student s1)
        {
            int retValue = 0;
            if (s1.StudentId > 0)
            {
                try
                {
                    using (var ctx = new SchoolDBContext())
                    {
                        // Updating records in a disconnected architecture
                        Student std = ctx.Students.Where(s => s.StudentId == s1.StudentId).Single();
                        if (std != null)
                        {
                            std.StudentName = s1.StudentName;
                            std.DateOfBirth = s1.DateOfBirth;
                            std.EnrollmentDate = s1.EnrollmentDate;
                            std.Height = s1.Height;
                            ctx.Entry(std).State = System.Data.Entity.EntityState.Modified;
                            if (s1.Address != null)
                            {
                                // Ensure that 1-1 relationship integrity is maintained
                                s1.Address.StudentAddressId = std.StudentId;
                                std.Address = s1.Address;
                                ctx.Entry(std.Address).State = System.Data.Entity.EntityState.Modified;
                            }
                            retValue = ctx.SaveChanges();
                        }
                    }
                }
                catch (DbUpdateConcurrencyException ex)
                {   // You can make an entry in a log file here.
                    throw ex;
                }
                catch (Exception ex)
                {   // You can make an entry in a log file here.
                    throw ex;
                }
            }
            return retValue;
        }

        public int UpdateStudentAddress(int sId, StudentAddress sAd)
        {
            int retValue = 0;
            if (sId > 0 && sAd != null)
            {
                try
                {
                    using (var ctx = new SchoolDBContext())
                    {
                        Student std = ctx.Students.Where(s => s.StudentId == sId).Single();
                        if (std != null)
                        {
                            // Ensure that 1-1 relationship integrity is maintained
                            sAd.StudentAddressId = sId;
                            std.Address = sAd;
                            ctx.Entry(std.Address).State = System.Data.Entity.EntityState.Modified;
                            retValue = ctx.SaveChanges();
                        }
                    }
                }
                catch (DbUpdateConcurrencyException ex)
                {
                    throw ex;
                }
                catch (Exception ex)
                {   // You can make an entry in a log file here.
                    throw ex;
                }
            }
            return retValue;
        }

        public int AddStudent(Student s1)
        {
            int retValue = 0;
            if (s1 != null)
            {
                try
                {
                    using (var ctx = new SchoolDBContext())
                    {
                        ctx.Students.Add(s1);
                        retValue = ctx.SaveChanges();
                    }
                }
                catch (Exception ex)
                {   // You can make an entry in a log file here.
                    throw ex;
                }
            }
            return retValue;
        }

        public int DeleteStudent(int sId)
        {
            int retValue = 0;
            if (sId > 0)
            {
                try
                {
                    using (var ctx = new SchoolDBContext())
                    {
                        //Student std = ctx.Students.Where(s => s.StudentId == stdId).Single();
                        Student std = ctx.Students.Find(sId);
                        if (std != null)
                        {
                            ctx.Entry(std).State = System.Data.Entity.EntityState.Deleted;
                            retValue = ctx.SaveChanges();
                        }
                    }
                }
                catch (Exception ex)
                {
                    throw ex;
                }
            }
            return retValue;
        }
    }
}
