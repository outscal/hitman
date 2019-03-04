using GameState;
using Common;
using PathSystem;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

namespace Enemy
{
    public class EnemyFactory : IEnemyFactory
    {
        IEnemyService enemyService;
        IPathService pathService;
        IGameService gameService;

        public EnemyFactory(IEnemyService _enemyService, IPathService _pathService,IGameService _gameService)
        {
            gameService = _gameService;
            pathService = _pathService;
            enemyService = _enemyService;
            
        }

       

        public List<IEnemyController> SpawnEnemies(EnemyScriptableObjectList _list)
        {
            List<IEnemyController> enemyList = new List<IEnemyController>();
        
            for (int i = 0; i < _list.enemyList.Count; i++)
            {
              enemyList= enemyList.Concat(SpawnSingleEnemyLocations(_list.enemyList[i])).ToList();
            }
            Debug.Log("enemy list [Factory ]" + enemyList.Count) ;
            return enemyList;
        }


        private List<IEnemyController> SpawnSingleEnemyLocations(EnemyScriptableObject _enemyScriptableObject)
        {
            List<int> spawnNodeID = new List<int>();
            List<IEnemyController> newEnemyControllers = new List<IEnemyController>();
            spawnNodeID.Clear();
            spawnNodeID = pathService.GetEnemySpawnLocation(_enemyScriptableObject.enemyType);
            switch (_enemyScriptableObject.enemyType)
            {
                case EnemyType.STATIC:
                    for (int i = 0; i < spawnNodeID.Count; i++)
                    {
                        Vector3 spawnLocation = pathService.GetNodeLocation(spawnNodeID[i]);
                        IEnemyController newEnemy = new StaticEnemyController(enemyService, pathService, gameService, spawnLocation, _enemyScriptableObject, spawnNodeID[i], pathService.GetEnemySpawnDirection(spawnNodeID[i]));
                        newEnemyControllers.Add(newEnemy);
                        
                    }
                    break;

                case EnemyType.PATROLLING:
                   
                    for (int i = 0; i < spawnNodeID.Count; i++)
                    {
                        

                        Vector3 spawnLocation = pathService.GetNodeLocation(spawnNodeID[i]);                       
                        IEnemyController newEnemy = new PatrollingEnemyController(enemyService, pathService, gameService, spawnLocation, _enemyScriptableObject, spawnNodeID[i], pathService.GetEnemySpawnDirection(spawnNodeID[i]));
                        newEnemyControllers.Add(newEnemy);
                    }
                    break;

                case EnemyType.ROTATING_KNIFE:
                    for (int i = 0; i < spawnNodeID.Count; i++)
                    {
                        Vector3 spawnLocation = pathService.GetNodeLocation(spawnNodeID[i]);
                        IEnemyController newEnemy = new RotatingKnifeEnemyController(enemyService, pathService, gameService, spawnLocation, _enemyScriptableObject, spawnNodeID[i], pathService.GetEnemySpawnDirection(spawnNodeID[i]));
                        newEnemyControllers.Add(newEnemy);
                    }
                    break;

                case EnemyType.CIRCULAR_COP:
                    
                    for (int i = 0; i < spawnNodeID.Count; i++)
                    {
                        Vector3 spawnLocation = pathService.GetNodeLocation(spawnNodeID[i]);
                        IEnemyController newEnemy = new CircularCopEnemyController(enemyService, pathService, gameService, spawnLocation, _enemyScriptableObject, spawnNodeID[i], pathService.GetEnemySpawnDirection(spawnNodeID[i]));
                        newEnemyControllers.Add(newEnemy);

                    }
                    break;

                case EnemyType.SHIELDED:                   
                    for (int i = 0; i < spawnNodeID.Count; i++)
                    {
                        Vector3 spawnLocation = pathService.GetNodeLocation(spawnNodeID[i]);
                        IEnemyController newEnemy = new ShieldedEnemyController(enemyService, pathService, gameService, spawnLocation, _enemyScriptableObject, spawnNodeID[i], pathService.GetEnemySpawnDirection(spawnNodeID[i]));

                        newEnemyControllers.Add(newEnemy);
                    }
                    break;

                case EnemyType.DOGS:                   
                    for (int i = 0; i < spawnNodeID.Count; i++)
                    {
                        Vector3 spawnLocation = pathService.GetNodeLocation(spawnNodeID[i]);
                        IEnemyController newEnemy = new DogsEnemyController(enemyService, pathService, gameService, spawnLocation, _enemyScriptableObject, spawnNodeID[i], pathService.GetEnemySpawnDirection(spawnNodeID[i]));
                        newEnemyControllers.Add(newEnemy);

                    }
                    break;

                case EnemyType.SNIPER:                   
                    for (int i = 0; i < spawnNodeID.Count; i++)
                    {
                        Vector3 spawnLocation = pathService.GetNodeLocation(spawnNodeID[i]);
                        IEnemyController newEnemy = new SniperEnemyController(enemyService, pathService, gameService, spawnLocation, _enemyScriptableObject, spawnNodeID[i], pathService.GetEnemySpawnDirection(spawnNodeID[i]));
                        newEnemyControllers.Add(newEnemy);

                    }
                    break;

                case EnemyType.TARGET:                   
                    for (int i = 0; i < spawnNodeID.Count; i++)
                    {
                        Vector3 spawnLocation = pathService.GetNodeLocation(spawnNodeID[i]);
                        IEnemyController newEnemy = new TargetEnemyController(enemyService, pathService, gameService, spawnLocation, _enemyScriptableObject, spawnNodeID[i], pathService.GetEnemySpawnDirection(spawnNodeID[i]));
                        newEnemyControllers.Add(newEnemy);

                    }
                    break;

                default:
                    Debug.Log("No Enemy Controller of this type");
                    break;                    
            }
            Debug.Log("enemy list [Factory single enemy ]" + newEnemyControllers.Count);
            return newEnemyControllers;
        }
    }

}