using System.Threading.Tasks;

namespace MD.Xamarin.Interfaces
{
    /// <summary>
    /// This service provides custom alerts.
    /// </summary>
    public interface IAlertService
    {
        /// <summary>
        /// Show alert popup window with message and button with text.
        /// </summary>
        /// <param name="message">message to show.</param>
        /// <param name="okButtonText">text for button.</param>
        void ShowOkAlert(string message, string okButtonText);

        /// <summary>
        /// Show alert popup window with message and two buttons with text.
        /// <para>return bool value</para>
        /// </summary>
        /// <param name="message">message to show.</param>
        /// <param name="yesButtonText">text for first button.</param>
        /// <param name="noButtonText">text for second button.</param>
        /// <returns></returns>
        Task<bool> ShowYesNoAlert(string message, string yesButtonText, string noButtonText);
    }
}