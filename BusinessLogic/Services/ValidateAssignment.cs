using BusinessLogic.Interfaces;
using Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;

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
            return assignedTasks.Exists(a => a.EmployeeId == newAssignment.EmployeeId && a.AssignmentDate.Date == newAssignment.AssignmentDate.Date);
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
            double totalAssignedHours = assignedTasks
                .Where(a => a.EmployeeId == newAssignment.EmployeeId && a.AssignmentDate.Date == newAssignment.AssignmentDate.Date)
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
