using App.DataAccess.Data;
using App.DataAccess.Repository.IRepository;
using App.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;

namespace AbbyWeb.Pages.Admin.FoodTypes
{
    [BindProperties]
    public class IndexModel : PageModel
    {
        private readonly IUnitOfWork _db;
        public IEnumerable<FoodType> FoodTypeList { get; set; }
        public IndexModel(IUnitOfWork db)
        {
            _db = db;
        }

        public  void OnGet()
        {
            FoodTypeList = _db.FoodType.GetAll();

        }
    }
}
