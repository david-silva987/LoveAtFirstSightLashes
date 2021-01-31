/// <summary>
/// Toasts generation using Xamarin
/// https://stackoverflow.com/questions/35279403/toast-equivalent-for-xamarin-forms
/// </summary>

namespace LoveAtFirstSightLashes.Interfaces
{
    public interface IMessage
    {
        void ShortAlert(string message);
        void LongAlert(string message);
    }

}