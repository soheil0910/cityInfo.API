using System.ComponentModel.DataAnnotations;

namespace CityInfo.API.Models
{
    public class PointOfInterestForUpdateDto
    {

        [Required(ErrorMessage = "نام را به درستی وارد کنید ")]
        [MaxLength(100)]
        public string Name { get; set; } = string.Empty;

        [Required(ErrorMessage = "توضیحات را به درستی وارد کنید ")]
        [MaxLength(500)]
        public string? Description { get; set; }
    }
}
