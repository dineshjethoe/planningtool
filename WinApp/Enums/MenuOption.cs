using WinApp.Attributes;

namespace WinApp.Enums
{
    public enum MenuOption
    {
        [MenuOption("Employees")]
        Employees,
        [MenuOption("Tasks")]
        Tasks,
        [MenuOption("Assigned Tasks")]
        AssignedTasks,
        [MenuOption("Add")]
        Add,
        [MenuOption("Edit")]
        Edit,
        [MenuOption("Remove")]
        Remove
    }
}
