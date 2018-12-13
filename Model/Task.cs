using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Model
{
    [Table("Details", Schema = "Task")]
    public class Task
    {
        public Task()
        {
            this.Employees = new HashSet<Employee>();
        }

        public int Id { get; set; }

        [Required]
        public string TaskDescription { get; set; }

        [Required]
        public DateTime TaskDate { get; set; }

        [Required]
        public DateTime TaskTime { get; set; }

        [Timestamp]
        public byte[] RowVersion { get; set; }

        public virtual ICollection<Employee> Employees { get; set; }
    }
}
