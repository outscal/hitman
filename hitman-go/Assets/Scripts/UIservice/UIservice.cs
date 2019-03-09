using Zenject;
using UnityEngine;
using GameState;
using Common;
namespace UIservice
{
    public class UIService : IUIService
    {
        SignalBus signalBus;
        PlayUIView playView;
        GameOverUIView overView;
        LevelFinishedUIView finishedUIView;
        LobbyUIView lobbyUIView;

        IUIController currentUI, previousUI;

        public UIService(SignalBus signalBus)
        {
            this.signalBus = signalBus;
            signalBus.Subscribe<StateChangeSignal>(OnGameStateChanged);
            playView = GameObject.FindObjectOfType<PlayUIView>();
            overView = GameObject.FindObjectOfType<GameOverUIView>();
            finishedUIView = GameObject.FindObjectOfType<LevelFinishedUIView>();
            lobbyUIView = GameObject.FindObjectOfType<LobbyUIView>();
            playView.gameObject.SetActive(false);
            overView.gameObject.SetActive(false);
            finishedUIView.gameObject.SetActive(false);
        }
        public void OnGameStateChanged(StateChangeSignal state)
        {
            switch (state.newGameState)
            {
                case GameStatesType.PLAYERSTATE:
                    if (currentUI != null)
                    {
                        previousUI = currentUI;
                    }
                    else
                    {
                        currentUI = new PlayUIController(playView);
                    }
                    if (currentUI.GetUIState() != GameStatesType.PLAYERSTATE)
                    {
                        currentUI = new PlayUIController(playView);
                        if (previousUI != null && previousUI.GetUIState() != GameStatesType.PLAYERSTATE)
                        {
                            previousUI.DestroyUI();
                        }
                    }
                    break;
                case GameStatesType.GAMEOVERSTATE:
                    previousUI = currentUI;
                    previousUI.DestroyUI();
                    currentUI = new GameOverUIController(overView);
                    break;
                case GameStatesType.LEVELFINISHEDSTATE:
                    previousUI = currentUI;
                    previousUI.DestroyUI();
                    currentUI = new LevelFinishedUIController(finishedUIView);
                    break;
                case GameStatesType.LOBBYSTATE:
                    previousUI = currentUI;
                    if (previousUI != null)
                        previousUI.DestroyUI();

                    currentUI = new LobbyUIController(lobbyUIView);
                    break;

            }
                
        }
    }
}