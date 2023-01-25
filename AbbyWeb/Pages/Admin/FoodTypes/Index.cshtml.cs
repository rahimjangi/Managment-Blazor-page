using App.DataAccess.Data;
using App.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;

namespace AbbyWeb.Pages.Admin.FoodTypes
{
    [BindProperties]
    public class IndexModel : PageModel
    {
        private readonly ApplicationDbContext _db;
        public IEnumerable<FoodType> FoodTypeList { get; set; }
        public IndexModel(ApplicationDbContext db)
        {
            _db = db;
        }

        public  void OnGet()
        {
            FoodTypeList =  _db.FoodTypes.ToList();

        }
    }
}
