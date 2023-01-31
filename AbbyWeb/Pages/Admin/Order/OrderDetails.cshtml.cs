using AbbyWeb.ViewModel;
using App.DataAccess.Repository.IRepository;
using App.Models;
using App.Utility;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Stripe;

namespace AbbyWeb.Pages.Admin.Order;

[Authorize]
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
        OrderDetailsVM = new()
        {
            OrderHeader = _db.OrderHeader.GetFirstOrDefault(u => u.Id == id, includeProperties: "ApplicationUser"),
            OrderDetailsList = _db.OrderDetails.GetAll(filter: u => u.OrderId == id).ToList()
        };
    }

    public IActionResult OnPostOrderCompleted(int orderId)
    {
        _db.OrderHeader.UpdateStatus(orderId, SD.StatusCompleted);
        _db.Save();
        return RedirectToPage("OrderList");
    }
    public IActionResult OnPostOrderRefund(int orderId)
    {
        if(ModelState.IsValid)
        {
            var orderHeader = _db.OrderHeader.GetFirstOrDefault(u => u.Id == orderId);
            var options = new RefundCreateOptions
            {
                Reason = RefundReasons.RequestedByCustomer,
                PaymentIntent = orderHeader.PaymentIntentId

            };
            var service = new RefundService();
            Refund refund = service.Create(options);

            _db.OrderHeader.UpdateStatus(orderId, SD.StatusRefunded);
            _db.Save();
        }

        return RedirectToPage("OrderList");
    }
    public IActionResult OnPostOrderCancel(int orderId)
    {
        _db.OrderHeader.UpdateStatus(orderId, SD.StatusCanceled);
        _db.Save();
        return RedirectToPage("OrderList");
    }
}
