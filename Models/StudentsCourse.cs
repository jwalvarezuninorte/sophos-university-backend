using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace SophosUniversityBackend.Models
{
    public partial class StudentsCourse
    {
        [Key]
        [Column("student_id")]
        public int StudentId { get; set; }
        [Key]
        [Column("course_id")]
        public int CourseId { get; set; }
        [Column("completed")]
        public bool Completed { get; set; }

        [ForeignKey("CourseId")]
        [InverseProperty("StudentsCourses")]
        public virtual Course Course { get; set; } = null!;
        [ForeignKey("StudentId")]
        [InverseProperty("StudentsCourses")]
        public virtual Student Student { get; set; } = null!;
    }
}
