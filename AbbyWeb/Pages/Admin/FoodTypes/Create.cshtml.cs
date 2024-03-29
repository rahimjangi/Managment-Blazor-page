using App.DataAccess.Data;
using App.DataAccess.Repository.IRepository;
using App.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace AbbyWeb.Pages.Admin.FoodTypes
{
    [BindProperties]
    public class CreateModel : PageModel
    {
        private readonly IUnitOfWork _db;
        public FoodType FoodType { get; set; }
        public CreateModel(IUnitOfWork db)
        {
            _db = db;
        }

        public void OnGet()
        {
            
        }
        public async Task<IActionResult> OnPost()
        {
            if(ModelState.IsValid)
            {
                _db.FoodType.Add(FoodType);
                _db.Save();
                return RedirectToPage("Index");
            }
            else
            {
                return Page();
            }
        }
    }
}
