using UnityEngine;
using UnityEngine.UI;
using Zenject;
using GameState;
namespace UIservice
{
    public class PlayUIView : MonoBehaviour, IUIView
    {

        [Inject] IGameService gameService;
        public Button retry, menu,closeButton;
        public MenuUIControllerView menuView;
        public void DestroyUI()
        {
            menu.onClick.RemoveListener(Menu);
            retry.onClick.RemoveListener(Retry);
            closeButton.onClick.RemoveListener(CloseMenu);
            menuView.gameObject.SetActive(false);
            gameObject.SetActive(false);
        }
         void CloseMenu(){
            closeButton.gameObject.SetActive(false);
            menuView.gameObject.SetActive(false);
            retry.gameObject.SetActive(true);
            menu.gameObject.SetActive(true);
        }

        public void DisplayUI()
        {
            closeButton.onClick.AddListener(CloseMenu);
            menu.onClick.AddListener(Menu);
            retry.onClick.AddListener(Retry);
            gameObject.SetActive(true);
            closeButton.gameObject.SetActive(false);
            retry.gameObject.SetActive(true);
            menu.gameObject.SetActive(true);
        }
        void Retry()
        {
            gameService.ChangeToGameOverState();
        }
        void Menu()
        {
            menuView.gameObject.SetActive(true);
            menuView.SetLevelPausedMenu();
            closeButton.gameObject.SetActive(true);
            retry.gameObject.SetActive(false);
            menu.gameObject.SetActive(false);            
        }
    }
}