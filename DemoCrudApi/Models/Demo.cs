using System.ComponentModel.DataAnnotations;

namespace DemoCrudApi.Models
{
    public class Demo
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
