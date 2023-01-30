﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace App.DataAccess.Repository.IRepository;

public interface IUnitOfWork:IDisposable
{
    ICategoryRepository Category { get; }
    IFoodTypeRepository FoodType { get; }
    IMenuItemRepository MenuItem { get; }
    IShoppingCartRepository ShoppingCart { get; }
    IOrderDetailsRepository OrderDetails { get; }
    IOrderHeaderRepository OrderHeader { get; }
    IApplicationUserRepository ApplicationUser { get; }
    void Save();
}
