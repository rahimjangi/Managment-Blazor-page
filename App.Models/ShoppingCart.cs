using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace App.Models
{
    public class ShoppingCart
    {
        [Key]
        public int Id { get; set; }
        public int MenuItemId { get; set; }
        [ForeignKey("MenuItemId")]
        [ValidateNever]
        public  MenuItem MenuItem { get; set; }
        [Range(1,10,ErrorMessage ="Should be between 1 and 10")]
        public int Count { get; set; }
        public string ApplicationUserId { get; set; }
        [ForeignKey("ApplicationUserId")]
        [NotMapped]
        [ValidateNever]
        public  ApplicationUser ApplicationUser { get; set; }

        public ShoppingCart()
        {
            Count = 1;
        }
    }
}
