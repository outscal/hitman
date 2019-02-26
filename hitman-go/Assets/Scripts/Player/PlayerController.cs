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
            GameObject currentPlayer=currentPlayerView.GetGameObject();
            currentPlayer.transform.LookAt(_location);
            currentPlayer.transform.localPosition=Vector3.Lerp(currentPlayer.transform.localPosition, _location,1f);
        }

        private void SpawnPlayerView()
        {
            //SPAWN PLAYER PREFAB
            currentPlayerView=scriptableObject.playerView;
            GameObject playerInstance= GameObject.Instantiate(currentPlayerView.GetGameObject());
            playerInstance.transform.localPosition = spawnLocation;

        }
    }
}