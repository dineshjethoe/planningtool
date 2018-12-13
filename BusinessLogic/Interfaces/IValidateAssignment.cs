using System;
using System.Collections.Generic;
using Entities;

namespace BusinessLogic.Interfaces
{
    /// <summary>
    /// This interface provides the abstract functions to validate an task assignment
    /// </summary>
    public interface IValidateAssignment
    {
        bool AssigneeHasAlreadyAssignmentsOnDate(AssignedTask newAssignment, List<AssignedTask> assignedTasks);

        double GetTotalHoursBetweenTwoTimes(DateTime beginTime, DateTime endTime);

        double GetTotalAssignedHoursOfEmployeeOnDate(AssignedTask newAssignment, List<AssignedTask> assignedTasks);

        bool IsTheStartTimeOfTheNewAssignmentGreaterThanTheEndTimeOfTheLastAssignment(AssignedTask newAssignment, List<AssignedTask> assignedTasks);

        bool ExceedsMaxHoursPerDay(AssignedTask newAssignment, double alreadyAssignedHours, double maxHours);

        DateTime GetStartDateOfWeek(AssignedTask newAssignment);

        DateTime GetEndDateOfWeek(AssignedTask newAssignment);

        bool AssignmentExceedsMaxDaysPerWeek(int assignedDaysPerWeek, int MaxDaysPerWeek);
    }
}
