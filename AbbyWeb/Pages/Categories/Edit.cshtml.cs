using AbbyWeb.Data;
using AbbyWeb.Model;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace AbbyWeb.Pages.Categories
{
    [BindProperties]
    public class EditModel : PageModel
    {
        public Category Category { get; set; }
        private readonly ApplicationDbContext _db;

        public EditModel(ApplicationDbContext db)
        {
            _db = db;
        }

        public void OnGet(int id)
        {
            Category=_db.Category.Find(id);
        }

        public async Task<IActionResult> OnPost()
        {
            if(ModelState.IsValid)
            {
                
                
                _db.Category.Update(Category);
                await _db.SaveChangesAsync();
                TempData["success"] = "Category edited successfully";
                return RedirectToPage("Index");
            }
            else
            {
                return Page();
            }
        }
    }
}
