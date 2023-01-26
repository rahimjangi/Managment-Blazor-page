using App.DataAccess.Data;
using App.DataAccess.Repository.IRepository;
using App.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace AbbyWeb.Pages.Admin.FoodTypes
{
    [BindProperties]
    public class DeleteModel : PageModel
    {
        private readonly IUnitOfWork _db;
        public FoodType FoodType { get; set; }

        public DeleteModel(IUnitOfWork db)
        {
            _db = db;
        }

        public void OnGet(int id)
        {
            FoodType = _db.FoodType.GetFirstOrDefault(f=>f.Id==id);
        }

        public async Task<IActionResult> OnPost()
        {
            if (ModelState.IsValid)
            {
                _db.FoodType.Remove(FoodType);
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
