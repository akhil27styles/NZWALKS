namespace NZwalksApi.Models.DTO
{
    public class UpdateRegionRequest
    {
        public required string Name { get; set; }

        public required string Code { get; set; }
        public string? RegionImageUrl { get; set; }
    }
}
