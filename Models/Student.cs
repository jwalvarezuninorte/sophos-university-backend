using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;
using SophosUniversityBackend.DTOs;

namespace SophosUniversityBackend.Models
{
    public partial class Student
    {
        public Student()
        {
            StudentsCourses = new HashSet<StudentsCourse>();
        }

        [Key]
        [Column("id")]
        public int Id { get; set; }
        [Column("first_name")]
        [StringLength(20)]
        [Unicode(false)]
        public string FirstName { get; set; } = null!;
        [Column("last_name")]
        [StringLength(20)]
        [Unicode(false)]
        public string LastName { get; set; } = null!;
        [Column("department_id")]
        public int DepartmentId { get; set; }
        [Column("total_courses")]
        public int TotalCourses { get; set; }
        [Column("total_credits")]
        public int TotalCredits { get; set; }

        [ForeignKey("DepartmentId")]
        [InverseProperty("Students")]
        public virtual Department Department { get; set; } = null!;
        [InverseProperty("Student")]
        public virtual ICollection<StudentsCourse> StudentsCourses { get; set; }

    }
}
