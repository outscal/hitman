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
       

        public EnemyService(IPathService _pathService,EnemyScriptableObjectList enemyList)
        {
            pathService = _pathService;
            SpawnEnemy(enemyList);
        }

        public void PerformMovement()
        {
            for (int i = 0; i < enemyList.Count; i++)
            {
                enemyList[i].Move();
            }
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
            List<int> spawnNodeID = new List<int>();

            switch(_enemyScriptableObject.enemyType)
            {
                case EnemyType.STATIC:
                    spawnNodeID.Clear();

                    spawnNodeID = pathService.GetEnemySpawnLocation(EnemyType.STATIC);
                    for (int i = 0; i < spawnNodeID.Count; i++)
                    {
                        Vector3 spawnLocation=pathService.GetNodeLocation(spawnNodeID[i]);
                        EnemyController newEnemy = new StaticEnemyController(this,pathService,spawnLocation,_enemyScriptableObject,spawnNodeID[i],pathService.GetEnemySpawnDirection(spawnNodeID[i]));
                        enemyList.Add(newEnemy);                        
                    }
                    break;

                case EnemyType.PATROLLING:
                    spawnNodeID.Clear();
                    spawnNodeID = pathService.GetEnemySpawnLocation(EnemyType.STATIC);
                    for (int i = 0; i < spawnNodeID.Count; i++)
                    {
                        Vector3 spawnLocation = pathService.GetNodeLocation(spawnNodeID[i]);
                        EnemyController newEnemy = new PatrollingEnemyController(this, pathService, spawnLocation, _enemyScriptableObject, spawnNodeID[i], pathService.GetEnemySpawnDirection(spawnNodeID[i]));
                        enemyList.Add(newEnemy);
                    }
                    break;

                case EnemyType.ROTATING_KNIFE:
                    spawnNodeID.Clear();
                    spawnNodeID = pathService.GetEnemySpawnLocation(EnemyType.STATIC);
                    for (int i = 0; i < spawnNodeID.Count; i++)
                    {
                        Vector3 spawnLocation = pathService.GetNodeLocation(spawnNodeID[i]);
                        EnemyController newEnemy = new RotatingKnifeEnemyController(this, pathService, spawnLocation, _enemyScriptableObject, spawnNodeID[i], pathService.GetEnemySpawnDirection(spawnNodeID[i]));
                        enemyList.Add(newEnemy);
                    }
                    break;

                case EnemyType.CIRCULAR_COP:
                    spawnNodeID.Clear();
                    spawnNodeID = pathService.GetEnemySpawnLocation(EnemyType.STATIC);
                    for (int i = 0; i < spawnNodeID.Count; i++)
                    {
                        Vector3 spawnLocation = pathService.GetNodeLocation(spawnNodeID[i]);
                        EnemyController newEnemy = new CircularCopEnemyController(this, pathService,spawnLocation, _enemyScriptableObject, spawnNodeID[i], pathService.GetEnemySpawnDirection(spawnNodeID[i]));
                        enemyList.Add(newEnemy);
                    }
                    break;

                case EnemyType.SHIELDED:
                    spawnNodeID.Clear();
                    spawnNodeID = pathService.GetEnemySpawnLocation(EnemyType.STATIC);
                    for (int i = 0; i < spawnNodeID.Count; i++)
                    {
                        Vector3 spawnLocation = pathService.GetNodeLocation(spawnNodeID[i]);
                        EnemyController newEnemy = new ShieldedEnemyController(this, pathService, spawnLocation, _enemyScriptableObject, spawnNodeID[i], pathService.GetEnemySpawnDirection(spawnNodeID[i]));
                        enemyList.Add(newEnemy);
                    }
                    break;

                case EnemyType.DOGS:
                    spawnNodeID.Clear();
                    spawnNodeID = pathService.GetEnemySpawnLocation(EnemyType.STATIC);
                    for (int i = 0; i < spawnNodeID.Count; i++)
                    {
                        Vector3 spawnLocation = pathService.GetNodeLocation(spawnNodeID[i]);
                        EnemyController newEnemy = new DogsEnemyController(this, pathService,spawnLocation, _enemyScriptableObject, spawnNodeID[i], pathService.GetEnemySpawnDirection(spawnNodeID[i]));
                        enemyList.Add(newEnemy);
                    }
                    break;

                case EnemyType.SNIPER:
                    spawnNodeID.Clear();
                    spawnNodeID = pathService.GetEnemySpawnLocation(EnemyType.STATIC);
                    for (int i = 0; i < spawnNodeID.Count; i++)
                    {
                        Vector3 spawnLocation = pathService.GetNodeLocation(spawnNodeID[i]);
                        EnemyController newEnemy = new SniperEnemyController(this, pathService, spawnLocation, _enemyScriptableObject, spawnNodeID[i], pathService.GetEnemySpawnDirection(spawnNodeID[i]));
                        enemyList.Add(newEnemy);
                    }
                    break;

                case EnemyType.TARGET:
                    spawnNodeID.Clear();
                    spawnNodeID = pathService.GetEnemySpawnLocation(EnemyType.STATIC);
                    for (int i = 0; i < spawnNodeID.Count; i++)
                    {
                        Vector3 spawnLocation = pathService.GetNodeLocation(spawnNodeID[i]);
                        EnemyController newEnemy = new TargetEnemyController(this, pathService,spawnLocation, _enemyScriptableObject, spawnNodeID[i], pathService.GetEnemySpawnDirection(spawnNodeID[i]));
                        enemyList.Add(newEnemy);
                    }
                    break;

                default:
                    Debug.Log("No Enemy Controller of this type");
                    break;
                    
            }
        }
    }
}