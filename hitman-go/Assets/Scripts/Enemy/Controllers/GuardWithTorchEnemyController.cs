using UnityEngine;
using GameState;
using PathSystem;
using Common;
using System.Collections;

namespace Enemy
{
    public class GuardWithTorchEnemyController : EnemyController
    {


        public GuardWithTorchEnemyController(IEnemyService _enemyService, IPathService _pathService, IGameService _gameService, Vector3 _spawnLocation, EnemyScriptableObject _enemyScriptableObject, int currentNodeID, Directions spawnDirection, bool _hasShield) : base(_enemyService, _pathService, _gameService, _spawnLocation, _enemyScriptableObject, currentNodeID, spawnDirection, _hasShield)
        {
            enemyType = EnemyType.GUARD_TORCH;

        }
        protected override void SetController()
        {
            currentEnemyView.SetCurrentController(this);
        }

    }
}