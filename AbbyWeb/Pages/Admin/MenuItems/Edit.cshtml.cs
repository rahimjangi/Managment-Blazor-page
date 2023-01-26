using App.DataAccess.Data;
using App.DataAccess.Repository.IRepository;
using App.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace AbbyWeb.Pages.Admin.MenuItems
{
    [BindProperties]
    public class EditModel : PageModel
    {
        private readonly IUnitOfWork _db;
        public MenuItem MenuItem { get; set; }

        public EditModel(IUnitOfWork db)
        {
            _db = db;
        }

        public async Task OnGet(int id)
        {
            MenuItem = _db.MenuItem.GetFirstOrDefault(f=>f.Id==id);
        }

        public async Task<IActionResult> OnPost()
        {
            if(ModelState.IsValid)
            {
                 _db.MenuItem.Update(MenuItem);
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
