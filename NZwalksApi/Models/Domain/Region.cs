namespace NZwalksApi.Models.Domain
{
    public class Region
    {
        public Guid Id { get; set; }
        public required string Name { get; set; }
        public required string Code { get; set; }

        public string? RegionImageUrl { get; set; }
   
    }
}
