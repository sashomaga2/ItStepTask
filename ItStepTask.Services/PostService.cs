using ItStepTask.Services.Contracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ItStepTask.Entity;
using ItStepTask.Data;

namespace ItStepTask.Services
{
    public class PostService : BaseService<Post> ,IPostService
    {
        public PostService(ITaskData data)
            :base(data)
        {
        }

        public override IQueryable<Post> GetAll()
        {
            return base.GetAll().OrderByDescending(p => p.CreatedOn);
        }

        public override void Add(Post entity)
        {
            entity.CreatedOn = DateTime.Now;
            base.Add(entity, entity.Id);
        }
    }
}
