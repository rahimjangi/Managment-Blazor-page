using System.ComponentModel.DataAnnotations;

namespace App.Models
{
    public class Category
    {
        [Key]
        public int Id { get; set; }
        [Required(ErrorMessage = "Name can not be null")]
        public string Name { get; set; }
        [Required(ErrorMessage = "You need to provide a valid number")]
        [Display(Name = "Display Order")]
        [Range(1, 100, ErrorMessage = "Out of range")]
        public int DisplayOrder { get; set; }
    }
}
