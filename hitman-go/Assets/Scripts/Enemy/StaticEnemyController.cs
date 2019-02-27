using UnityEngine;
using PathSystem;
using Zenject;
using Player;
using Common;
using System.Collections;

namespace Enemy
{
    public class StaticEnemyController : EnemyController
    {
        //readonly PlayerDeathSignal _playerDeathSignal;

        public StaticEnemyController(IEnemyService _enemyService, IPathService _pathService, Vector3 _spawnLocation, EnemyScriptableObject _enemyScriptableObject) : base(_enemyService,_pathService, _spawnLocation, _enemyScriptableObject)
        {
            SpawnEnemyView();

        }
        public override void MoveToNextNode(int nodeID)
        {
            //if in range()
            currentEnemyView.GetGameObject().transform.position = pathService.GetNodeLocation(nodeID);
           
        }

    }
}