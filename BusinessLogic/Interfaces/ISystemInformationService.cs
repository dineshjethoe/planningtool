namespace BusinessLogic.Interfaces
{
    /// <summary>
    /// This interface checks if the current OS is in high contrast mode
    /// </summary>
    public interface ISystemInformationService
    {
        bool IsHighContrastColourScheme { get; }
    }
}
