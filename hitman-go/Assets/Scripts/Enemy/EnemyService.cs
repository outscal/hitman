using Common;
using GameState.Interface;
using GameState.Signals;
using PathSystem;
using Player;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using Zenject;

namespace Enemy
{
    public class EnemyService : IEnemyService
    {
        readonly SignalBus signalBus;
        private Dictionary<int, EnemyController> enemyList = new Dictionary<int, EnemyController>();
        private IPathService pathService;
        private IPlayerService playerService;
        private IGameService gameService;
    


        public EnemyService(IPathService _pathService, EnemyScriptableObjectList enemyList, SignalBus _signalBus, IGameService _gameService)
        {
            pathService = _pathService;
            gameService = _gameService;
            signalBus = _signalBus;
            SpawnEnemy(enemyList);
            signalBus.Subscribe<EnemyDeathSignal>(EnemyDead);
        }

        public void SetPlayerService(IPlayerService _playerService)
        {
            playerService = _playerService;
        }

        public bool CheckForEnemyPresence(int nodeID)
        {
            if (enemyList.ContainsKey(nodeID))
            {

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
            List<int> enemyKey = new List<int>();
            foreach (int key in enemyList.Keys)
            {
                enemyKey.Add(key);
            }

            for (int i = 0; i < enemyKey.Count; i++)
            {
                EnemyController controller;

                enemyList.TryGetValue(enemyKey[i], out controller);

                controller.Move();
            }
            if (!playerService.PlayerDeathStatus())
                signalBus.TryFire(new StateChangeSignal());
        }
        public void EnemyDead(EnemyDeathSignal _deathSignal)
        {
            Debug.Log("disable enemy");
            Debug.Log("node value" + _deathSignal.nodeID.ToString());

            EnemyController enemy;
            //enemyList.ElementAt(_deathSignal.nodeID).Value;
            enemyList.TryGetValue(_deathSignal.nodeID, out enemy);
            enemy.DisableEnemy();
            enemyList.Remove(_deathSignal.nodeID);
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
                    //Debug.Log(spawnNodeID.Count);
                    for (int i = 0; i < spawnNodeID.Count; i++)
                    {
                        Vector3 spawnLocation = pathService.GetNodeLocation(spawnNodeID[i]);
                        EnemyController newEnemy = new StaticEnemyController(this, pathService, gameService, spawnLocation, _enemyScriptableObject, spawnNodeID[i], pathService.GetEnemySpawnDirection(spawnNodeID[i]));
                        //Debug.Log("spawn id node :" + spawnNodeID[i]);
                        enemyList.Add(spawnNodeID[i], newEnemy);
                    }
                    break;

                case EnemyType.PATROLLING:
                    spawnNodeID.Clear();
                    spawnNodeID = pathService.GetEnemySpawnLocation(EnemyType.STATIC);
                    for (int i = 0; i < spawnNodeID.Count; i++)
                    {
                        Vector3 spawnLocation = pathService.GetNodeLocation(spawnNodeID[i]);
                        EnemyController newEnemy = new PatrollingEnemyController(this, pathService, gameService, spawnLocation, _enemyScriptableObject, spawnNodeID[i], pathService.GetEnemySpawnDirection(spawnNodeID[i]));
                        enemyList.Add(spawnNodeID[i], newEnemy);
                    }
                    break;

                case EnemyType.ROTATING_KNIFE:
                    spawnNodeID.Clear();
                    spawnNodeID = pathService.GetEnemySpawnLocation(EnemyType.STATIC);
                    for (int i = 0; i < spawnNodeID.Count; i++)
                    {
                        Vector3 spawnLocation = pathService.GetNodeLocation(spawnNodeID[i]);
                        EnemyController newEnemy = new RotatingKnifeEnemyController(this, pathService, gameService, spawnLocation, _enemyScriptableObject, spawnNodeID[i], pathService.GetEnemySpawnDirection(spawnNodeID[i]));
                        enemyList.Add(spawnNodeID[i], newEnemy);
                    }
                    break;

                case EnemyType.CIRCULAR_COP:
                    spawnNodeID.Clear();
                    spawnNodeID = pathService.GetEnemySpawnLocation(EnemyType.STATIC);
                    for (int i = 0; i < spawnNodeID.Count; i++)
                    {
                        Vector3 spawnLocation = pathService.GetNodeLocation(spawnNodeID[i]);
                        EnemyController newEnemy = new CircularCopEnemyController(this, pathService, gameService, spawnLocation, _enemyScriptableObject, spawnNodeID[i], pathService.GetEnemySpawnDirection(spawnNodeID[i]));
                        enemyList.Add(spawnNodeID[i], newEnemy);
                    }
                    break;

                case EnemyType.SHIELDED:
                    spawnNodeID.Clear();
                    spawnNodeID = pathService.GetEnemySpawnLocation(EnemyType.STATIC);
                    for (int i = 0; i < spawnNodeID.Count; i++)
                    {
                        Vector3 spawnLocation = pathService.GetNodeLocation(spawnNodeID[i]);
                        EnemyController newEnemy = new ShieldedEnemyController(this, pathService, gameService, spawnLocation, _enemyScriptableObject, spawnNodeID[i], pathService.GetEnemySpawnDirection(spawnNodeID[i]));
                        enemyList.Add(spawnNodeID[i], newEnemy);
                    }
                    break;

                case EnemyType.DOGS:
                    spawnNodeID.Clear();
                    spawnNodeID = pathService.GetEnemySpawnLocation(EnemyType.STATIC);
                    for (int i = 0; i < spawnNodeID.Count; i++)
                    {
                        Vector3 spawnLocation = pathService.GetNodeLocation(spawnNodeID[i]);
                        EnemyController newEnemy = new DogsEnemyController(this, pathService, gameService, spawnLocation, _enemyScriptableObject, spawnNodeID[i], pathService.GetEnemySpawnDirection(spawnNodeID[i]));
                        enemyList.Add(spawnNodeID[i], newEnemy);
                    }
                    break;

                case EnemyType.SNIPER:
                    spawnNodeID.Clear();
                    spawnNodeID = pathService.GetEnemySpawnLocation(EnemyType.STATIC);
                    for (int i = 0; i < spawnNodeID.Count; i++)
                    {
                        Vector3 spawnLocation = pathService.GetNodeLocation(spawnNodeID[i]);
                        EnemyController newEnemy = new SniperEnemyController(this, pathService, gameService, spawnLocation, _enemyScriptableObject, spawnNodeID[i], pathService.GetEnemySpawnDirection(spawnNodeID[i]));
                        enemyList.Add(spawnNodeID[i], newEnemy);
                    }
                    break;

                case EnemyType.TARGET:
                    spawnNodeID.Clear();
                    spawnNodeID = pathService.GetEnemySpawnLocation(EnemyType.STATIC);
                    for (int i = 0; i < spawnNodeID.Count; i++)
                    {
                        Vector3 spawnLocation = pathService.GetNodeLocation(spawnNodeID[i]);
                        EnemyController newEnemy = new TargetEnemyController(this, pathService, gameService, spawnLocation, _enemyScriptableObject, spawnNodeID[i], pathService.GetEnemySpawnDirection(spawnNodeID[i]));
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