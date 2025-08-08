using System.ComponentModel.DataAnnotations;

namespace WebBanSach.Models
{
    public class Category
    {
        [Key]
        public int Id { get; set; }
        [Required]
        public string Name { get; set; }
        [Display(Name="Display Order")]
        public int DisplayOrder { get; set; }
        public DateTime CreatedAt { get; set; } = DateTime.Now;
      
    }
}
