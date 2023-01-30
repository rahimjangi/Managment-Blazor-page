using App.DataAccess.Repository.IRepository;
using App.Models;
using App.Utility;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Security.Claims;

namespace AbbyWeb.Pages.Customer.Cart
{
    [Authorize]
    [BindProperties]
    public class SummaryModel : PageModel
    {
        public IEnumerable<ShoppingCart> ShoppingCartList { get; set; }
        public OrderHeader OrderHeader { get; set; }
        private readonly IUnitOfWork _db;

        public SummaryModel(IUnitOfWork db)
        {
            _db = db;
            OrderHeader= new OrderHeader();
        }

        public void OnGet()
        {
            var claimsIdentity = (ClaimsIdentity)User.Identity;
            var claim=claimsIdentity.FindFirst(ClaimTypes.NameIdentifier);
            if (claim != null)
            {
                ShoppingCartList = _db.ShoppingCart.GetAll(u=>u.ApplicationUserId==claim.Value,
                    includeProperties:"MenuItem,MenuItem.FoodType,MenuItem.Category");
                foreach (var item in ShoppingCartList)
                {
                    OrderHeader.OrderTotal += (item.MenuItem.Price*item.Count);
                }
                ApplicationUser applicationUser=_db.ApplicationUser.GetFirstOrDefault(u=>u.Id==claim.Value);
                OrderHeader.PickUpName=applicationUser.FirstName+" "+applicationUser.LastName;
                OrderHeader.PhoneNumber=applicationUser.PhoneNumber;
            }
        }

        public IActionResult OnPost()
        {
            var claimIdentity = (ClaimsIdentity)User.Identity;
            var claim = claimIdentity.FindFirst(ClaimTypes.NameIdentifier);
            if(claim != null)
            {
                
                ShoppingCartList = _db.ShoppingCart.GetAll(u=>u.ApplicationUserId==claim.Value,includeProperties: "MenuItem,MenuItem.FoodType,MenuItem.Category");
                foreach (var item in ShoppingCartList)
                {
                    OrderHeader.OrderTotal += (item.MenuItem.Price * item.Count);
                }
                OrderHeader.Status = SD.StatusPending;
                OrderHeader.OrderDate = DateTime.Now;
                OrderHeader.UserId = claim.Value;
                OrderHeader.PickUpTime = Convert.ToDateTime(OrderHeader.PickUpDate.ToShortDateString()
                    + " " + OrderHeader.PickUpTime.ToShortTimeString());
                _db.OrderHeader.Add(OrderHeader);
                _db.Save();
                foreach (var item in ShoppingCartList)
                {
                    OrderDetails orderDetails = new()
                    {
                        MenuItemId = item.MenuItem.Id,
                        OrderId=OrderHeader.Id,
                        Name= item.MenuItem.Name,
                        Price = item.MenuItem.Price,
                        Count=item.Count
                    };
                    _db.OrderDetails.Add(orderDetails);
                }
                _db.ShoppingCart.RemoveRange(ShoppingCartList);
                _db.Save();
            }

            return RedirectToPage("Index");
        }
    }
}






























