using System.ComponentModel.DataAnnotations;

namespace DemoCrudApi.Dtos
{
    public class AddDemoDto
    {
        [Required]
        [StringLength(50)]
        public string Name { get; set; }

        [Required]
        public int Age { get; set; }
    }
}
