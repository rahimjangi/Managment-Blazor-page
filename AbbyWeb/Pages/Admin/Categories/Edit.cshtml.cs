using App.DataAccess.Data;
using App.DataAccess.Repository.IRepository;
using App.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace AbbyWeb.Pages.Admin.Categories;

[BindProperties]
public class EditModel : PageModel
{
    public Category Category { get; set; }
    private readonly IUnitOfWork _db;

    public EditModel(IUnitOfWork db)
    {
        _db = db;
    }

    public void OnGet(int id)
    {
        Category = _db.Category.GetFirstOrDefault(x => x.Id == id);
    }

    public async Task<IActionResult> OnPost()
    {
        if (ModelState.IsValid)
        {


            _db.Category.Update(Category);
             _db.Save();
            TempData["success"] = "Category edited successfully";
            return RedirectToPage("Index");
        }
        else
        {
            return Page();
        }
    }
}
