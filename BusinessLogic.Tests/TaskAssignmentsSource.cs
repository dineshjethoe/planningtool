using Entities;
using System;
using System.Collections;

namespace BusinessLogic.Tests
{
    /// <summary>
    /// This class provides a list assignments (data) to be used for testing the business logic functions
    /// </summary>
    public class TaskAssignmentsSource : IEnumerable
    {
        public IEnumerator GetEnumerator()
        {
            yield return new AssignedTask[]
            {
                new  AssignedTask()
                {
                   EmployeeId = 26,
                   AssignmentDate = new DateTime(2018,08,20),
                   StartTime = new TimeSpan(9, 00, 00),
                   EndTime = new TimeSpan(10, 00, 00),
                },
                new  AssignedTask()
                {
                   EmployeeId = 26,
                   AssignmentDate = new DateTime(2018,08,20),
                   StartTime = new TimeSpan(9, 00, 00),
                   EndTime = new TimeSpan(10, 00, 00),
                },
                new  AssignedTask()
                {
                   EmployeeId = 26,
                   AssignmentDate = new DateTime(2018,08,21),
                   StartTime = new TimeSpan(9, 00, 00),
                   EndTime = new TimeSpan(16, 00, 00),
                },
                new  AssignedTask()
                {
                   EmployeeId = 26,
                   AssignmentDate = new DateTime(2018,08,20),
                   StartTime = new TimeSpan(9, 00, 00),
                   EndTime = new TimeSpan(10, 00, 00),
                },
                new  AssignedTask()
                {
                   EmployeeId = 26,
                   AssignmentDate = new DateTime(2018,08,20),
                   StartTime = new TimeSpan(9, 00, 00),
                   EndTime = new TimeSpan(10, 00, 00),
                },
                new  AssignedTask()
                {
                   EmployeeId = 26,
                   AssignmentDate = new DateTime(2018,08,20),
                   StartTime = new TimeSpan(9, 00, 00),
                   EndTime = new TimeSpan(10, 00, 00),
                },
                new  AssignedTask()
                {
                   EmployeeId = 26,
                   AssignmentDate = new DateTime(2018,08,20),
                   StartTime = new TimeSpan(9, 00, 00),
                   EndTime = new TimeSpan(10, 00, 00),
                },
                new  AssignedTask()
                {
                   EmployeeId = 26,
                   AssignmentDate = new DateTime(2018,08,20),
                   StartTime = new TimeSpan(9, 00, 00),
                   EndTime = new TimeSpan(10, 00, 00),
                },
                new  AssignedTask()
                {
                   EmployeeId = 26,
                   AssignmentDate = new DateTime(2018,08,20),
                   StartTime = new TimeSpan(9, 00, 00),
                   EndTime = new TimeSpan(10, 00, 00),
                },
                new  AssignedTask()
                {
                   EmployeeId = 26,
                   AssignmentDate = new DateTime(2018,08,20),
                   StartTime = new TimeSpan(9, 00, 00),
                   EndTime = new TimeSpan(10, 00, 00),
                },
                new  AssignedTask()
                {
                   EmployeeId = 26,
                   AssignmentDate = new DateTime(2018,08,20),
                   StartTime = new TimeSpan(9, 00, 00),
                   EndTime = new TimeSpan(10, 00, 00),
                },
            };
        }
    }
}
