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
        private Node currentNode;
        

        public PlayerController(IPlayerService _playerService, Node _spawnNode, PlayerScriptableObject _playerScriptableObject)
        {
            currentPlayerService = _playerService;
            currentNode = _spawnNode;
            scriptableObject = _playerScriptableObject;    
            SpawnPlayerView();
        }
        
        public Node GetCurrentNode()
        {
            return currentNode;
        }

        public void MoveToNode(Node node)
        {
            GameObject currentPlayer=currentPlayerView.GetGameObject();
            currentPlayer.transform.LookAt(node.nodePosition);
            currentPlayer.transform.localPosition=Vector3.Lerp(currentPlayer.transform.localPosition, node.nodePosition,1f);
        }

        private void SpawnPlayerView()
        {
            //SPAWN PLAYER PREFAB
            currentPlayerView=scriptableObject.playerView;

        }
    }
}