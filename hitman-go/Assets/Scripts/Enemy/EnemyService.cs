using Common;
using GameState;
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
        private List<EnemyController> enemyList = new List<EnemyController>();
        private IPathService pathService;
        private IPlayerService playerService;
        private IGameService gameService;
        private EnemyScriptableObjectList enemyScriptableObjectList;
        public EnemyService(IPlayerService _playerService, IPathService _pathService, EnemyScriptableObjectList enemyList, SignalBus _signalBus, IGameService _gameService)
        {
            pathService = _pathService;
            gameService = _gameService;
            signalBus = _signalBus;
            playerService = _playerService;
            enemyScriptableObjectList = enemyList;
            signalBus.Subscribe<EnemyDeathSignal>(EnemyDead);
            signalBus.Subscribe<StateChangeSignal>(OnTurnStateChange);
            signalBus.Subscribe<ResetSignal>(ResetEnemy);
            signalBus.Subscribe<GameStartSignal>(OnGameStart);
            signalBus.Subscribe<SignalAlertGuards>(AlertEnemies);

        }

        private void OnGameStart()
        {
            SpawnEnemy(enemyScriptableObjectList);
        }

        public bool CheckForEnemyPresence(int nodeID)
        {
            if (nodeID == -1)
            {
                return false;
            }
            if (enemyList.Count == 0)
            {
                return false;
            }

            foreach (var enemy in enemyList)
            {
                if (enemy.GetCurrentID() == nodeID)
                {
                    Debug.Log("enemy found");
                    return true;
                }
            }
            return false;
        }

        private void ResetEnemy()
        {
            ResetEverything();
        }

        private void ResetEverything()
        {
            for (int i = 0; i < enemyList.Count; i++)
            {
                enemyList[i].Reset();
            }
            enemyList.Clear();
        }

        public int GetPlayerNodeID()
        {
            return playerService.GetPlayerNodeID();
        }
        private void OnTurnStateChange()
        {
            if (gameService.GetCurrentState() == GameStatesType.ENEMYSTATE)
            {
                PerformMovement();
            }
        }

        private void PerformMovement()
        {
            if (enemyList.Count == 0)
            {
                if (!playerService.PlayerDeathStatus())
                {
                    signalBus.TryFire(new StateChangeSignal() { newGameState = GameStatesType.PLAYERSTATE });
                }
                return;
            }
            EnemyController controller;
            for (int i = 0; i < enemyList.Count; i++)
            {

                controller = enemyList[i];
                if (!playerService.PlayerDeathStatus())
                {
                    if (CheckForEnemyPresence(playerService.GetPlayerNodeID()))
                    {
                        signalBus.TryFire(new EnemyDeathSignal() { nodeID = playerService.GetPlayerNodeID() });
                    }
                    else { controller.Move(); }
                }
            }
            if (!playerService.PlayerDeathStatus())
            {
                signalBus.TryFire(new StateChangeSignal() { newGameState = GameStatesType.PLAYERSTATE });
            }
        }

        public void EnemyDead(EnemyDeathSignal _deathSignal)
        {
            Debug.Log(_deathSignal.nodeID);
            Debug.Log("enemy death");
            foreach (EnemyController enemyController in enemyList)
            {
                if (enemyController.GetCurrentID() == _deathSignal.nodeID)
                {
                    enemyController.DisableEnemy();
                    enemyList.Remove(enemyController);
                    break;
                }
            }

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
                        EnemyController newEnemy = new StaticEnemyController(this, pathService, gameService, spawnLocation, _enemyScriptableObject, spawnNodeID[i], pathService.GetEnemySpawnDirection(spawnNodeID[i]));
                        enemyList.Add(newEnemy);
                    }
                    break;

                case EnemyType.PATROLLING:
                    spawnNodeID.Clear();
                    spawnNodeID = pathService.GetEnemySpawnLocation(EnemyType.PATROLLING);
                    for (int i = 0; i < spawnNodeID.Count; i++)
                    {
                        Debug.Log(spawnNodeID[i]);

                        Vector3 spawnLocation = pathService.GetNodeLocation(spawnNodeID[i]);
                        //Debug.Log(spawnLocation);
                        EnemyController newEnemy = new PatrollingEnemyController(this, pathService, gameService, spawnLocation, _enemyScriptableObject, spawnNodeID[i], pathService.GetEnemySpawnDirection(spawnNodeID[i]));
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
                        EnemyController newEnemy = new RotatingKnifeEnemyController(this, pathService, gameService, spawnLocation, _enemyScriptableObject, spawnNodeID[i], pathService.GetEnemySpawnDirection(spawnNodeID[i]));
                        enemyList.Add(newEnemy);
                    }
                    break;

                case EnemyType.CIRCULAR_COP:
                    spawnNodeID.Clear();
                    spawnNodeID = pathService.GetEnemySpawnLocation(EnemyType.CIRCULAR_COP);
                    for (int i = 0; i < spawnNodeID.Count; i++)
                    {
                        Vector3 spawnLocation = pathService.GetNodeLocation(spawnNodeID[i]);
                        EnemyController newEnemy = new CircularCopEnemyController(this, pathService, gameService, spawnLocation, _enemyScriptableObject, spawnNodeID[i], pathService.GetEnemySpawnDirection(spawnNodeID[i]));
                        enemyList.Add(newEnemy);
                    }
                    break;

                case EnemyType.SHIELDED:
                    spawnNodeID.Clear();
                    spawnNodeID = pathService.GetEnemySpawnLocation(EnemyType.SHIELDED);
                    for (int i = 0; i < spawnNodeID.Count; i++)
                    {
                        Vector3 spawnLocation = pathService.GetNodeLocation(spawnNodeID[i]);
                        EnemyController newEnemy = new ShieldedEnemyController(this, pathService, gameService, spawnLocation, _enemyScriptableObject, spawnNodeID[i], pathService.GetEnemySpawnDirection(spawnNodeID[i]));
                        enemyList.Add(newEnemy);
                    }
                    break;

                case EnemyType.DOGS:
                    spawnNodeID.Clear();
                    spawnNodeID = pathService.GetEnemySpawnLocation(EnemyType.DOGS);
                    for (int i = 0; i < spawnNodeID.Count; i++)
                    {
                        Vector3 spawnLocation = pathService.GetNodeLocation(spawnNodeID[i]);
                        EnemyController newEnemy = new DogsEnemyController(this, pathService, gameService, spawnLocation, _enemyScriptableObject, spawnNodeID[i], pathService.GetEnemySpawnDirection(spawnNodeID[i]));
                        enemyList.Add(newEnemy);
                    }
                    break;

                case EnemyType.SNIPER:
                    spawnNodeID.Clear();
                    spawnNodeID = pathService.GetEnemySpawnLocation(EnemyType.SNIPER);
                    for (int i = 0; i < spawnNodeID.Count; i++)
                    {
                        Vector3 spawnLocation = pathService.GetNodeLocation(spawnNodeID[i]);
                        EnemyController newEnemy = new SniperEnemyController(this, pathService, gameService, spawnLocation, _enemyScriptableObject, spawnNodeID[i], pathService.GetEnemySpawnDirection(spawnNodeID[i]));
                        enemyList.Add(newEnemy);
                    }
                    break;

                case EnemyType.TARGET:
                    spawnNodeID.Clear();
                    spawnNodeID = pathService.GetEnemySpawnLocation(EnemyType.TARGET);
                    for (int i = 0; i < spawnNodeID.Count; i++)
                    {
                        Vector3 spawnLocation = pathService.GetNodeLocation(spawnNodeID[i]);
                        EnemyController newEnemy = new TargetEnemyController(this, pathService, gameService, spawnLocation, _enemyScriptableObject, spawnNodeID[i], pathService.GetEnemySpawnDirection(spawnNodeID[i]));
                        enemyList.Add(newEnemy);
                    }
                    break;

                default:
                    Debug.Log("No Enemy Controller of this type");
                    break;

            }
        }

        public bool CheckForKillablePlayer()
        {
            return playerService.CheckForKillablePlayer();
        }

        private void AlertEnemies(SignalAlertGuards _signalAlertGuards)
        {
            List<int> alertedNodes = new List<int>();
            Debug.Log("node id throw" + _signalAlertGuards.nodeID);
            alertedNodes = pathService.GetAlertedNodes(_signalAlertGuards.nodeID);
            foreach (var item in alertedNodes)
            {
                Debug.Log(item);
            }

            for (int i = 0; i < alertedNodes.Count; i++)
            {
                for (int j = 0; j < enemyList.Count; j++)
                {

                    switch (_signalAlertGuards.interactablePickup)
                    {
                        case InteractablePickup.BONE:
                            if (enemyList[j].GetEnemyType() == EnemyType.DOGS)
                            {
                                if (enemyList[j].GetCurrentID() == alertedNodes[i])
                                {
                                    enemyList[j].AlertEnemy(_signalAlertGuards.nodeID);
                                }
                            }
                            break;
                        case InteractablePickup.STONE:
                            if (enemyList[j].GetEnemyType() != EnemyType.DOGS)
                            {
                                if (enemyList[j].GetCurrentID() == alertedNodes[i])
                                {
                                    enemyList[j].AlertEnemy(_signalAlertGuards.nodeID);
                                }
                            }
                            break;
                    }

                }
            }
        }
    }
}