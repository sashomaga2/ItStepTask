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
    public class CategoryService : BaseService<Category>, ICategoryService
    {
        public CategoryService(ITaskData data)
            :base(data)
        {
        }

        public override IQueryable<Category> GetAll()
        {
            return base.GetAll().Where(i => i.IsDeleted == false).OrderByDescending(p => p.CreatedOn);
        }

        public override void Add(Category entity)
        {
            if (Find(entity.Id) != null)
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
            if (item != null)
            {
                Delete(item);
            }
        }

        public override void Delete(Category item)
        {
            item.IsDeleted = true;
            base.Update(item);
        }


    }
}
