﻿using ItStepTask.Data;
using ItStepTask.Data.Repositories;
using ItStepTask.Services.Contracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ItStepTask.Services.Contracts;

namespace ItStepTask.Services
{
    public class BaseService<T> : IService<T> where T:class
    {
        private IRepository<T> repository;

        public BaseService(ITaskData data)
        {
            this.Data = data;
            this.repository = data.GetRepository<T>();
        }

        protected ITaskData Data { get; private set; }


        public virtual IQueryable<T> GetAll()
        {
            return this.repository.All();
        }

        public virtual T Find(object id)
        {
            return this.repository.Find(id);
        }

        public virtual void Add(T entity, int id)
        {
            this.repository.Add(entity);
            this.repository.SaveChanges();
        }

        public virtual void Add(T entity)
        {
            
            repository.Add(entity);
            repository.SaveChanges();
        }

        public virtual void Update(T entity)
        {
            this.repository.Update(entity);
            this.repository.SaveChanges();
        }

        public virtual void Delete(object id)
        {
            this.repository.Delete(id);
            this.repository.SaveChanges();
        }

        public virtual void Delete(T entity)
        {
            this.repository.Delete(entity);
            this.repository.SaveChanges();
        }

        public void SaveChanges()
        {
            this.repository.SaveChanges();
        }
    }
}
