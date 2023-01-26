using App.DataAccess.Data;
using App.DataAccess.Repository.IRepository;
using App.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace App.DataAccess.Repository;

public class FoodTypeRepository : Repository<FoodType>, IFoodTypeRepository
{
    private readonly ApplicationDbContext _db;

    public FoodTypeRepository(ApplicationDbContext db) : base(db)
    {
        _db = db;
    }

    public void Update(FoodType foodType)
    {
        var objFromDb = _db.FoodTypes.FirstOrDefault(f => f.Id == foodType.Id);
        objFromDb.Name=foodType.Name;
    }
}
