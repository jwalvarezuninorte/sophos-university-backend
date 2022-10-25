namespace SophosUniversityBackend.DTOs
{
    public class StudentDetailDto
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int DepartmentId { get; set; }
        public string DepartmentName { get; set; }
        public int TotalCredits { get; set; }

        public List<SimpleStudentCourseDto> Courses { get; set; }

    }
}