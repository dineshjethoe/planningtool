using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using BusinessLogic.Interfaces;
using Entities;

namespace BusinessLogic.Services
{
    /// <summary>
    /// This class implements the IValidateAssignment
    /// The methods are used to validate an assignment
    /// </summary>
    public class ValidateAssignment : IValidateAssignment
    {
        public bool AssigneeHasAlreadyAssignmentsOnDate(AssignedTask newAssignment, List<AssignedTask> assignedTasks)
        {
            var assignments = assignedTasks.ToList();

            var employeeAssignments = from item in assignments
                                      where item.AssignmentDate.Date >= newAssignment.AssignmentDate.Date
                                            && item.EmployeeId == newAssignment.EmployeeId
                                      select item;

            return employeeAssignments.Any();
        }

        public bool AssignmentExceedsMaxDaysPerWeek(int assignedDaysPerWeek, int maxDaysPerWeek)
        {
            return assignedDaysPerWeek > maxDaysPerWeek;
        }

        public bool ExceedsMaxHoursPerDay(AssignedTask newAssignment, double alreadyAssignedHours, double maxHoursPerDay)
        {
            DateTime startDate = DateTime.Now.Date + newAssignment.StartTime;
            DateTime endDate = DateTime.Now.Date + newAssignment.EndTime;

            var totalHoursOfNewAssigment = GetTotalHoursBetweenTwoTimes(startDate, endDate);
            var totalHoursAssigned = totalHoursOfNewAssigment + alreadyAssignedHours;

            return totalHoursAssigned > maxHoursPerDay;
        }

        public DateTime GetEndDateOfWeek(AssignedTask newAssignment)
        {
            var assignmentDate = newAssignment.AssignmentDate;
            var culture = Thread.CurrentThread.CurrentCulture;
            var diff = assignmentDate.DayOfWeek - culture.DateTimeFormat.FirstDayOfWeek;
            if (diff < 0)
                diff += 7;
            return assignmentDate.AddDays(-diff).Date.AddDays(6);
        }

        public DateTime GetStartDateOfWeek(AssignedTask newAssignment)
        {
            var assignmentDate = newAssignment.AssignmentDate;
            var culture = Thread.CurrentThread.CurrentCulture;
            var diff = assignmentDate.DayOfWeek - culture.DateTimeFormat.FirstDayOfWeek;
            if (diff < 0)
                diff += 7;
            return assignmentDate.AddDays(-diff).Date;
        }

        public double GetTotalAssignedHoursOfEmployeeOnDate(AssignedTask newAssignment, List<AssignedTask> assignedTasks)
        {
            var assignments = assignedTasks.ToList();

            var employeeAssignments = from item in assignments
                                      where item.AssignmentDate.Date >= newAssignment.AssignmentDate.Date
                                            && item.EmployeeId == newAssignment.EmployeeId
                                      select item;

            //this can cause error because of date conversion
            //double totalAssignedHours = assignedTasks
            //    .Where(a => a.EmployeeId == newAssignment.EmployeeId 
            //        && Convert.ToDateTime(a.AssignmentDate.Date) == Convert.ToDateTime(newAssignment.AssignmentDate.Date))
            //    .Sum(a => (new TimeSpan(a.EndTime.Ticks - a.StartTime.Ticks)).TotalHours);


            //WorkAround:
            double totalAssignedHours = employeeAssignments
                                        .Sum(a => (new TimeSpan(a.EndTime.Ticks - a.StartTime.Ticks)).TotalHours);


            return totalAssignedHours;
        }

        public double GetTotalHoursBetweenTwoTimes(DateTime beginTime, DateTime endTime)
        {
            TimeSpan duration = new TimeSpan(endTime.TimeOfDay.Ticks - beginTime.TimeOfDay.Ticks);

            return duration.TotalHours;
        }

        public bool IsTheStartTimeOfTheNewAssignmentGreaterThanTheEndTimeOfTheLastAssignment(AssignedTask newAssignment, List<AssignedTask> assignedTasks)
        {
            var datesList = new List<DateTime>();
            var max = DateTime.MinValue;

            foreach (var assignedTask in assignedTasks)
            {
                datesList.Add(DateTime.Now.Date + assignedTask.EndTime);
            }

            foreach (DateTime date in datesList)
            {
                if (DateTime.Compare(date, max) == 1)
                    max = date;
            }

            var EndTimeOfTheLastAssignment = max;
            var StartTimeOfTheNewAssignment = DateTime.Now.Date + newAssignment.StartTime;

            return StartTimeOfTheNewAssignment > EndTimeOfTheLastAssignment;
        }
    }
}
