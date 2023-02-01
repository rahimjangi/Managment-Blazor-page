using App.DataAccess.Repository.IRepository;
using App.Utility;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace AbbyWeb.ViewComponents;

public class ShoppingCartViewComponent:ViewComponent
{
    private readonly IUnitOfWork _db;

    public ShoppingCartViewComponent(IUnitOfWork db)
    {
        _db = db;
    }

    public async Task<IViewComponentResult> InvokeAsync()
    {
        var claimsIdentity=(ClaimsIdentity)User.Identity;
        var claim=claimsIdentity.FindFirst(ClaimTypes.NameIdentifier);
        int count = 0;
        if(claim!=null)
        {
            if (HttpContext.Session.GetInt32(SD.SessionCart) != null)
            {
                return View(HttpContext.Session.GetInt32(SD.SessionCart));
            }
            else
            {
                count=_db.ShoppingCart.GetAll(u=>u.ApplicationUserId==claim.Value).ToList().Count;
                HttpContext.Session.SetInt32(SD.SessionCart, count);
                return View(count);
            }
        }
        else
        {
            HttpContext.Session.Clear();
            return View(count);
        }
    }
}
