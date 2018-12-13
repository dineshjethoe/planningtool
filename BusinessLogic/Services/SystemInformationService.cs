using BusinessLogic.Interfaces;
using System.Windows.Forms;

namespace BusinessLogic.Services
{
    /// <summary>
    /// This class implements ISystemInformationService
    /// The main purpose is to check whether the current OS theme is in high contrast mode
    /// </summary>
    public class SystemInformationService : ISystemInformationService
    {
        public bool IsHighContrastColourScheme { get { return SystemInformation.HighContrast; } }
    }
}
