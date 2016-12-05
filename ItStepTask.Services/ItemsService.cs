using ItStepTask.Data;
using ItStepTask.Entity;
using ItStepTask.Services.Contracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ItStepTask.Services
{
    public class ItemsService : BaseService<Item>, IItemsService
    {
        public ItemsService(ITaskData data)
            :base(data)
        {
        }

        public override IQueryable<Item> GetAll()
        {
            return base.GetAll().OrderByDescending(p => p.CreatedOn);
        }

        public override void Add(Item entity)
        {
            entity.CreatedOn = DateTime.Now;
            base.Add(entity, entity.Id);
        }

        public IQueryable<Item> GetByCategory(int categoryId)
        {
            return GetAll().Where(i => i.Category.Id == categoryId);
        }
    }

}
