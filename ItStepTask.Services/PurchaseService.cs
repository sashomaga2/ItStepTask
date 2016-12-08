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
    public class PurchaseService : BaseService<Purchase>, IPurchaseService
    {
        public PurchaseService(ITaskData data)
            :base(data)
        {
        }

        public override IQueryable<Purchase> GetAll()
        {
            return base.GetAll().OrderByDescending(p => p.CreatedOn);
        }

        public override void Add(Purchase entity)
        {
            entity.CreatedOn = DateTime.Now;
            base.Add(entity, entity.Id);
        }
    }
}
