namespace SophosUniversityBackend.DTOs
{
    public class UpdateProffesorDto
    {
        public int Id { get; set; }

        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string MaximumDegree { get; set; }
        public int YearsOfExperience { get; set; }
    }
}