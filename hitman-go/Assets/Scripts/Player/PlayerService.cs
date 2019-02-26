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
        private PlayerDeathSignal playerDeathSignal;
        private PlayerScriptableObject playerScriptableObject;
        

        public PlayerService(IPathService _pathService,PlayerScriptableObject _playerScriptableObject)
        {
            currentPathService = _pathService;
            playerScriptableObject = _playerScriptableObject;
        }

        public void SetDirection(Directions _direction)
        {
            
            
        }

        public void SpawnPlayer(Node _node)
        {
            playerController = new PlayerController(this,_node,playerScriptableObject);

        }

        public void IncreaseScore()
        {

        }
    }
}