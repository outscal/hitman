using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using GameState;
using Zenject;

namespace UIservice
{
    public class MenuUIControllerView : MonoBehaviour
    {

        [Inject] IGameService gameService;
        public Button nextButton, retryButton, LobyButton;
        public RectTransform starPanalTransform;
        public GameObject card;
        public void SetLevelFinishedMenu()
        {
            nextButton.gameObject.SetActive(true);
            retryButton.gameObject.SetActive(false);
        }
        public void SetLevelPausedMenu()
        {
            nextButton.gameObject.SetActive(false);
            retryButton.gameObject.SetActive(true);
        }
        // Start is called before the first frame update
        private void OnEnable()
        {
            nextButton.onClick.AddListener(LoadNext);
            retryButton.onClick.AddListener(Retry);
        }
        void OnDisable()
        {
            nextButton.onClick.RemoveListener(LoadNext);
            retryButton.onClick.RemoveListener(Retry);
        }
        void LoadNext()
        {
            gameService.IncrimentLevel();
            gameService.ChangeToLoadLevelState();
        }
        void Retry()
        {
            gameService.ChangeToGameOverState();
        }

        // Update is called once per frame
        void Update()
        {

        }
    }
}
