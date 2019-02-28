using UnityEngine;
using GameState;
using PathSystem;
using Common;
using System.Collections;

namespace Enemy
{
    public class TargetEnemyController : EnemyController
    {


        public TargetEnemyController(IEnemyService _enemyService, IPathService _pathService, IGameService _gameService, Vector3 _spawnLocation, EnemyScriptableObject _enemyScriptableObject, int currentNodeID, Directions spawnDirection) : base(_enemyService, _pathService, _gameService, _spawnLocation, _enemyScriptableObject, currentNodeID, spawnDirection)
        {


        }

    }
}