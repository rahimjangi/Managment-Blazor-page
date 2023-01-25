using App.DataAccess.Data;
using App.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace AbbyWeb.Pages.Admin.Categories;

[BindProperties]
public class CreateModel : PageModel
{

    public Category Category { get; set; }
    private readonly ApplicationDbContext _db;

    public CreateModel(ApplicationDbContext db)
    {
        _db = db;
    }

    public void OnGet()
    {
    }

    public async Task<IActionResult> OnPost()

    {
        if (Category.Name == Category.DisplayOrder.ToString())
        {
            ModelState.AddModelError("Category.Name", "Name and order can not be the same!");
        }
        if (ModelState.IsValid)
        {
            var savedCategory = await _db.Category.AddAsync(Category);
            await _db.SaveChangesAsync();
            TempData["success"] = "Category created successfully";
            return RedirectToPage("Index");
        }
        else
        {
            return Page();
        }
    }
}
