using Common;
using UnityEngine;
namespace UIservice
{
    public class PlayUIController : IUIController
    {
        IUIView view;
        GameStatesType state = GameStatesType.PLAYERSTATE;
        public PlayUIController(IUIView view)
        {
            this.view = view;
            view.DisplayUI();
        }


        public void DestroyUI()
        {
            view.DestroyUI();
        }
        public void DispalyUI()
        {
            view.DisplayUI();
        }

        public GameStatesType GetUIState()
        {
            return state;
        }
    }
}