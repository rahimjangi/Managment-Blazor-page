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
    public class IndexModel : PageModel
    {
        private readonly IUnitOfWork _db;
        public IEnumerable<ShoppingCart> ShoppingCartList { get; set; }
        public double CartTotal { get; set; }

        public IndexModel(IUnitOfWork db)
        {
            _db = db;
            CartTotal=0;
            
        }

        public void OnGet()
        {
            var claimsIdentity = (ClaimsIdentity)User.Identity;
            var claim = claimsIdentity.FindFirst(ClaimTypes.NameIdentifier);
            var applicationUserId = claim.Value;
            if(applicationUserId != null)
            {
                ShoppingCartList=_db.ShoppingCart.GetAll(
                    filter:u=>u.ApplicationUserId==applicationUserId,
                    includeProperties: "MenuItem,MenuItem.Category,MenuItem.FoodType");
                foreach (var item in ShoppingCartList)
                {
                    CartTotal += item.Count * item.MenuItem.Price;
                }

            }
            
        }

        public IActionResult OnPostPlus(int cartId) {
            var objFromDb=_db.ShoppingCart.GetFirstOrDefault(u=>u.Id==cartId);
            _db.ShoppingCart.IncrementCount(objFromDb, 1);
            return RedirectToPage("Index");
        }
        public IActionResult OnPostMinus(int cartId) {
            var objFromDb = _db.ShoppingCart.GetFirstOrDefault(u => u.Id == cartId);
            if(objFromDb.Count==1)
            {
                var count = _db.ShoppingCart.GetAll(u => u.ApplicationUserId == objFromDb.ApplicationUserId).ToList().Count - 1;
                _db.ShoppingCart.DecrementCount(objFromDb, 1);
                HttpContext.Session.SetInt32(SD.SessionCart, count);
            }
            else
            {
                
                _db.ShoppingCart.Remove(objFromDb);
                _db.Save();
                
            }

            return RedirectToPage("Index");
        }
        public IActionResult OnPostRemove(int cartId) {
            var objFromDb = _db.ShoppingCart.GetFirstOrDefault(u => u.Id == cartId);
            var count = _db.ShoppingCart.GetAll(u=>u.ApplicationUserId==objFromDb.ApplicationUserId).ToList().Count-1;
            
            _db.ShoppingCart.Remove(objFromDb);
            _db.Save();
            HttpContext.Session.SetInt32(SD.SessionCart,count);
            return RedirectToPage("Index");
        }
    }
}
