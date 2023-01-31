using AbbyWeb.ViewModel;
using App.DataAccess.Repository.IRepository;
using App.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace AbbyWeb.Pages.Admin.Order;

[Authorize]
[BindProperties]
public class OrderDetailsModel : PageModel
{
    private readonly IUnitOfWork _db;
    public OrderDetailsVM OrderDetailsVM { get; set; }

    public OrderDetailsModel(IUnitOfWork db)
    {
        _db = db;
    }

    public void OnGet(int id)
    {
        //OrderDetails = _db.OrderDetails.GetFirstOrDefault(includeProperties: "OrderHeader,MenuItem");
        OrderDetailsVM = new()
        {
            OrderHeader = _db.OrderHeader.GetFirstOrDefault(u => u.Id == id, includeProperties: "ApplicationUser"),
            OrderDetailsList = _db.OrderDetails.GetAll(filter: u => u.OrderId == id).ToList()
        };
    }
}
