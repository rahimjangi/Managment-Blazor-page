using App.Models;

namespace AbbyWeb.ViewModel;

public class OrderDetailsVM
{
    public OrderHeader OrderHeader { get; set; }
    public List<OrderDetails> OrderDetailsList { get; set; }
}
