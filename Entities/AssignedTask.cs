using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace Entities
{
    /// <summary>
    /// This class respresents an assigned task
    /// </summary>
    public class AssignedTask
    {

        [DatabaseGeneratedAttribute(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        public int TaskId { get; set; }

        public int EmployeeId { get; set; }

        public virtual Task Task { get; set; }

        public virtual Employee Employee { get; set; }

        public DateTime AssignmentDate { get; set; }

        public TimeSpan StartTime { get; set; }

        public TimeSpan EndTime { get; set; }

        public DateTime DateTimeCreated { get; set; }

        public bool IsDeleted { get; set; }

        [NotMapped]
        public byte[] RowVersion { get; set; }
    }
}
