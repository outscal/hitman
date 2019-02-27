using UnityEngine;
using Common;
using System.Collections;

namespace Enemy
{
    public class ShieldedEnemyController : EnemyController
    {


        public ShieldedEnemyController(IEnemyService _enemyService, Vector3 _spawnLocation, EnemyScriptableObject _enemyScriptableObject) : base(_enemyService, _spawnLocation, _enemyScriptableObject)
        {


        }

    }
}