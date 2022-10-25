using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace SophosUniversityBackend.Models
{
    [Index("CourseId", Name = "UQ__Proffeso__8F1EF7AFC69E7257", IsUnique = true)]
    public partial class ProffesorsCourse
    {
        [Key]
        [Column("proffesor_id")]
        public int ProffesorId { get; set; }
        [Key]
        [Column("course_id")]
        public int CourseId { get; set; }

        [ForeignKey("CourseId")]
        [InverseProperty("ProffesorsCourse")]
        public virtual Course Course { get; set; } = null!;
        [ForeignKey("ProffesorId")]
        [InverseProperty("ProffesorsCourses")]
        public virtual Proffesor Proffesor { get; set; } = null!;
    }
}
