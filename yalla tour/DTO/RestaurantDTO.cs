using System.ComponentModel.DataAnnotations;

namespace Yalla_Tour.DTO
{
    public class RestaurantDTO
    {

        [Required]
        public string? Name { get; set; }
        [Required]
        public string? Address { get; set; }
        public string? Description { get; set; }
        public string? Type { get; set; }
        public string? ImagesUrl { get; set; }
        public string? Menu { get; set; }

    }
}
