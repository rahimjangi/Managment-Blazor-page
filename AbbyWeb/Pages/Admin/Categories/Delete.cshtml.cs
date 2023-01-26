using App.DataAccess.Data;
using App.DataAccess.Repository.IRepository;
using App.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;

namespace AbbyWeb.Pages.Admin.Categories;

[BindProperties]
public class DeleteModel : PageModel
{
    public Category Category { get; set; }
    private readonly IUnitOfWork _db;

    public DeleteModel(IUnitOfWork db)
    {
        _db = db;
    }

    public void OnGet(int id)
    {
        Category = _db.Category.GetFirstOrDefault(m => m.Id == id);
    }

    public async Task<IActionResult> OnPost()
    {
        if (ModelState.IsValid)
        {
            _db.Category.Remove(Category);
            _db.Save();
            TempData["success"] = "Category deleted successfully";
            return RedirectToPage("Index");
        }
        else
        {
            return Page();
        }
    }
}
