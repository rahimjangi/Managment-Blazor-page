using App.DataAccess.Data;
using App.DataAccess.Repository.IRepository;
using App.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace AbbyWeb.Pages.Admin.MenuItems
{
    [BindProperties]
    public class DeleteModel : PageModel
    {
        private readonly IUnitOfWork _db;
        public MenuItem MenuItem { get; set; }

        public DeleteModel(IUnitOfWork db)
        {
            _db = db;
        }

        public void OnGet(int id)
        {
            MenuItem = _db.MenuItem.GetFirstOrDefault(f=>f.Id==id);
        }

        public async Task<IActionResult> OnPost()
        {
            if (ModelState.IsValid)
            {
                _db.MenuItem.Remove(MenuItem);
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
