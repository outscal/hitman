using UnityEngine;
using System;
using System.Collections;
using System.Collections.Generic;
using PathSystem;
using Common;

namespace Enemy
{
    public class EnemyService : IEnemyService
    {
        private List<EnemyController> enemyList = new List<EnemyController>();
        private IPathService pathService;

        public EnemyService(IPathService _pathService)
        {
            pathService = _pathService;
        }

       

        public void SpawnEnemy(EnemyScriptableObjectList scriptableObjectList)
        {
            for(int i=0;i<scriptableObjectList.enemyList.Count;i++)
            {
                SpawnSingleEnemy(scriptableObjectList.enemyList[i]);
            }
        }

        private void SpawnSingleEnemy(EnemyScriptableObject _enemyScriptableObject)
        {
            List<Vector3> spawnLocations = new List<Vector3>();
            switch(_enemyScriptableObject.enemyType)
            {
                case EnemyType.STATIC:
                    spawnLocations.Clear();
                    spawnLocations=pathService.GetSpawnLocation(EnemyType.STATIC);
                    for (int i = 0; i < spawnLocations.Count; i++)
                    {
                        EnemyController newEnemy = new StaticEnemyController(this,spawnLocations[i],_enemyScriptableObject);
                        enemyList.Add(newEnemy);
                        
                    }
                    break;
                case EnemyType.PATROLLING:
                    break;
                case EnemyType.ROTATING_KNIFE:
                    break;
                case EnemyType.CIRCULAR_COP:
                    break;
                case EnemyType.SHIELDED:
                    break;
                case EnemyType.DOGS:
                    break;
                case EnemyType.SNIPER:
                    break;
                default:
                    Debug.Log("No Enemy Controller of this type");
                    break;

                    
            }
        }
    }
}