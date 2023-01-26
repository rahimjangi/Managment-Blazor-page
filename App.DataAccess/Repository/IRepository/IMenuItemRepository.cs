using App.DataAccess.Repository.IRepository;
using App.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace App.DataAccess.Repository
{
    public interface IMenuItemRepository:IRepository<MenuItem>
    {
        void Update(MenuItem menuItem);
    }
}
