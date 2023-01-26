using App.DataAccess.Data;
using App.DataAccess.Repository.IRepository;
using App.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace AbbyWeb.Pages.Admin.Categories;

public class IndexModel : PageModel
{
    private readonly IUnitOfWork _dbCategory;
    public IEnumerable<Category> CategoryList;

    public IndexModel(IUnitOfWork db)
    {
        _dbCategory = db;
    }

    public void OnGet()
    {
        CategoryList = _dbCategory.Category.GetAll();
    }
}
