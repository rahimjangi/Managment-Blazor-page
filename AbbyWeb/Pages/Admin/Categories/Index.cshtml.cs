using App.DataAccess.Data;
using App.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace AbbyWeb.Pages.Admin.Categories;

public class IndexModel : PageModel
{
    private readonly ApplicationDbContext _db;
    public IEnumerable<Category> CategoryList;

    public IndexModel(ApplicationDbContext db)
    {
        _db = db;
    }

    public void OnGet()
    {
        CategoryList = _db.Category.ToList();
    }
}
