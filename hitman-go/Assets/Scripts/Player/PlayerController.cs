using UnityEngine;
using Common;
using System.Collections;
using System;

namespace Player
{
    public class PlayerController :IPlayerController
    {
        private IPlayerService currentPlayerService;
        private IPlayerView currentPlayerView;
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

        public void MoveToLocation(Vector3 _location)

        {
            currentPlayerView.MoveToLocation(_location);


        }

        private void SpawnPlayerView()
        {
            // currentPlayerView=scriptableObject.playerView;
            playerInstance = GameObject.Instantiate(scriptableObject.playerView.gameObject);
            currentPlayerView = playerInstance.GetComponent<PlayerView>();
           
            playerInstance.transform.localPosition = spawnLocation;

        }
    }
}