using Common;
namespace UIservice
{
    public interface IUIController
    {
         void DestroyUI();
         GameStatesType GetUIState();
    }
}