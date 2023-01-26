using App.DataAccess.Data;
using App.DataAccess.Repository.IRepository;
using App.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;

namespace AbbyWeb.Pages.Admin.MenuItems
{
    [BindProperties]
    public class IndexModel : PageModel
    {
        private readonly IUnitOfWork _db;
        public IEnumerable<MenuItem> MenuItems { get; set; }
        public IndexModel(IUnitOfWork db)
        {
            _db = db;
        }

        public  void OnGet()
        {
            MenuItems = _db.MenuItem.GetAll();

        }
    }
}
