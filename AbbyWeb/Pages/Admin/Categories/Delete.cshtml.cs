using App.DataAccess.Data;
using App.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;

namespace AbbyWeb.Pages.Admin.Categories;

[BindProperties]
public class DeleteModel : PageModel
{
    public Category Category { get; set; }
    private readonly ApplicationDbContext _db;

    public DeleteModel(ApplicationDbContext db)
    {
        _db = db;
    }

    public void OnGet(int id)
    {
        Category = _db.Category.FirstOrDefault(m => m.Id == id);
    }

    public async Task<IActionResult> OnPost()
    {
        if (ModelState.IsValid)
        {
            _db.Category.Remove(Category);
            _db.SaveChangesAsync();
            TempData["success"] = "Category deleted successfully";
            return RedirectToPage("Index");
        }
        else
        {
            return Page();
        }
    }
}
