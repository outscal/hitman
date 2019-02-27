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

<<<<<<< HEAD
        public EnemyService(IPathService _pathService)
        {
            pathService = _pathService;
        }

       

=======
        public EnemyService(IPathService _pathService,EnemyScriptableObjectList enemyList)
        {
            pathService = _pathService;
            SpawnEnemy(enemyList);
        }
>>>>>>> fce40dc3f5bda65f2644f01f554157912183462a
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
<<<<<<< HEAD
            
=======
>>>>>>> fce40dc3f5bda65f2644f01f554157912183462a
            switch(_enemyScriptableObject.enemyType)
            {
                case EnemyType.STATIC:
                    spawnNodeID.Clear();
<<<<<<< HEAD
                    spawnNodeID = pathService.GetEnemySpawnLocation(EnemyType.STATIC);
                    for (int i = 0; i < spawnNodeID.Count; i++)
                    {
                        Vector3 spawnLocation=pathService.GetNodeLocation(spawnNodeID[i]);
                        EnemyController newEnemy = new StaticEnemyController(this,spawnLocation,_enemyScriptableObject);
                        enemyList.Add(newEnemy);                        
                    }
                    break;

                case EnemyType.PATROLLING:
                    spawnNodeID.Clear();
                    spawnNodeID = pathService.GetEnemySpawnLocation(EnemyType.STATIC);
                    for (int i = 0; i < spawnNodeID.Count; i++)
                    {
                        Vector3 spawnLocation = pathService.GetNodeLocation(spawnNodeID[i]);
                        EnemyController newEnemy = new PatrollingEnemyController(this, spawnLocation, _enemyScriptableObject);
                        enemyList.Add(newEnemy);
                    }
                    break;
                case EnemyType.ROTATING_KNIFE:
                    spawnNodeID.Clear();
                    spawnNodeID = pathService.GetEnemySpawnLocation(EnemyType.STATIC);
                    for (int i = 0; i < spawnNodeID.Count; i++)
                    {
                        Vector3 spawnLocation = pathService.GetNodeLocation(spawnNodeID[i]);
                        EnemyController newEnemy = new RotatingKnifeEnemyController(this, spawnLocation, _enemyScriptableObject);
                        enemyList.Add(newEnemy);
                    }
                    break;
                case EnemyType.CIRCULAR_COP:
                    spawnNodeID.Clear();
                    spawnNodeID = pathService.GetEnemySpawnLocation(EnemyType.STATIC);
                    for (int i = 0; i < spawnNodeID.Count; i++)
                    {
                        Vector3 spawnLocation = pathService.GetNodeLocation(spawnNodeID[i]);
                        EnemyController newEnemy = new CircularCopEnemyController(this, spawnLocation, _enemyScriptableObject);
                        enemyList.Add(newEnemy);
                    }
                    break;
                case EnemyType.SHIELDED:
                    spawnNodeID.Clear();
                    spawnNodeID = pathService.GetEnemySpawnLocation(EnemyType.STATIC);
                    for (int i = 0; i < spawnNodeID.Count; i++)
                    {
                        Vector3 spawnLocation = pathService.GetNodeLocation(spawnNodeID[i]);
                        EnemyController newEnemy = new ShieldedEnemyController(this, spawnLocation, _enemyScriptableObject);
                        enemyList.Add(newEnemy);
                    }
                    break;
                case EnemyType.DOGS:
                    spawnNodeID.Clear();
                    spawnNodeID = pathService.GetEnemySpawnLocation(EnemyType.STATIC);
                    for (int i = 0; i < spawnNodeID.Count; i++)
                    {
                        Vector3 spawnLocation = pathService.GetNodeLocation(spawnNodeID[i]);
                        EnemyController newEnemy = new DogsEnemyController(this, spawnLocation, _enemyScriptableObject);
                        enemyList.Add(newEnemy);
                    }
                    break;
                case EnemyType.SNIPER:
                    spawnNodeID.Clear();
                    spawnNodeID = pathService.GetEnemySpawnLocation(EnemyType.STATIC);
                    for (int i = 0; i < spawnNodeID.Count; i++)
                    {
                        Vector3 spawnLocation = pathService.GetNodeLocation(spawnNodeID[i]);
                        EnemyController newEnemy = new SniperEnemyController(this, spawnLocation, _enemyScriptableObject);
                        enemyList.Add(newEnemy);
                    }
                    break;

                case EnemyType.TARGET:
                    spawnNodeID.Clear();
                    spawnNodeID = pathService.GetEnemySpawnLocation(EnemyType.STATIC);
                    for (int i = 0; i < spawnNodeID.Count; i++)
                    {
                        Vector3 spawnLocation = pathService.GetNodeLocation(spawnNodeID[i]);
                        EnemyController newEnemy = new TargetEnemyController(this, spawnLocation, _enemyScriptableObject);
                        enemyList.Add(newEnemy);
                    }
                    break;

=======
                    spawnNodeID=pathService.GetEnemySpawnLocation(EnemyType.STATIC);
                    for (int i = 0; i < spawnNodeID.Count; i++)
                    {
                        Vector3 spawnLocation = pathService.GetNodeLocation(spawnNodeID[i]);
                        EnemyController newEnemy = new StaticEnemyController(this,spawnLocation,_enemyScriptableObject);
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
>>>>>>> fce40dc3f5bda65f2644f01f554157912183462a
                default:
                    Debug.Log("No Enemy Controller of this type");
                    break;

                    
            }
        }
    }
}