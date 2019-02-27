using UnityEngine;
using PathSystem;
using GameState.Interface;
using Common;
using System.Collections;

namespace Enemy
{
    public class ShieldedEnemyController : EnemyController
    {


        public ShieldedEnemyController(IEnemyService _enemyService, IPathService _pathService, IGameService _gameService, Vector3 _spawnLocation, EnemyScriptableObject _enemyScriptableObject, int currentNodeID, Directions spawnDirection) : base(_enemyService, _pathService, _gameService, _spawnLocation, _enemyScriptableObject, currentNodeID, spawnDirection)
        {


        }

    }
}