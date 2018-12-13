using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace Entities
{
    /// <summary>
    /// This class respresents an task
    /// </summary>
    public class Task
    {
        public int Id { get; set; }

        public string TaskDescription { get; set; }

        public DateTime TaskDate { get; set; }

        public DateTime DateTimeCreated { get; set; }

        public bool IsDeleted { get; set; }

        [NotMapped]
        public byte[] RowVersion { get; set; }

        public ICollection<AssignedTask> AssignedTasks;
    }
}
