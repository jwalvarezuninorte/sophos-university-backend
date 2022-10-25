namespace SophosUniversityBackend.DTOs
{

    public class CourseDetailDto
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public int Credits { get; set; }
        public int Limit { get; set; }
        public int TotalStudents { get; set; }
        public string ProffesorName { get; set; }
        public List<StudentDto> Students { get; set; }
    }
}