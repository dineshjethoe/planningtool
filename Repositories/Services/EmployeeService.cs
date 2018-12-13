using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using Entities;
using Repositories.Interfaces;

namespace Repositories.Services
{
    /// <summary>
    /// This class provides data access to employees data
    /// </summary>
    public class EmployeeService : IService<Employee>
    {
        private UnitOfWork<Employee> unitOfWork = new UnitOfWork<Employee>();

        public List<Employee> Get(
           Expression<Func<Employee, bool>> filter = null,
           Func<IQueryable<Employee>, IOrderedQueryable<Employee>> orderBy = null,
           string includeProperties = "")
        {
            var employees = unitOfWork.Repository.Get(filter, orderBy, includeProperties);
            return employees.ToList();
        }

        public Employee GetById(int id)
        {
            Employee employee = unitOfWork.Repository.GetByID(id);
            return employee;
        }

        public void Create(Employee entity)
        {
            unitOfWork.Repository.Insert(entity);
            unitOfWork.Save();

        }
        public void Update(Employee entity)
        {
            unitOfWork.Repository.Update(entity);
            unitOfWork.Save();
        }

        public void Delete(int id)
        {
            var employee = GetById(id);
            employee.IsDeleted = true;
            Update(employee);

            //Do not delete record from database but instead do soft delete
            //unitOfWork.Repository.Delete(id);
            //unitOfWork.Save();
        }
    }
}
