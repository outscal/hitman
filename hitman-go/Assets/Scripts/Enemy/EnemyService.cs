using UnityEngine;
using System;
using System.Collections;
using System.Collections.Generic;
using GameState.Signals;
using Player;
using Zenject;
using PathSystem;
using Common;

namespace Enemy
{
    public class EnemyService : IEnemyService
    {
        readonly SignalBus signalBus;
        private Dictionary<int, EnemyController> enemyList = new Dictionary<int, EnemyController>();
        private IPathService pathService;
        private IPlayerService playerService;


        public EnemyService(IPathService _pathService, IPlayerService _playerService, EnemyScriptableObjectList enemyList, SignalBus _signalBus)
        {
            pathService = _pathService;
            playerService = _playerService;
            signalBus = _signalBus;
            SpawnEnemy(enemyList);
        }

        public bool CheckForEnemyPresence(int playerNodeID)
        {
            if (enemyList.ContainsKey(playerNodeID))
            {
                enemyList.Remove(playerNodeID);

                return true;
            }
            else
                return false;
        }

        public int GetPlayerNodeID()
        {
            return playerService.GetPlayerNodeID();
        }

        public void PerformMovement()
        {
            for (int i = 0; i < enemyList.Count; i++)
            {
                enemyList[i].Move();
            }
            signalBus.TryFire(new StateChangeSignal());
        }

        public void SpawnEnemy(EnemyScriptableObjectList scriptableObjectList)
        {
            for (int i = 0; i < scriptableObjectList.enemyList.Count; i++)
            {
                SpawnSingleEnemy(scriptableObjectList.enemyList[i]);
            }
        }

        public void TriggerPlayerDeath()
        {
            signalBus.TryFire(new PlayerDeathSignal());
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
                        EnemyController newEnemy = new StaticEnemyController(this, pathService, spawnLocation, _enemyScriptableObject, spawnNodeID[i], pathService.GetEnemySpawnDirection(spawnNodeID[i]));
                        enemyList.Add(spawnNodeID[i], newEnemy);
                    }
                    break;

                case EnemyType.PATROLLING:
                    spawnNodeID.Clear();
                    spawnNodeID = pathService.GetEnemySpawnLocation(EnemyType.STATIC);
                    for (int i = 0; i < spawnNodeID.Count; i++)
                    {
                        Vector3 spawnLocation = pathService.GetNodeLocation(spawnNodeID[i]);
                        EnemyController newEnemy = new PatrollingEnemyController(this, pathService, spawnLocation, _enemyScriptableObject, spawnNodeID[i], pathService.GetEnemySpawnDirection(spawnNodeID[i]));
                        enemyList.Add(spawnNodeID[i], newEnemy);
                    }
                    break;

                case EnemyType.ROTATING_KNIFE:
                    spawnNodeID.Clear();
                    spawnNodeID = pathService.GetEnemySpawnLocation(EnemyType.STATIC);
                    for (int i = 0; i < spawnNodeID.Count; i++)
                    {
                        Vector3 spawnLocation = pathService.GetNodeLocation(spawnNodeID[i]);
                        EnemyController newEnemy = new RotatingKnifeEnemyController(this, pathService, spawnLocation, _enemyScriptableObject, spawnNodeID[i], pathService.GetEnemySpawnDirection(spawnNodeID[i]));
                        enemyList.Add(spawnNodeID[i], newEnemy);
                    }
                    break;

                case EnemyType.CIRCULAR_COP:
                    spawnNodeID.Clear();
                    spawnNodeID = pathService.GetEnemySpawnLocation(EnemyType.STATIC);
                    for (int i = 0; i < spawnNodeID.Count; i++)
                    {
                        Vector3 spawnLocation = pathService.GetNodeLocation(spawnNodeID[i]);
                        EnemyController newEnemy = new CircularCopEnemyController(this, pathService, spawnLocation, _enemyScriptableObject, spawnNodeID[i], pathService.GetEnemySpawnDirection(spawnNodeID[i]));
                        enemyList.Add(spawnNodeID[i], newEnemy);
                    }
                    break;

                case EnemyType.SHIELDED:
                    spawnNodeID.Clear();
                    spawnNodeID = pathService.GetEnemySpawnLocation(EnemyType.STATIC);
                    for (int i = 0; i < spawnNodeID.Count; i++)
                    {
                        Vector3 spawnLocation = pathService.GetNodeLocation(spawnNodeID[i]);
                        EnemyController newEnemy = new ShieldedEnemyController(this, pathService, spawnLocation, _enemyScriptableObject, spawnNodeID[i], pathService.GetEnemySpawnDirection(spawnNodeID[i]));
                        enemyList.Add(spawnNodeID[i], newEnemy);
                    }
                    break;

                case EnemyType.DOGS:
                    spawnNodeID.Clear();
                    spawnNodeID = pathService.GetEnemySpawnLocation(EnemyType.STATIC);
                    for (int i = 0; i < spawnNodeID.Count; i++)
                    {
                        Vector3 spawnLocation = pathService.GetNodeLocation(spawnNodeID[i]);
                        EnemyController newEnemy = new DogsEnemyController(this, pathService, spawnLocation, _enemyScriptableObject, spawnNodeID[i], pathService.GetEnemySpawnDirection(spawnNodeID[i]));
                        enemyList.Add(spawnNodeID[i], newEnemy);
                    }
                    break;

                case EnemyType.SNIPER:
                    spawnNodeID.Clear();
                    spawnNodeID = pathService.GetEnemySpawnLocation(EnemyType.STATIC);
                    for (int i = 0; i < spawnNodeID.Count; i++)
                    {
                        Vector3 spawnLocation = pathService.GetNodeLocation(spawnNodeID[i]);
                        EnemyController newEnemy = new SniperEnemyController(this, pathService, spawnLocation, _enemyScriptableObject, spawnNodeID[i], pathService.GetEnemySpawnDirection(spawnNodeID[i]));
                        enemyList.Add(spawnNodeID[i], newEnemy);
                    }
                    break;

                case EnemyType.TARGET:
                    spawnNodeID.Clear();
                    spawnNodeID = pathService.GetEnemySpawnLocation(EnemyType.STATIC);
                    for (int i = 0; i < spawnNodeID.Count; i++)
                    {
                        Vector3 spawnLocation = pathService.GetNodeLocation(spawnNodeID[i]);
                        EnemyController newEnemy = new TargetEnemyController(this, pathService, spawnLocation, _enemyScriptableObject, spawnNodeID[i], pathService.GetEnemySpawnDirection(spawnNodeID[i]));
                        enemyList.Add(spawnNodeID[i], newEnemy);
                    }
                    break;

                default:
                    Debug.Log("No Enemy Controller of this type");
                    break;

            }
        }
    }
}