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

        public void OnGet(int? id)
        {
            if(id != null)
            {
                MenuItem=_db.MenuItem.GetFirstOrDefault(x => x.Id == id);
            }
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
                MenuItem.Image = @"\Images\MenuItems\" + fileNameNew + fileExtention;
                //MenuItem.Image = uploads + fileNameNew + fileExtention;
                _db.MenuItem.Add(MenuItem);
                _db.Save();
                return RedirectToPage("Index");
            }
            else
            {
                //Update
                var objFromDb=_db.MenuItem.GetFirstOrDefault(m=>m.Id==MenuItem.Id);
                if (files.Count() > 0)
                {
                    string fileNameNew = Guid.NewGuid().ToString();
                    var uploads = Path.Combine(webRootPath, @"Images\MenuItems");
                    var fileExtention = Path.GetExtension(files[0].FileName);
                    //delete existing image
                    var oldImagePath = Path.Combine(webRootPath, objFromDb.Image.TrimStart('\\'));
                    if(System.IO.File.Exists(oldImagePath))
                    {
                        System.IO.File.Delete(oldImagePath);
                    }
                    // Handle new upload

                    using (var filestreem = new FileStream(Path.Combine(uploads, fileNameNew + fileExtention), FileMode.Create))
                    {
                        files[0].CopyTo(filestreem);
                    }
                    MenuItem.Image = @"\Images\MenuItems\" + fileNameNew + fileExtention;
                }
                else
                {
                    MenuItem.Image=objFromDb.Image;
                }
                _db.MenuItem.Update(MenuItem);
                _db.Save();
                return RedirectToPage("Index");
            }
            return Page();
        }
    }
}
