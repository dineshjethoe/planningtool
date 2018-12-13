using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using Entities;
using Repositories.Interfaces;

namespace Repositories.Services
{
    /// <summary>
    /// This class provides data access to Assigned Tasks data
    /// </summary>
    public class AssignedTaskService : IService<AssignedTask>
    {
        private UnitOfWork<AssignedTask> unitOfWork = new UnitOfWork<AssignedTask>();

        public List<AssignedTask> Get(
           Expression<Func<AssignedTask, bool>> filter = null,
           Func<IQueryable<AssignedTask>, IOrderedQueryable<AssignedTask>> orderBy = null,
           string includeProperties = "")
        {
            var assignedTasks = unitOfWork.Repository.Get(filter, orderBy, includeProperties);
            return assignedTasks.ToList();
        }

        public AssignedTask GetById(int id)
        {
            AssignedTask assignedTask = unitOfWork.Repository.GetByID(id);
            return assignedTask;
        }

        public void Create(AssignedTask entity)
        {
            unitOfWork.Repository.Insert(entity);
            unitOfWork.Save();

        }
        public void Update(AssignedTask entity)
        {
            unitOfWork.Repository.Update(entity);
            unitOfWork.Save();
        }

        public void Delete(int id)
        {
            var assignedTask = GetById(id);
            assignedTask.IsDeleted = true;
            Update(assignedTask);

            //Do not delete record from database but instead do soft delete
            //unitOfWork.Repository.Delete(id);
            //unitOfWork.Save();
        }
    }
}
