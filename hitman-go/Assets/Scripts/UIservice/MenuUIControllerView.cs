using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using GameState;
using Zenject;
using PathSystem;
using Common;
using StarSystem;
using SavingSystem;

namespace UIservice
{
    public class MenuUIControllerView : MonoBehaviour
    {

        [Inject] IGameService gameService;
        [Inject] IPathService pathService;
        [Inject] ISaveService saveService;
        bool levelComplete;
        [Inject] IStarService starService;
        List<CardControllerView> cards = new List<CardControllerView>();
        public Button nextButton, retryButton, LobyButton;
        public RectTransform starPanalTransform;
        public GameObject card;
        public void SetLevelFinishedMenu()
        {
            ClearCards();
            nextButton.gameObject.SetActive(true);
            retryButton.gameObject.SetActive(false);
            levelComplete = true;
            SetCards();
        }
        void ClearCards()
        {
            for (int i = 0; i < cards.Count; i++)
            {
                Destroy(cards[i].gameObject);
            }
            cards.Clear();
        }
        public void SetLevelPausedMenu()
        {
            ClearCards();
            levelComplete = false;
            nextButton.gameObject.SetActive(false);
            retryButton.gameObject.SetActive(true);
            SetCards();
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
        public void SetCards()
        {
            List<StarTypes> stars = pathService.GetStarsForLevel();
            CardControllerView cardview;
            cardview = Instantiate(card, starPanalTransform).GetComponent<CardControllerView>();
            cards.Add(cardview);
            cardview.setCardName("Level Complete");
            cardview.SetAchievement(levelComplete);
            for (int i = 0; i < stars.Count; i++)
            {
                
                cardview = Instantiate(card, starPanalTransform).GetComponent<CardControllerView>();
                cardview.setCardName(stars[i].ToString());
                cardview.SetAchievement(saveService.ReadStarTypeForLevel(gameService.GetCurrentLevel(),stars[i]));
                cards.Add(cardview);
            }
        }
        // Update is called once per frame
    }
}
