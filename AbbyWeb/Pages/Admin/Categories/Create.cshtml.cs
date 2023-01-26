using App.DataAccess.Data;
using App.DataAccess.Repository.IRepository;
using App.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace AbbyWeb.Pages.Admin.Categories;

[BindProperties]
public class CreateModel : PageModel
{

    public Category Category { get; set; }
    //private readonly ApplicationDbContext _db;
    private readonly IUnitOfWork _db;

    public CreateModel(IUnitOfWork db)
    {
        _db = db;
    }

    public void OnGet()
    {
    }

    public IActionResult OnPost()

    {
        if (Category.Name == Category.DisplayOrder.ToString())
        {
            ModelState.AddModelError("Category.Name", "Name and order can not be the same!");
        }
        if (ModelState.IsValid)
        {
            _db.Category.Add(Category);
            _db.Save();
            TempData["success"] = "Category created successfully";
            return RedirectToPage("Index");
        }
        else
        {
            return Page();
        }
    }
}
