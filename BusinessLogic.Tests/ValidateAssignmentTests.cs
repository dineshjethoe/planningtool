using System;
using System.Collections.Generic;
using System.Linq;
using BusinessLogic.Interfaces;
using BusinessLogic.Services;
using Entities;
using NUnit.Framework;

namespace BusinessLogic.Tests
{
    /// <summary>
    /// These test functions assert (test) the business logic functions to validate an assigment.
    /// </summary>
    [TestFixture]
    public class ValidateAssignmentTests
    {
        private IValidateAssignment validateAssignment;
        private List<AssignedTask> assignedTasks;

        [SetUp]
        public void TestSetup()
        {
            assignedTasks = new List<AssignedTask>();
            validateAssignment = new ValidateAssignment();
        }

        [TestCaseSource(typeof(TaskAssignmentsSource))]
        public void When_EmployeeHasAssignmentsOnDate_Expects_True(IEnumerable<AssignedTask> assignedTasks)
        {
            //arrange
            var newAssignment = new AssignedTask
            {
                EmployeeId = 26,
                AssignmentDate = new DateTime(2018, 08, 20),
                StartTime = new TimeSpan(10, 30, 00),
                EndTime = new TimeSpan(10, 45, 00),
            };
            var assignments = assignedTasks.ToList();

            //act
            bool result = validateAssignment.AssigneeHasAlreadyAssignmentsOnDate(newAssignment, assignments);

            //assert
            Assert.That(result, Is.True);
        }

        [TestCaseSource(typeof(TaskAssignmentsSource))]
        public void When_EmployeeHasAssignmentsOnDate_Expects_False(IEnumerable<AssignedTask> assignedTasks)
        {
            //arrange
            var newAssignment = new AssignedTask
            {
                EmployeeId = 26,
                AssignmentDate = new DateTime(2018, 08, 30),
                StartTime = new TimeSpan(10, 30, 00),
                EndTime = new TimeSpan(10, 45, 00),
            };
            var assignments = assignedTasks.ToList();

            //act
            bool result = validateAssignment.AssigneeHasAlreadyAssignmentsOnDate(newAssignment, assignments);

            //assert
            Assert.That(result, Is.False);
        }

        [TestCaseSource(typeof(TaskAssignmentsSource))]
        public void When_TheStartTimeOfTheNewAssignmentIsGreaterThanTheEndTimeOfTheLastAssignment_Expects_True(IEnumerable<AssignedTask> assignedTasks)
        {
            //arrange
            var newAssignment = new AssignedTask
            {
                EmployeeId = 26,
                AssignmentDate = new DateTime(2018, 08, 20),
                StartTime = new TimeSpan(18, 00, 00),
                EndTime = new TimeSpan(22, 00, 00),
            };
            var assignments = assignedTasks.ToList();

            //act
            var result = validateAssignment
                .IsTheStartTimeOfTheNewAssignmentGreaterThanTheEndTimeOfTheLastAssignment(newAssignment, assignments);

            //assert
            Assert.That(result, Is.True);
        }

        [TestCaseSource(typeof(TaskAssignmentsSource))]
        public void When_TheStartTimeOfTheNewAssignmentIsGreaterThanTheEndTimeOfTheLastAssignment_Expects_False(IEnumerable<AssignedTask> assignedTasks)
        {
            //arrange
            var newAssignment = new AssignedTask
            {
                EmployeeId = 26,
                AssignmentDate = new DateTime(2018, 08, 20),
                StartTime = new TimeSpan(10, 00, 00),
                EndTime = new TimeSpan(13, 00, 00),
            };
            var assignments = assignedTasks.ToList();

            //act
            var result = validateAssignment
                .IsTheStartTimeOfTheNewAssignmentGreaterThanTheEndTimeOfTheLastAssignment(newAssignment, assignments);

            //assert
            Assert.That(result, Is.False);
        }

        [TestCase]
        public void When_CurrentDateIs12December2018_Expects_FirstDateOfWeek9December2018()
        {
            //arrange
            var newAssignment = new AssignedTask
            {
                EmployeeId = 26,
                AssignmentDate = new DateTime(2018, 12, 12),
                StartTime = new TimeSpan(10, 00, 00),
                EndTime = new TimeSpan(13, 00, 00),
            };

            var expected = new DateTime(2018, 12, 9);

            //act
            var result = validateAssignment
                .GetStartDateOfWeek(newAssignment);

            //assert
            Assert.That(result, Is.EqualTo(expected));
        }

        [TestCase]
        public void When_CurrentDateIs12December2018_Expects_LastDateOfWeek15December2018()
        {
            //arrange
            var newAssignment = new AssignedTask
            {
                EmployeeId = 26,
                AssignmentDate = new DateTime(2018, 12, 12),
                StartTime = new TimeSpan(10, 00, 00),
                EndTime = new TimeSpan(13, 00, 00),
            };

            var expected = new DateTime(2018, 12, 15);

            //act
            var result = validateAssignment
                .GetEndDateOfWeek(newAssignment);

            //assert
            Assert.That(result, Is.EqualTo(expected));
        }

        [TestCaseSource(typeof(TaskAssignmentsSource))]
        public void When_AssignedDaysPerWeekIs4_Expects_False(IEnumerable<AssignedTask> assignedTasks)
        {
            //arrange
            var assignedDaysPerWeek = 4;
            var maxDaysPerWeek = 5;

            //act
            var result = validateAssignment
                .AssignmentExceedsMaxDaysPerWeek(assignedDaysPerWeek, maxDaysPerWeek);

            //assert
            Assert.That(result, Is.False);
        }

        [TestCaseSource(typeof(TaskAssignmentsSource))]
        public void When_AssignedDaysPerWeekIs5_Expects_False(IEnumerable<AssignedTask> assignedTasks)
        {
            //arrange
            var assignedDaysPerWeek = 5;
            var maxDaysPerWeek = 5;

            //act
            var result = validateAssignment
                .AssignmentExceedsMaxDaysPerWeek(assignedDaysPerWeek, maxDaysPerWeek);

            //assert
            Assert.That(result, Is.False);
        }

        [TestCaseSource(typeof(TaskAssignmentsSource))]
        public void When_AssignedDaysPerWeekIs6_Expects_True(IEnumerable<AssignedTask> assignedTasks)
        {
            //arrange
            var assignedDaysPerWeek = 6;
            var maxDaysPerWeek = 5;

            //act
            var result = validateAssignment
                .AssignmentExceedsMaxDaysPerWeek(assignedDaysPerWeek, maxDaysPerWeek);

            //assert
            Assert.That(result, Is.True);
        }

        [TestCaseSource(typeof(TaskAssignmentsSource))]
        public void When_TotalAssignedHoursOfEmployeeOnDateIs7_Expects_7(IEnumerable<AssignedTask> assignedTasks)
        {
            //arrange
            var newAssignment = new AssignedTask
            {
                EmployeeId = 26,
                AssignmentDate = new DateTime(2018, 08, 21),
                StartTime = new TimeSpan(18, 00, 00),
                EndTime = new TimeSpan(19, 00, 00),
            };
            var assignments = assignedTasks.ToList();
            var expected = 7d;

            //act
            var result = validateAssignment.GetTotalAssignedHoursOfEmployeeOnDate(newAssignment, assignments);

            //assert
            Assert.AreEqual(result, expected);
        }

        [TestCase]
        public void When_TotalAlreadyAssignedHoursOfEmployeeOnDateIs7_Expects_False()
        {
            //arrange
            var newAssignment = new AssignedTask
            {
                EmployeeId = 26,
                AssignmentDate = new DateTime(2018, 08, 21),
                StartTime = new TimeSpan(18, 00, 00),
                EndTime = new TimeSpan(19, 00, 00),
            };
            var totalHoursAssignedAlready = 7d;
            var maxHoursPerDay = 8d;

            //act
            var result = validateAssignment.ExceedsMaxHoursPerDay(newAssignment, totalHoursAssignedAlready, maxHoursPerDay);

            //assert
            Assert.That(result, Is.False);
        }

        [TestCase]
        public void When_TotalAssignedHoursOfEmployeeOnDateIs9_Expects_True()
        {
            //arrange
            var newAssignment = new AssignedTask
            {
                EmployeeId = 26,
                AssignmentDate = new DateTime(2018, 08, 21),
                StartTime = new TimeSpan(18, 00, 00),
                EndTime = new TimeSpan(19, 00, 00),
            };
            var totalHoursAssignedAlready = 8d;
            var maxHoursPerDay = 8d;

            //act
            var result = validateAssignment.ExceedsMaxHoursPerDay(newAssignment, totalHoursAssignedAlready, maxHoursPerDay);

            //assert
            Assert.That(result, Is.True);
        }
    }
}
