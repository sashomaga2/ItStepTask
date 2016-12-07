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
    public class SuppliersService : BaseService<Supplier>, ISuppliersService
    {
        public SuppliersService(ITaskData data)
            :base(data)
        {
        }

        public override IQueryable<Supplier> GetAll()
        {
            return base.GetAll().Where(i => i.IsDeleted == false).OrderByDescending(p => p.CreatedOn);
        }

        public override void Add(Supplier entity)
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

        public override void Delete(Supplier item)
        {
            item.IsDeleted = true;
            base.Update(item);
        }
    }
}
