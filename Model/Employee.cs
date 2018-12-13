using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model
{
    [Table("Details", Schema = "Employee")]
    public class Employee
    {
        public Employee()
        {
            this.Tasks = new HashSet<Task>();
        }

        public int Id { get; set; }

        [Required]
        public string FirstName { get; set; }

        [Required]
        public DateTime LastName { get; set; }

        [Timestamp]
        public byte[] RowVersion { get; set; }

        public virtual ICollection<Task> Tasks { get; set; }
    }
}
