using UnityEngine;
using Common;
using System.Collections;
using System;

namespace Player
{
    public class PlayerController :IPlayerController
    {
        private IPlayerService currentPlayerService;
        private PlayerView currentPlayerView;
        private Node currentNode;
        

        public PlayerController(IPlayerService _playerService, Node _spawnNode)
        {
            currentPlayerService = _playerService;
            currentNode = _spawnNode;
            SpawnPlayerView();
        }

        public Node GetCurrentNode()
        {
            return currentNode;
        }

        public void MoveToNode(Node node)
        {
            currentPlayerView.gameObject.transform.localPosition = node.nodePosition;
        }

        private void SpawnPlayerView()
        {
            //SPAWN PLAYER PREFAB
        }
    }
}