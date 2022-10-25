namespace SophosUniversityBackend.DTOs
{

    public class UpdateCourseDto
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public int Credits { get; set; }
        public int Limit { get; set; }
        public int ProffesorId { get; set; }
    }
}