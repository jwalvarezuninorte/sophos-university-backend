using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace SophosUniversityBackend.Models
{
    public partial class Department
    {
        public Department()
        {
            Students = new HashSet<Student>();
        }

        [Key]
        [Column("id")]
        public int Id { get; set; }
        [Column("department_name")]
        [StringLength(100)]
        [Unicode(false)]
        public string? DepartmentName { get; set; }

        [InverseProperty("Department")]
        public virtual ICollection<Student> Students { get; set; }
    }
}
