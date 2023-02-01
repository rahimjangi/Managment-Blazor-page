using App.DataAccess.Repository.IRepository;
using App.Models;
using App.Utility;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.ComponentModel.DataAnnotations;
using System.Security.Claims;

namespace AbbyWeb.Pages.Customer.Home;


[Authorize]
public class DetailsModel : PageModel
{
    private readonly IUnitOfWork _db;

    [BindProperty]
    public ShoppingCart ShoppingCart { get; set; }
    public DetailsModel(IUnitOfWork db)
    {
        _db = db;
    }

    public void OnGet(int id)
    {
        var claimsIdentity = (ClaimsIdentity)User.Identity;
        var claim = claimsIdentity.FindFirst(ClaimTypes.NameIdentifier);
        var applicationUserId = claim.Value;
        ShoppingCart = new()
        {
            ApplicationUserId = applicationUserId,
            MenuItem = _db.MenuItem.GetFirstOrDefault(x => x.Id == id, includeProperties: "Category,FoodType"),
            MenuItemId= id
        };
    }

    public IActionResult OnPost()
    {
        if (ModelState.IsValid)
        {
            ShoppingCart shoppingCartFromDb=_db.ShoppingCart.GetFirstOrDefault(
                u=>u.ApplicationUserId== ShoppingCart.ApplicationUserId &&
                u.MenuItemId==ShoppingCart.MenuItemId);
            if (shoppingCartFromDb!=null)
            {
                _db.ShoppingCart.IncrementCount(shoppingCartFromDb, ShoppingCart.Count);
            }
            else
            {
                _db.ShoppingCart.Add(ShoppingCart);
                _db.Save();
                HttpContext.Session.SetInt32(SD.SessionCart,
                    _db.ShoppingCart.GetAll(u=>u.ApplicationUserId== ShoppingCart.ApplicationUserId).ToList().Count
                    );
            }

            return RedirectToPage("Index");
        }
        else
        {
            return Page();
        }

    }

}

