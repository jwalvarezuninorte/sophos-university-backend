namespace SophosUniversityBackend.DTOs
{

    public class SimpleCourseDto
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int Credits { get; set; }
        public int TotalStudents { get; set; }
        public int Limit { get; set; }
    }

    public class SimpleStudentCourseDto
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int Credits { get; set; }
        public int TotalStudents { get; set; }
        public bool Completed { get; set; }
    }

    public class SimpleProffesorCourseDto
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int Credits { get; set; }
    }
}