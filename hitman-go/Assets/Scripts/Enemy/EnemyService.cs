using Common;
using GameState;
using PathSystem;
using Player;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using StarSystem;
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
        private IStarService starService;
        private IEnemyFactory enemyFactory;
        private IGameService gameService;
        private EnemyScriptableObjectList enemyScriptableObjectList;
        private IEnumerable<Task> moveTasks;
        private List<Task> moveTaskList = new List<Task>();

        public EnemyService(IPlayerService _playerService, IStarService _starService ,IPathService _pathService, EnemyScriptableObjectList enemyList, SignalBus _signalBus, IGameService _gameService)
        {
            pathService = _pathService;
            gameService = _gameService;
            playerService = _playerService;
            starService = _starService;

            signalBus = _signalBus;
            enemyScriptableObjectList = enemyList;

            signalBus.Subscribe<EnemyKillSignal>(EnemyDead);
            signalBus.Subscribe<StateChangeSignal>(OnTurnStateChange);
            signalBus.Subscribe<ResetSignal>(ResetEnemy);
            signalBus.Subscribe<GameStartSignal>(OnGameStart);
            signalBus.Subscribe<SignalAlertGuards>(AlertEnemies);

        }

        private void OnGameStart()
        {
            enemyFactory = new EnemyFactory(this, pathService, gameService, signalBus);
            SpawnEnemy(enemyScriptableObjectList);

        }

        private bool CheckForEnemyPresence(IEnemyController enemyController, int nodeID)
        {
            if (nodeID == -1)
            {
                return false;
            }
            if (enemyList.Count == 0)
            {

                return false;
            }
            return enemyController.GetCurrentNodeID() == nodeID;
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

            List<IEnemyController> KillableEnemies = new List<IEnemyController>();
            List<Task> taskToKill = new List<Task>();

            Task moveTask;
            for (int i = 0; i < enemyList.Count; i++)
            {
                IEnemyController controller = enemyList[i];
                if (!playerService.PlayerDeathStatus())
                {
                    if (CheckForEnemyPresence(controller, playerService.GetPlayerNodeID()) && CheckForKillablePlayer(controller.GetEnemyType()))
                    {                       
                        KillableEnemies.Add(controller);
                        continue;
                    }
                    else
                    {
                        moveTask = controller.Move();
                        moveTaskList.Add(moveTask);
                    }
                }


            }
            await Task.WhenAll(moveTaskList.ToArray());
            moveTaskList.Clear();

            IEnemyController controllerToKill;
            for (int i = 0; i < KillableEnemies.Count; i++)
            {
                controllerToKill = KillableEnemies[i];
                KillEnemy(controllerToKill);

            }
            
            if (!playerService.PlayerDeathStatus())
            {
                
                gameService.ChangeToPlayerState();

            }
        }

        private bool CheckForKillableEnemy(IEnemyController controller, KillMode killMode)
        {
            return controller.IsKillable(killMode);
        }

        private void KillEnemy(IEnemyController controllerToKill)
        {
            if(controllerToKill.GetEnemyType()==EnemyType.DOGS)
            {
                starService.DogsKilled();
            }
            enemyList.Remove(controllerToKill);
            controllerToKill.Reset();
            signalBus.TryFire(new EnemyDeathSignal() { nodeID = controllerToKill.GetCurrentNodeID() });
        }


        async public void EnemyDead(EnemyKillSignal _killSignal)
        {
            var tempList = enemyList;

            for (int i = 0; i < tempList.Count; i++)
            {
                IEnemyController enemyController = tempList[i];
                if (enemyController.GetCurrentNodeID() == _killSignal.nodeID)
                {
                    if (CheckForKillableEnemy(enemyController, _killSignal.killMode))
                    {
                        enemyList.Remove(enemyController);
                        enemyController.Reset();
                    }

                }
            }
            bool allDogsKilled=true;
            foreach(IEnemyController enemyController in enemyList)
            {
                if(enemyController.GetEnemyType()==EnemyType.DOGS)
                {
                    allDogsKilled = false;
                }
            }
            if(allDogsKilled)
            {
                starService.AllDogsKilled();
            }
            if (enemyList.Count == 0)
            {
                starService.AllDogsKilled();
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

       public bool CheckForKillablePlayer(EnemyType enemyType)
        {
            return playerService.CheckForKillablePlayer(enemyType);
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
                                if (enemyList[i].GetCurrentNodeID() == alertedNodes[j])
                                {
                                    enemyList[i].AlertEnemy(_signalAlertGuards.nodeID);
                                }
                            }
                            break;
                        case InteractablePickup.STONE:
                            if (enemyList[i].GetEnemyType() != EnemyType.DOGS)
                            {
                                if (enemyList[i].GetCurrentNodeID() == alertedNodes[j])
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