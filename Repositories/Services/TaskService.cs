using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using Entities;
using Repositories.Interfaces;

namespace Repositories.Services
{
    /// <summary>
    /// This class provides data access to tasks data
    /// </summary>
    public class TaskService : IService<Task>
    {
        private UnitOfWork<Task> unitOfWork = new UnitOfWork<Task>();

        public List<Task> Get(
            Expression<Func<Task, bool>> filter = null,
            Func<IQueryable<Task>, IOrderedQueryable<Task>> orderBy = null,
            string includeProperties = "")
        {
            var tasks = unitOfWork.Repository.Get(filter, orderBy, includeProperties);
            return tasks.ToList();
        }

        public Task GetById(int id)
        {
            Task employee = unitOfWork.Repository.GetByID(id);
            return employee;
        }

        public void Create(Task entity)
        {
            unitOfWork.Repository.Insert(entity);
            unitOfWork.Save();

        }
        public void Update(Task entity)
        {
            unitOfWork.Repository.Update(entity);
            unitOfWork.Save();
        }

        public void Delete(int id)
        {
            var task = GetById(id);
            task.IsDeleted = true;
            Update(task);

            //Do not delete record from database but instead do soft delete
            //unitOfWork.Repository.Delete(id);
            //unitOfWork.Save();
        }
    }
}
