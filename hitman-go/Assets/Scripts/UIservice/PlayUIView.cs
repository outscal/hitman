using UnityEngine;
using UnityEngine.UI;
using Zenject;
using GameState;
namespace UIservice
{
    public class PlayUIView : MonoBehaviour, IUIView
    {

        [Inject]IGameService gameService;
        public Button retry, menu;
        public MenuUIControllerView menuView;
        public void DestroyUI()
        {
            menu.onClick.RemoveListener(Menu);
            retry.onClick.RemoveListener(Retry);
            menuView.gameObject.SetActive(false);
            gameObject.SetActive(false);
        }

        public void DisplayUI()
        {
            menu.onClick.AddListener(Menu);
            retry.onClick.AddListener(Retry);
            gameObject.SetActive(true);
        }
        void Retry(){
            gameService.ChangeToGameOverState();
        }
        void Menu(){
            menuView.gameObject.SetActive(true);
            menuView.SetLevelPausedMenu();
        }
    }
}