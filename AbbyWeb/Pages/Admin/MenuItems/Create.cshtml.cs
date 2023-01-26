using App.DataAccess.Data;
using App.DataAccess.Repository.IRepository;
using App.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace AbbyWeb.Pages.Admin.MenuItems
{
    [BindProperties]
    public class CreateModel : PageModel
    {
        private readonly IUnitOfWork _db;
        public MenuItem MenuItem{ get; set; }
        public CreateModel(IUnitOfWork db)
        {
            _db = db;
        }

        public void OnGet()
        {
            
        }
        public async Task<IActionResult> OnPost()
        {
            if(ModelState.IsValid)
            {
                _db.MenuItem.Add(MenuItem);
                _db.Save();
                return RedirectToPage("Index");
            }
            else
            {
                return Page();
            }
        }
    }
}
