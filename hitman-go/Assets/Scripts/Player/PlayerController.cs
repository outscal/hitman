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
            //Debug.Log("next Node"+_location);
           // GameObject currentPlayer=currentPlayerView.GetGameObject();
            playerInstance.transform.LookAt(_location);
            playerInstance.transform.localPosition=Vector3.Lerp(playerInstance.transform.localPosition, _location,1f);
            //playerInstance.transform.position=_location;
            Debug.Log(playerInstance.transform.position);
        }

        private void SpawnPlayerView()
        {
            //SPAWN PLAYER PREFAB
            currentPlayerView=scriptableObject.playerView;
            playerInstance= GameObject.Instantiate(currentPlayerView.GetGameObject());
            playerInstance.transform.localPosition = spawnLocation;

        }
    }
}