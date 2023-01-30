using App.DataAccess.Repository.IRepository;
using App.Models;
using App.Utility;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Stripe.Checkout;
using System.Security.Claims;
using System.Xml.Linq;

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
                //_db.ShoppingCart.RemoveRange(ShoppingCartList);
                _db.Save();
                var quantity = ShoppingCartList.ToList().Count;
                var domain = "https://localhost:44383";
                var unitAmmount = OrderHeader.OrderTotal;
                var currency = "usd";
                var name = "WebApp";
                //return StripePayment(domain,OrderHeader.OrderTotal,"usd","WebApp",quantity,OrderHeader.Id);


                var options = new SessionCreateOptions
                {
                    LineItems = new List<SessionLineItemOptions>(),
                    PaymentMethodTypes= new List<string>
                    {
                        "card",
                    },
                    
                        Mode = "payment",
                        SuccessUrl = domain + $"/Customer/Cart/OrderConfirmation?id={OrderHeader.Id}",
                        CancelUrl = domain + "/Customer/Cart/Index",
                };
                //Add line items
                foreach (var item in ShoppingCartList)
                {
                    var sessionLineItem = new SessionLineItemOptions
                    {
                        // Provide the exact Price ID (for example, pr_1234) of the product you want to sell
                        PriceData = new SessionLineItemPriceDataOptions
                        {
                            UnitAmount = (long)(item.MenuItem.Price * 100),
                            Currency = currency,
                            ProductData = new SessionLineItemPriceDataProductDataOptions
                            {
                                Name = item.MenuItem.Name
                            }
                        },
                        Quantity = item.Count
                    };
                    options.LineItems.Add(sessionLineItem);
                }
                

                var service = new SessionService();
                Session session = service.Create(options);

                Response.Headers.Add("Location", session.Url);

                OrderHeader.SessionId = session.Id;
                OrderHeader.PaymentIntentId = session.PaymentIntentId;
                _db.Save();
                return new StatusCodeResult(303);


            }

            return Page();
        }

        private StatusCodeResult StripePayment(string domain,double unitAmmount,string currency,string name,int quantity,int orderId)
        {
            
            var options = new SessionCreateOptions
            {
                LineItems = new List<SessionLineItemOptions>
                {
                  new SessionLineItemOptions
                  {
                    // Provide the exact Price ID (for example, pr_1234) of the product you want to sell
                    PriceData= new SessionLineItemPriceDataOptions
                    {
                        UnitAmount=(long)(unitAmmount*100),
                        Currency=currency,
                        ProductData=new SessionLineItemPriceDataProductDataOptions
                        {
                            Name=name,
                            Description="Total Distinct Item: "+quantity
                        }
                    },
                    Quantity=1
                  },
                },
                Mode = "payment",
                SuccessUrl = domain + "/Customer/Cart/OrderConfirmation?id={orderId}",
                CancelUrl = domain + "/Customer/Cart/Index",
            };
            var service = new SessionService();
            Session session = service.Create(options);

            Response.Headers.Add("Location", session.Url);
            return new StatusCodeResult(303);
        }
    }
}






























