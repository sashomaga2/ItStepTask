using ItStepTask.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ItStepTask.Services.Contracts
{
    public interface IShoppingCartService : IService<ShoppingCart>
    {
        IQueryable<ShoppingCart> GetAll();
    }
}
