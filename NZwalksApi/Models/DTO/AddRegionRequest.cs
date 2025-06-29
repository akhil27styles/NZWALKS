using System.ComponentModel.DataAnnotations;

namespace NZwalksApi.Models.DTO
{
    public class AddRegionRequest
    {
        [Required]
        [MinLength(3,ErrorMessage = "Name must be at least 3 characters long.")]
        [MaxLength(3, ErrorMessage = "Name must not exceed 3 characters.")]
        public required string Code { get; set; }
        [Required]
        [MaxLength(100, ErrorMessage = "Name must not exceed 100 characters.")]
        public required string Name { get; set; }

        public string? RegionImageUrl { get; set; }
    }
}
