namespace SophosUniversityBackend.DTOs
{
    public class ProffesorDto
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public String MaximumDegree { get; set; }
        public int YearsOfExperience { get; set; }

        public List<SimpleProffesorCourseDto>? Courses { get; set; }
    }
}