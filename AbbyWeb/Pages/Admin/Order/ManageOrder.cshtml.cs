using AbbyWeb.ViewModel;
using App.DataAccess.Repository.IRepository;
using App.Models;
using App.Utility;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace AbbyWeb.Pages.Admin.Order;

[BindProperties]
[Authorize(Roles =$"{SD.ManagerRole},{SD.KitchenRole}")]
public class ManageOrderModel : PageModel
{
    private readonly IUnitOfWork _db;
    public List<OrderDetailsVM> OrderDetailsVM { get; set; }

    public ManageOrderModel(IUnitOfWork db)
    {
        _db = db;
    }

    public void OnGet()
    {
        OrderDetailsVM = new();
        List<OrderHeader> orderHeaders = 
            _db.OrderHeader.GetAll(u=>u.Status==SD.StatusSubmitted|| u.Status == SD.StatusInProcess).ToList();
        foreach (var item in orderHeaders)
        {
            var _n = new OrderDetailsVM()
            {
                OrderHeader = item,
                OrderDetailsList = _db.OrderDetails.GetAll(filter: u => u.OrderId == item.Id).ToList()
            };
            OrderDetailsVM.Add(_n);

        }
    }
    public IActionResult OnPostOrderInProcess(int orderId)
    {
       _db.OrderHeader.UpdateStatus(orderId, SD.StatusInProcess);
        _db.Save();
        return RedirectToPage("ManageOrder");
    }
    public IActionResult OnPostOrderReady(int orderId)
    {
        _db.OrderHeader.UpdateStatus(orderId, SD.StatusReady);
        _db.Save();
        return RedirectToPage("ManageOrder");
    }
    public IActionResult OnPostOrderCancel(int orderId)
    {
        _db.OrderHeader.UpdateStatus(orderId, SD.StatusCompleted);
        _db.Save();
        return RedirectToPage("ManageOrder");
    }
}
