namespace MyApi.DTOs
{
    public class ActivityCreationDto
    {
        public required string Name { get; set; }
        public required string Address { get; set; }
        public required DateTime StartDate { get; set; }
        public required DateTime EndDate { get; set; }
        public string? Description { get; set; }
        public string? AdditionalInformation { get; set; }
        public string? Location { get; set; } 
        public bool Active { get; set; }
    }
}