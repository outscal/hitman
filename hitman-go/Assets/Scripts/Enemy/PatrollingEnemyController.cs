using UnityEngine;
using Common;
using System.Collections;

namespace Enemy
{
    public class PatrollingEnemyController : EnemyController
    {


        public PatrollingEnemyController(IEnemyService _enemyService, Vector3 _spawnLocation, EnemyScriptableObject _enemyScriptableObject) : base(_enemyService, _spawnLocation, _enemyScriptableObject)
        {


        }

    }
}