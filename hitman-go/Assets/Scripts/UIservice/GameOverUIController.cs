using Common;
using UnityEngine;
namespace UIservice
{
    public class GameOverUIController : IUIController
    {
        IUIView view;
        GameStatesType state = GameStatesType.GAMEOVERSTATE;
        public GameOverUIController(IUIView view)
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