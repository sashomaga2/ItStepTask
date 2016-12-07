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
            return base.GetAll().Where(i => i.IsDeleted == false).OrderByDescending(p => p.CreatedOn);
        }

        public override void Add(Item entity)
        {
            if(Find(entity.Id) != null)
            {
                base.Update(entity);
            }
            else
            {
                entity.CreatedOn = DateTime.Now;
                base.Add(entity);
            }
        }

        public override void Delete(object id)
        {
            var item = Find((int)id);
            if(item != null)
            {
                Delete(item);
            }
        }

        public override void Delete(Item item)
        {
            item.IsDeleted = true;
            base.Update(item);
        }

        public IQueryable<Item> GetByCategory(int? categoryId)
        {
            if (categoryId == null)
            {
                return GetAll();
            }

            return GetAll().Where(i => i.Category.Id == categoryId.Value);
        }
    }

}
