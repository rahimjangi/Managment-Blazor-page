using AbbyWeb.Pages.Admin.Order;
using App.DataAccess.Repository.IRepository;
using App.Utility;
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
    public IActionResult Index(string? status=null)
    {
        var OrderHeaderList = _db.OrderHeader.GetAll(includeProperties:"ApplicationUser");
        if(status== "cancelled")
        {
            OrderHeaderList= OrderHeaderList.Where(u=>u.Status==SD.StatusCanceled|| u.Status == SD.StatusRejected).ToList();
        } else if (status == "completed")
        {
            OrderHeaderList = OrderHeaderList.Where(u => u.Status == SD.StatusCompleted).ToList();
        } else if (status == "inProcess")
        {
            OrderHeaderList = OrderHeaderList.Where(u => u.Status == SD.StatusInProcess).ToList();
        } else if(status == "submitted")
        {
                OrderHeaderList = OrderHeaderList.Where(u => u.Status == SD.StatusSubmitted|| u.Status == SD.StatusInProcess).ToList();
        }

        return Json(new { data= OrderHeaderList });
    }
}
