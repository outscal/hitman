using Common;
using GameState;
using System;
using System.Threading.Tasks;
using System.Collections;
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
            playerStateMachine = new PlayerStateMachine(currentPlayerView);

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

        public PlayerStateMachine GetCurrentStateMachine()
        {
            return playerStateMachine;
        }
       
    }
}