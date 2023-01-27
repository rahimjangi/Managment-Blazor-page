using App.DataAccess.Repository.IRepository;
using Microsoft.AspNetCore.Mvc;

namespace AbbyWeb.Controllers;

[Route("api/[controller]")]
[ApiController]
public class MenuItemController : Controller
{
    private readonly IUnitOfWork _db;

    public MenuItemController(IUnitOfWork db)
    {
        _db = db;
    }

    [HttpGet]
    public IActionResult Get()
    {
        var menuItemList=_db.MenuItem.GetAll(includeProperties:"Category,FoodType");
        return Json(new {data=menuItemList});
    }
}
