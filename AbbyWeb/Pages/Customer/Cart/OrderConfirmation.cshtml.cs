using App.DataAccess.Repository.IRepository;
using App.Models;
using App.Utility;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Stripe.Checkout;

namespace AbbyWeb.Pages.Customer.Cart
{
    [BindProperties]
    public class OrderConfirmationModel : PageModel
    {
        private readonly IUnitOfWork _db;
        public OrderHeader OrderHeader { get; set; }
        public int OrderId { get; set; }

        public OrderConfirmationModel(IUnitOfWork db)
        {
            _db = db;
            
        }

        public void OnGet(int id)
        {
            OrderHeader=_db.OrderHeader.GetFirstOrDefault(x => x.Id == id);
            if(OrderHeader.SessionId!=null)
            {
                var service = new SessionService();
                Session session = service.Get(OrderHeader.SessionId);
                if (session.PaymentStatus.ToLower() == "paid")
                {
                    OrderHeader.Status = SD.StatusSubmitted;
                    OrderHeader.PaymentIntentId = session.PaymentIntentId;
                    _db.Save();
                }
            }
            List < ShoppingCart >shoppingCartsList= 
                _db.ShoppingCart.GetAll(u=>u.ApplicationUserId==OrderHeader.UserId).ToList();
            _db.ShoppingCart.RemoveRange(shoppingCartsList);
            _db.Save();
            OrderId = id;
        }
    }
}
