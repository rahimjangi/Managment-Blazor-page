using App.DataAccess.Data;
using App.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace App.DataAccess.Repository.IRepository
{
    public class MenuItemRepository : Repository<MenuItem>, IMenuItemRepository
    {
        private readonly ApplicationDbContext _db;
        public MenuItemRepository(ApplicationDbContext db) : base(db)
        {
            _db = db;
        }

        public void Update(MenuItem menuItem)
        {
            var objFromDb=_db.MenuItems.FirstOrDefault(m=> m.Id == menuItem.Id);
            objFromDb.Name=menuItem.Name;
            objFromDb.Description=menuItem.Description;
            objFromDb.Price=menuItem.Price;
            if(menuItem.Image!=null)
            {
                objFromDb.Image = menuItem.Image;
            }

            objFromDb.CategoryId=menuItem.CategoryId;
            objFromDb.FoodTypeId=menuItem.FoodTypeId;
            
        }
    }
}
