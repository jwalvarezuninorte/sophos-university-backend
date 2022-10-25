using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace SophosUniversityBackend.Models
{
    public partial class Course
    {
        public Course()
        {
            StudentsCourses = new HashSet<StudentsCourse>();
        }

        [Key]
        [Column("id")]
        public int Id { get; set; }
        [Column("title")]
        [StringLength(30)]
        [Unicode(false)]
        public string Title { get; set; } = null!;
        [Column("description")]
        [StringLength(1024)]
        [Unicode(false)]
        public string Description { get; set; } = null!;
        [Column("credits")]
        public int Credits { get; set; }
        [Column("limit")]
        public int Limit { get; set; }
        [Column("total_students")]
        public int TotalStudents { get; set; }
        [Column("proffesor_id")]
        public int ProffesorId { get; set; }

        [ForeignKey("ProffesorId")]
        [InverseProperty("Courses")]
        public virtual Proffesor Proffesor { get; set; } = null!;
        [InverseProperty("Course")]
        public virtual ProffesorsCourse ProffesorsCourse { get; set; } = null!;
        [InverseProperty("Course")]
        public virtual ICollection<StudentsCourse> StudentsCourses { get; set; }
    }
}
