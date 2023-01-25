using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace App.Models;

public class FoodType
{
    [Key]
    public int Id { get; set; }
    [Required(ErrorMessage ="Name can not be null")]
    [Display(Name ="Name")]
    public string Name { get; set; }
}
