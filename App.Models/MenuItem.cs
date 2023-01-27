using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace App.Models;

public class MenuItem
{
    [Key]
    public int Id { get; set; }
    [Required]
    public string Name { get; set; }
    [Required]
    public string Description { get; set; }
    public string Image { get; set; }
    [Range(1,1000,ErrorMessage ="The price should be between 1$ and 1000$")]
    public double Price { get; set; }
    [Display(Name ="Food Type")]
    public int FoodTypeId { get; set; }
    [ForeignKey("FoodTypeId")]
    public virtual FoodType FoodType { get; set; }
    [Display(Name ="Category")]
    public int CategoryId { get; set; }
    [ForeignKey("CategoryId")]
    public virtual Category Category { get; set; }


}
