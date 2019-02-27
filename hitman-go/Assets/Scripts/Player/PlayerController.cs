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
<<<<<<< HEAD
        private Vector3 spawnLocation;
        private GameObject playerInstance;
=======

         GameObject playerInstance;
        private Vector3 spawnLocation;
>>>>>>> fce40dc3f5bda65f2644f01f554157912183462a
        

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
<<<<<<< HEAD
        {           
            currentPlayerView.MoveToLocation(_location);
           
=======
        {
            //Debug.Log("next Node"+_location);
           // GameObject currentPlayer=currentPlayerView.GetGameObject();
            playerInstance.transform.LookAt(_location);
            playerInstance.transform.localPosition=Vector3.Lerp(playerInstance.transform.localPosition, _location,1f);
            //playerInstance.transform.position=_location;
            Debug.Log(playerInstance.transform.position);
>>>>>>> fce40dc3f5bda65f2644f01f554157912183462a
        }

        private void SpawnPlayerView()
        {
            //SPAWN PLAYER PREFAB
<<<<<<< HEAD

            // currentPlayerView=scriptableObject.playerView;
            playerInstance = GameObject.Instantiate(scriptableObject.playerView.gameObject);
            currentPlayerView = playerInstance.GetComponent<PlayerView>();
=======
            currentPlayerView=scriptableObject.playerView;
            playerInstance= GameObject.Instantiate(currentPlayerView.GetGameObject());
>>>>>>> fce40dc3f5bda65f2644f01f554157912183462a
            playerInstance.transform.localPosition = spawnLocation;

        }
    }
}