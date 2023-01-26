using App.DataAccess.Data;
using App.DataAccess.Repository.IRepository;
using App.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace AbbyWeb.Pages.Admin.FoodTypes
{
    [BindProperties]
    public class EditModel : PageModel
    {
        private readonly IUnitOfWork _db;
        public FoodType FoodType { get; set; }

        public EditModel(IUnitOfWork db)
        {
            _db = db;
        }

        public async Task OnGet(int id)
        {
            FoodType =  _db.FoodType.GetFirstOrDefault(f=>f.Id==id);
        }

        public async Task<IActionResult> OnPost()
        {
            if(ModelState.IsValid)
            {
                 _db.FoodType.Update(FoodType);
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
