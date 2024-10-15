namespace MyApi.DTOs
{
    public class ActivityEditionDto
    {
        public int Id { get; set; }
        public required string Name { get; set; }
        public required DateTime StartDate { get; set; }
        public required DateTime EndDate { get; set; }
        public string? Description { get; set; }
        public string? Location { get; set; }
        public string? ColorRef { get; set; }
        public int? NbRegistrationsMax { get; set; }
        public bool? IsArchived { get; set; }
    }
}