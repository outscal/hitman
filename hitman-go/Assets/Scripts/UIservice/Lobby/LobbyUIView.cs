using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Zenject;
using GameState;
using SavingSystem;
using PathSystem;
using UnityEngine.UI;
using System.Threading.Tasks;

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

        private List<LobbyCardController> lobbyCardControllerList;

        void Awake()
        {
            lobbyCardControllerList = new List<LobbyCardController>();
        }

        public void DestroyUI()
        {
            gameObject.SetActive(false);
        }

        public void DisplayUI()
        {
            gameObject.SetActive(true);

            if (lobbyCardControllerList.Count <= 0)
                CreateLevelButtonsAsync();
            else
            {
                UpdateLobbyUI();
            }
        }

        void CreateLevelButtonsAsync()
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

                lobbyCardControllerList.Add(lobbyCardController);
            }

        }

        void UpdateLobbyUI()
        {
            for (int i = 0; i < lobbyCardControllerList.Count; i++)
            {
                if (i <= saveService.ReadMaxLevel())
                {
                    lobbyCardControllerList[i].DefaultSettings(true, i, this);
                }
                else
                {
                    lobbyCardControllerList[i].DefaultSettings(false, i, this);
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