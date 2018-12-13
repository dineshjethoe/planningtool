using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace Entities
{
    /// <summary>
    /// This class respresents an employee
    /// </summary>
    public class Employee
    {
        public int Id { get; set; }

        public string FirstName { get; set; }

        public string LastName { get; set; }

        public DateTime DateTimeCreated { get; set; }

        public bool IsDeleted { get; set; }

        [NotMapped]
        public byte[] RowVersion { get; set; }

        public ICollection<AssignedTask> AssignedTasks;
    }
}
