using Common;
using GameState;
using PathSystem;
using Player;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using UnityEngine;
using Zenject;

namespace Enemy
{
    public class EnemyService : IEnemyService
    {
        readonly SignalBus signalBus;
        private List<IEnemyController> enemyList = new List<IEnemyController>();
        private IPathService pathService;
        private IPlayerService playerService;
        private IEnemyFactory enemyFactory;
        private IGameService gameService;
        private EnemyScriptableObjectList enemyScriptableObjectList;
        private IEnumerable<Task> moveTasks;
        private List<Task> moveTaskList = new List<Task>();

        public EnemyService(IPlayerService _playerService, IPathService _pathService, EnemyScriptableObjectList enemyList, SignalBus _signalBus, IGameService _gameService)
        {
            pathService = _pathService;
            gameService = _gameService;
            playerService = _playerService;

            signalBus = _signalBus;
            enemyScriptableObjectList = enemyList;

            signalBus.Subscribe<EnemyDeathSignal>(EnemyDead);
            signalBus.Subscribe<StateChangeSignal>(OnTurnStateChange);
            signalBus.Subscribe<ResetSignal>(ResetEnemy);
            signalBus.Subscribe<GameStartSignal>(OnGameStart);
            signalBus.Subscribe<SignalAlertGuards>(AlertEnemies);

        }

        private void OnGameStart()
        {
            enemyFactory = new EnemyFactory(this, pathService, gameService);
            SpawnEnemy(enemyScriptableObjectList);

        }

        private bool CheckForEnemyPresence(int nodeID)
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

        async private Task PerformMovement()
        {

            if (enemyList.Count == 0)
            {
                if (!playerService.PlayerDeathStatus())
                {
                    gameService.ChangeToPlayerState();

                }
                return;
            }

            IEnemyController controller;
            for (int i = 0; i < enemyList.Count; i++)
            {

                controller = enemyList[i];
               // controller.ChangeState(EnemyStates.MOVING);
             
                if (!playerService.PlayerDeathStatus())
                {
                    if (CheckForEnemyPresence(playerService.GetPlayerNodeID()))
                    {
                        signalBus.TryFire(new EnemyDeathSignal() { nodeID = playerService.GetPlayerNodeID() });
                    }
                    else
                    {                        
                       await controller.Move();
                    }
                }
             

            }
          // await Task.WhenAll(moveTaskList);

            if (!playerService.PlayerDeathStatus())
            {
                gameService.ChangeToPlayerState();

            }
        }

        public void EnemyDead(EnemyDeathSignal _deathSignal)
        {
            foreach (EnemyController enemyController in enemyList)
            {
                if (enemyController.GetCurrentID() == _deathSignal.nodeID)
                {
                    enemyController.DisableEnemy();
                    enemyList.Remove(enemyController);
                    break;
                }
            }
            if (enemyList.Count == 0)
            {
                gameService.ChangeToPlayerState();
            }

        }

        public void TriggerPlayerDeath()
        {
            signalBus.TryFire(new PlayerDeathSignal());
        }
        private void SpawnEnemy(EnemyScriptableObjectList enemyScriptableObjectList)
        {
            enemyList = enemyFactory.SpawnEnemies(enemyScriptableObjectList);

        }

        public bool CheckForKillablePlayer()
        {
            return playerService.CheckForKillablePlayer();
        }

        private void AlertEnemies(SignalAlertGuards _signalAlertGuards)
        {
            List<int> alertedNodes = new List<int>();
            alertedNodes = pathService.GetAlertedNodes(_signalAlertGuards.nodeID);

            for (int i = 0; i < enemyList.Count; i++)
            {
                for (int j = 0; j < alertedNodes.Count; j++)
                {

                    switch (_signalAlertGuards.interactablePickup)
                    {
                        case InteractablePickup.BONE:
                            if (enemyList[i].GetEnemyType() == EnemyType.DOGS)
                            {
                                if (enemyList[i].GetCurrentID() == alertedNodes[j])
                                {
                                    enemyList[i].AlertEnemy(_signalAlertGuards.nodeID);
                                }
                            }
                            break;
                        case InteractablePickup.STONE:
                            if (enemyList[i].GetEnemyType() != EnemyType.DOGS)
                            {
                                if (enemyList[i].GetCurrentID() == alertedNodes[j])
                                {
                                    enemyList[i].AlertEnemy(_signalAlertGuards.nodeID);
                                }
                            }
                            break;
                    }

                }
            }
        }
    }
}