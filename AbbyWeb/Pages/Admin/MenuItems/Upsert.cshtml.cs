using App.DataAccess.Data;
using App.DataAccess.Repository.IRepository;
using App.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace AbbyWeb.Pages.Admin.MenuItems
{
    [BindProperties]
    public class UpsertModel : PageModel
    {
        private readonly IWebHostEnvironment _WebHostEnvironment;
        private readonly IUnitOfWork _db;
        public MenuItem MenuItem{ get; set; }
        public IEnumerable<SelectListItem> CategoryList { get; set; }
        public IEnumerable<SelectListItem> FoodTypeList { get; set; }
        public UpsertModel(IUnitOfWork db, IWebHostEnvironment webHostEnvironment)
        {
            _db = db;
            MenuItem = new();
            _WebHostEnvironment = webHostEnvironment;
        }

        public void OnGet()
        {
            CategoryList = _db.Category.GetAll().Select(i=>
            new SelectListItem()
            {
                Text= i.Name,
                Value=i.Id.ToString()
            }
                );
            FoodTypeList=_db.FoodType.GetAll().Select(i=>new SelectListItem{
                Text=i.Name,
                Value=i.Id.ToString()
            });
        }
        public async Task<IActionResult> OnPost()
        {
            string webRootPath = _WebHostEnvironment.WebRootPath;
            var files = HttpContext.Request.Form.Files;
            if (MenuItem.Id == 0)
            {
                //Create
                string fileNameNew = Guid.NewGuid().ToString();
                var uploads = Path.Combine(webRootPath, @"Images\MenuItems");
                var fileExtention = Path.GetExtension(files[0].FileName);
                using(var filestreem= new FileStream(Path.Combine(uploads, fileNameNew+fileExtention),FileMode.Create))
                {
                    files[0].CopyTo(filestreem);
                }
                MenuItem.Image = @"\Images\MenuItems" + fileNameNew + fileExtention;
                _db.MenuItem.Add(MenuItem);
                _db.Save();
                return RedirectToPage("Index");
            }
            else
            {
                //Update
            }
            return Page();
        }
    }
}
