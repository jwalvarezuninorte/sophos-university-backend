using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace SophosUniversityBackend.Models
{
    public partial class Proffesor
    {
        public Proffesor()
        {
            Courses = new HashSet<Course>();
            ProffesorsCourses = new HashSet<ProffesorsCourse>();
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
        [Column("maximum_degree")]
        [StringLength(20)]
        [Unicode(false)]
        public string MaximumDegree { get; set; } = null!;
        [Column("years_of_experience")]
        public int YearsOfExperience { get; set; }

        [InverseProperty("Proffesor")]
        public virtual ICollection<Course> Courses { get; set; }
        [InverseProperty("Proffesor")]
        public virtual ICollection<ProffesorsCourse> ProffesorsCourses { get; set; }
    }
}
