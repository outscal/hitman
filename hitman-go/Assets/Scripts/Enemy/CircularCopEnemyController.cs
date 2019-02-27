using UnityEngine;
using Common;
using PathSystem;
using GameState.Interface;
using System.Collections;

namespace Enemy
{
    public class CircularCopEnemyController : EnemyController
    {


        public CircularCopEnemyController(IEnemyService _enemyService, IPathService _pathService, IGameService _gameService, Vector3 _spawnLocation, EnemyScriptableObject _enemyScriptableObject, int currentNodeID, Directions spawnDirection) : base(_enemyService, _pathService, _gameService, _spawnLocation, _enemyScriptableObject, currentNodeID, spawnDirection)
        {


        }

    }
}