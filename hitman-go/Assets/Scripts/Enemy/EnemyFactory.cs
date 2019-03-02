using GameState;
using Common;
using PathSystem;
using System.Collections.Generic;
using UnityEngine;

namespace Enemy
{
    public class EnemyFactory : IEnemyFactory
    {
        IEnemyService enemyService;
        IPathService pathService;
        IGameService gameService;
        List<IEnemyController> enemyList = new List<IEnemyController>();

        public EnemyFactory(IEnemyService _enemyService, IPathService _pathService,IGameService _gameService)
        {
            gameService = _gameService;
            pathService = _pathService;
            enemyService = _enemyService;
            
        }

        public List<IEnemyController> GetEnemyList()
        {
            return enemyList;
        }

        public void SpawnEnemies(EnemyScriptableObjectList _list)
        {
        
            for (int i = 0; i < _list.enemyList.Count; i++)
            {
                SpawnSingleEnemy(_list.enemyList[i]);
            }
        }


        private void SpawnSingleEnemy(EnemyScriptableObject _enemyScriptableObject)
        {
            List<int> spawnNodeID = new List<int>();

            switch (_enemyScriptableObject.enemyType)
            {
                case EnemyType.STATIC:
                    spawnNodeID.Clear();

                    spawnNodeID = pathService.GetEnemySpawnLocation(EnemyType.STATIC);

                    for (int i = 0; i < spawnNodeID.Count; i++)
                    {
                        Vector3 spawnLocation = pathService.GetNodeLocation(spawnNodeID[i]);
                        IEnemyController newEnemy = new StaticEnemyController(enemyService, pathService, gameService, spawnLocation, _enemyScriptableObject, spawnNodeID[i], pathService.GetEnemySpawnDirection(spawnNodeID[i]));
                        enemyList.Add(newEnemy);
                    }
                    break;

                case EnemyType.PATROLLING:
                    spawnNodeID.Clear();
                    spawnNodeID = pathService.GetEnemySpawnLocation(EnemyType.PATROLLING);
                    for (int i = 0; i < spawnNodeID.Count; i++)
                    {
                        

                        Vector3 spawnLocation = pathService.GetNodeLocation(spawnNodeID[i]);                       
                        IEnemyController newEnemy = new PatrollingEnemyController(enemyService, pathService, gameService, spawnLocation, _enemyScriptableObject, spawnNodeID[i], pathService.GetEnemySpawnDirection(spawnNodeID[i]));
                        Debug.Log(spawnNodeID[i]);
                        enemyList.Add(newEnemy);
                    }
                    break;

                case EnemyType.ROTATING_KNIFE:
                    spawnNodeID.Clear();
                    spawnNodeID = pathService.GetEnemySpawnLocation(EnemyType.ROTATING_KNIFE);
                    for (int i = 0; i < spawnNodeID.Count; i++)
                    {
                        Vector3 spawnLocation = pathService.GetNodeLocation(spawnNodeID[i]);
                        IEnemyController newEnemy = new RotatingKnifeEnemyController(enemyService, pathService, gameService, spawnLocation, _enemyScriptableObject, spawnNodeID[i], pathService.GetEnemySpawnDirection(spawnNodeID[i]));
                        enemyList.Add(newEnemy);
                    }
                    break;

                case EnemyType.CIRCULAR_COP:
                    spawnNodeID.Clear();
                    spawnNodeID = pathService.GetEnemySpawnLocation(EnemyType.CIRCULAR_COP);
                    for (int i = 0; i < spawnNodeID.Count; i++)
                    {
                        Vector3 spawnLocation = pathService.GetNodeLocation(spawnNodeID[i]);
                        IEnemyController newEnemy = new CircularCopEnemyController(enemyService, pathService, gameService, spawnLocation, _enemyScriptableObject, spawnNodeID[i], pathService.GetEnemySpawnDirection(spawnNodeID[i]));
                        enemyList.Add(newEnemy);
                    }
                    break;

                case EnemyType.SHIELDED:
                    spawnNodeID.Clear();
                    spawnNodeID = pathService.GetEnemySpawnLocation(EnemyType.SHIELDED);
                    for (int i = 0; i < spawnNodeID.Count; i++)
                    {
                        Vector3 spawnLocation = pathService.GetNodeLocation(spawnNodeID[i]);
                        IEnemyController newEnemy = new ShieldedEnemyController(enemyService, pathService, gameService, spawnLocation, _enemyScriptableObject, spawnNodeID[i], pathService.GetEnemySpawnDirection(spawnNodeID[i]));
                        enemyList.Add(newEnemy);
                    }
                    break;

                case EnemyType.DOGS:
                    spawnNodeID.Clear();
                    spawnNodeID = pathService.GetEnemySpawnLocation(EnemyType.DOGS);
                    for (int i = 0; i < spawnNodeID.Count; i++)
                    {
                        Vector3 spawnLocation = pathService.GetNodeLocation(spawnNodeID[i]);
                        IEnemyController newEnemy = new DogsEnemyController(enemyService, pathService, gameService, spawnLocation, _enemyScriptableObject, spawnNodeID[i], pathService.GetEnemySpawnDirection(spawnNodeID[i]));
                        enemyList.Add(newEnemy);
                    }
                    break;

                case EnemyType.SNIPER:
                    spawnNodeID.Clear();
                    spawnNodeID = pathService.GetEnemySpawnLocation(EnemyType.SNIPER);
                    for (int i = 0; i < spawnNodeID.Count; i++)
                    {
                        Vector3 spawnLocation = pathService.GetNodeLocation(spawnNodeID[i]);
                        IEnemyController newEnemy = new SniperEnemyController(enemyService, pathService, gameService, spawnLocation, _enemyScriptableObject, spawnNodeID[i], pathService.GetEnemySpawnDirection(spawnNodeID[i]));
                        enemyList.Add(newEnemy);
                    }
                    break;

                case EnemyType.TARGET:
                    spawnNodeID.Clear();
                    spawnNodeID = pathService.GetEnemySpawnLocation(EnemyType.TARGET);
                    for (int i = 0; i < spawnNodeID.Count; i++)
                    {
                        Vector3 spawnLocation = pathService.GetNodeLocation(spawnNodeID[i]);
                        IEnemyController newEnemy = new TargetEnemyController(enemyService, pathService, gameService, spawnLocation, _enemyScriptableObject, spawnNodeID[i], pathService.GetEnemySpawnDirection(spawnNodeID[i]));
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