using App.DataAccess.Data;
using App.DataAccess.Repository.IRepository;
using App.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace App.DataAccess.Repository;

public class CategoryRepository : Repository<Category>, ICategoryRepository
{
    private readonly ApplicationDbContext _db;

    public CategoryRepository(ApplicationDbContext db):base(db)
    {
        _db = db;
    }

    public void Update(Category category)
    {
        var objFromDb=_db.Category.FirstOrDefault(c=> c.Id == category.Id);
        objFromDb.Name=category.Name;
        objFromDb.DisplayOrder= category.DisplayOrder;
        
    }
}
