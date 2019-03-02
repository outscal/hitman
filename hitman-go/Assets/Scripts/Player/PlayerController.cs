using Common;
using GameState;
using InteractableSystem;
using System;
using System.Collections;
using System.Threading.Tasks;
using UnityEngine;

namespace Player
{
    public class PlayerController : IPlayerController
    {
        private IPlayerService currentPlayerService;
        private IPlayerView currentPlayerView;
        private PlayerStateMachine playerStateMachine;
        private PlayerScriptableObject scriptableObject;
        GameObject playerInstance;
        private Vector3 spawnLocation;
        public PlayerController(IPlayerService _playerService, Vector3 _spawnLocation, PlayerScriptableObject _playerScriptableObject)
        {
            currentPlayerService = _playerService;
            spawnLocation = _spawnLocation;
            scriptableObject = _playerScriptableObject;

            SpawnPlayerView();
        }

        public Vector3 GetCurrentLocation()
        {
            return spawnLocation;
        }

        async public Task MoveToLocation(Vector3 _location)
        {
            await currentPlayerView.MoveToLocation(_location);
        }

        private void SpawnPlayerView()
        {
            // currentPlayerView=scriptableObject.playerView;
            playerInstance = GameObject.Instantiate(scriptableObject.playerView.gameObject);
            currentPlayerView = playerInstance.GetComponent<PlayerView>();
            playerStateMachine = new PlayerStateMachine(currentPlayerView, currentPlayerService);

            playerInstance.transform.localPosition = spawnLocation;

        }

        public void DisablePlayer()
        {
            currentPlayerView.DisablePlayer();
        }

        public void Reset()
        {
            currentPlayerView.Reset();
        }

        private PlayerStateMachine GetCurrentStateMachine()
        {
            return playerStateMachine;
        }

        async public Task ChangePlayerState(PlayerStates _state, PlayerStates stateToChange, IInteractableController interactableController = null)
        {
            await playerStateMachine.ChangePlayerState(_state, stateToChange, interactableController);

        }
        public PlayerStates GetPlayerState()
        {
            return playerStateMachine.GetPlayerState();
        }
    }
}