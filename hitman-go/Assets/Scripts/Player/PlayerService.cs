using UnityEngine;
using System.Collections;
using PathSystem;
using Common;

namespace Player
{
    public class PlayerService : IPlayerService
    {
        private PlayerController playerController;
        private IPathService currentPathService;
        

        public PlayerService(IPathService _pathService)
        {
            currentPathService = _pathService;
        }

        public void SetDirection(Directions _direction)
        {
            Node currentNode = playerController.GetCurrentNode();
            Node nextNode=currentPathService.GetNode(currentNode, _direction);

            playerController.MoveToNode(nextNode);
        }

        public void SpawnPlayer(Node node)
        {
            playerController = new PlayerController(this,node);
        }
    }
}