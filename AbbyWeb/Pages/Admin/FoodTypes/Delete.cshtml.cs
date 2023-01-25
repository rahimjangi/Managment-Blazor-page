using App.DataAccess.Data;
using App.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace AbbyWeb.Pages.Admin.FoodTypes
{
    [BindProperties]
    public class DeleteModel : PageModel
    {
        private readonly ApplicationDbContext _db;
        public FoodType FoodType { get; set; }

        public DeleteModel(ApplicationDbContext db)
        {
            _db = db;
        }

        public void OnGet(int id)
        {
            FoodType = _db.FoodTypes.Find(id);
        }

        public async Task<IActionResult> OnPost()
        {
            if (ModelState.IsValid)
            {
                 _db.FoodTypes.Remove(FoodType);
                _db.SaveChangesAsync();
                return RedirectToPage("Index");

            }
            else
            {
                return Page();
            }
        }
    }
}
