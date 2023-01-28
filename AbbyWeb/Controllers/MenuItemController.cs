using App.DataAccess.Repository.IRepository;
using Microsoft.AspNetCore.Mvc;

namespace AbbyWeb.Controllers;

[Route("api/[controller]")]
[ApiController]
public class MenuItemController : Controller
{
    private readonly IUnitOfWork _db;
    private readonly IWebHostEnvironment _webHostEnvironment;

    public MenuItemController(IUnitOfWork db, IWebHostEnvironment webHostEnvironment)
    {
        _db = db;
        _webHostEnvironment = webHostEnvironment;
    }

    [HttpGet]
    public IActionResult Get()
    {
        var menuItemList = _db.MenuItem.GetAll(includeProperties: "Category,FoodType");
        return Json(new { data = menuItemList });
    }

    [HttpDelete("{id}")]
    public IActionResult Delete(int id)
    {
        var objFromDb=_db.MenuItem.GetFirstOrDefault(m=>m.Id==id);
        var oldImagePath = Path.Combine(_webHostEnvironment.WebRootPath, objFromDb.Image.TrimStart('\\'));
        if(System.IO.File.Exists(oldImagePath))
        {
            System.IO.File.Delete(oldImagePath);
        }
        if (objFromDb != null)
        {
            _db.MenuItem.Remove(objFromDb);
            _db.Save();
        }
        return Json(new {success=true,message="Delete success!"});
    }
}
