using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ItStepTask.Data;
using ItStepTask.Entity;
using ItStepTask.Services.Contracts;

namespace ItStepTask.Services
{
    public class OrdersService : BaseService<Order>, IOrdersService
    {
        public OrdersService(ITaskData data)
            :base(data)
        {
        }

        public override IQueryable<Order> GetAll()
        {
            return base.GetAll().OrderByDescending(p => p.CreatedOn);
        }

        public override void Add(Order entity)
        {
            entity.CreatedOn = DateTime.Now;
            base.Add(entity, entity.Id);
        }
    }
}
