using ItStepTask.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ItStepTask.Services.Contracts
{
    public interface IItemsService : IService<Item>
    {
        IQueryable<Item> GetAll();
        IQueryable<Item> GetByCategory(int id);
    }
}
