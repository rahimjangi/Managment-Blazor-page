using App.DataAccess.Repository.IRepository;
using App.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.ComponentModel.DataAnnotations;

namespace AbbyWeb.Pages.Customer.Home
{
    [BindProperties]
    public class DetailsModel : PageModel
    {
        private readonly IUnitOfWork _db;
        public MenuItem MenuItem { get; set; }
        [Range(1,10,ErrorMessage ="Accepted value betwen 1 and 10")]
        public int Count { get; set; }
        public DetailsModel(IUnitOfWork db)
        {
            _db = db;
        }

        public void OnGet(int id)
        {
            MenuItem=_db.MenuItem.GetFirstOrDefault(x => x.Id == id,includeProperties:"Category,FoodType");
        }
    }
}
