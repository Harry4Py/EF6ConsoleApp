using System;
using System.Collections.Generic;
using System.Text;
using System.ComponentModel.DataAnnotations.Schema;

namespace EFCodeFirstConsoleApp2
{
    public class Student
    {
        public int StudentId { get; set; }
        public string StudentName { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public DateTime? DateOfBirth { get; set; }
        public DateTime? EnrollmentDate { get; set; }
        public byte[] Photo { get; set; }
        public decimal Height { get; set; }
        public float Weight { get; set; }
        public int GradeId { get; set; }
        public Grade grade { get; set; }
        public virtual StudentAddress Address { get; set; }
    }

    public class Grade
    {
        public int GradeId { get; set; }
        public string GradeName { get; set; }
        public string Section { get; set; }

        public ICollection<Student> Students { get; set; }
    }

    public class StudentAddress
    {
        [ForeignKey("Student")]
        public int StudentAddressId { get; set; }

        public string Address1 { get; set; }
        public string Address2 { get; set; }
        public string City { get; set; }
        public int Zipcode { get; set; }
        public string State { get; set; }
        public string Country { get; set; }

        public virtual Student Student { get; set; }
    }


}
