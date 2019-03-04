using Common;
using UnityEngine;
namespace UIservice
{

    public class LevelFinishedUIController : IUIController
    {
        IUIView view;
        GameStatesType state = GameStatesType.LEVELFINISHEDSTATE;
        public LevelFinishedUIController(IUIView view){
            this.view=view;
            view.DisplayUI();
        }
        public void DestroyUI()
        {
            view.DestroyUI();
        }
        public GameStatesType GetUIState()
        {
            return state;
        }
    }
}