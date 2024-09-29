using System.ComponentModel.DataAnnotations;

namespace DemoCrudApi.Dtos
{
    public class DemoDto
    {

        [Key]
        public int Id { get; set; }

        [Required]
        [StringLength(50)]
        public string Name { get; set; }

        [Required]
        public int Age { get; set; }
    }
}
