namespace NZwalksApi.Models.DTO
{
    public class AddRegionRequest
    {
        public required string Name { get; set; }
        public required string Code { get; set; }

        public string? RegionImageUrl { get; set; }
    }
}
