using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WebBanSach.Model
{
    public class Product
    {
        [Key]
        public int Id { get; set; }
        [Required]
        public string Title { get; set; }
        [Required]
        public string Description { get; set; }
        [Required]
        public decimal ISBN { get; set; }
        [Required]
        public string Author { get; set; }
        [Required]
        [Range(0, 1000000, ErrorMessage = "Price must be between 0 and 1000000")]
        public double Price50 { get; set; }
        [Required]
        [Range(0, 1000000, ErrorMessage = "Price must be between 0 and 1000000")]
        public double Price100 { get; set; }
        [ValidateNever]
        public string? ImageUrl { get; set; }
        public int CategoryId { get; set; }
        [ValidateNever]
        [ForeignKey("CategoryId")]
        public Category Category { get; set; }
        [Required]
        public int CoverTypeId { get; set; }   // <— đúng tên
        [ValidateNever]
        [ForeignKey("CoverTypeId")]
        public CoverType CoverType { get; set; }

    }
}
