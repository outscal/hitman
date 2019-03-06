using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Zenject;
using GameState;
using SavingSystem;
using PathSystem;

namespace UIservice
{
    public class LobbyUIView : MonoBehaviour, IUIView
    {
        [SerializeField]
        private GameObject buttonContainer;
        [SerializeField]
        private GameObject lobbyCardPrefab;
        [Inject] IGameService gameService;
        [Inject] IPathService pathService;
        [Inject] ISaveService saveService;

        //private List<LobbyCardController> lobbyCardControllerList;

        public void DestroyUI()
        {
            gameObject.SetActive(false);
        }

        public void DisplayUI()
        {
            gameObject.SetActive(true);

            CreateLevelButtons();
        }

        void CreateLevelButtons()
        {
            for (int i = 0; i < gameService.GetNumberOfLevels(); i++)
            {
                GameObject lobbyCard = Instantiate(lobbyCardPrefab);
                lobbyCard.transform.SetParent(buttonContainer.transform);
                LobbyCardController lobbyCardController = lobbyCard.GetComponent<LobbyCardController>();
                if(i <= saveService.ReadMaxLevel())
                {
                    lobbyCardController.DefaultSettings(true, i, this);
                }
                else
                {
                    lobbyCardController.DefaultSettings(false, i, this);
                }
            }
        }

        public IGameService ReturnGameService()
        {
            return gameService;
        }

        public IPathService ReturnPathService()
        {
            return pathService; 
        }

        public ISaveService ReturnSaveService()
        {
            return saveService; 
        }
    }
}