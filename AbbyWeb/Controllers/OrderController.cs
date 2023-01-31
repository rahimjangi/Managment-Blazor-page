using App.DataAccess.Repository.IRepository;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace AbbyWeb.Controllers;

[Route("api/[controller]")]
[ApiController]
public class OrderController : Controller
{
    private readonly IUnitOfWork _db;

    public OrderController(IUnitOfWork db)
    {
        _db = db;
    }


    [HttpGet]
    [Authorize]
    public IActionResult Index()
    {
        var OrderHeaderList = _db.OrderHeader.GetAll(includeProperties:"ApplicationUser");
        return Json(new { data= OrderHeaderList });
    }
}
