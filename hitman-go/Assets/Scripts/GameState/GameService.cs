using Common;
using Enemy;
using GameState;
using InputSystem;
using PathSystem;
using Player;
using UnityEngine;
using Zenject;

namespace GameState
{
    public class GameService : IGameService, IInitializable
    {
        IGameStates currentGameState;
        IGameStates previousGameState;
        readonly SignalBus signalBus;
        ScriptableLevels levels;
        int currentLevel = 0;
        IPathService pathService;

        public GameService(SignalBus signalBus, ScriptableLevels levels, IPathService pathService)
        {
            this.pathService = pathService;
            this.levels = levels;
            this.signalBus = signalBus;
            signalBus.Subscribe<LevelFinishedSignal>(ChangeToLevelFinishedState);
            //pathService.DrawGraph(levels.levelsList[currentLevel]);
        }
        public GameStatesType GetCurrentState()
        {
            return currentGameState.GetStatesType();
        }
        public void ChangeToPlayerState()
        {
            ChangeState(new GamePlayerState(signalBus));
        }
        public void ChangeToGameOverState()
        {
            ChangeState(new GameOverState(signalBus, this));
        }
        public void ChangeToLoadLevelState()
        {

            ChangeState(new LoadLevelState(signalBus, levels.levelsList[currentLevel], pathService, this));
        }
        public void ChangeToLevelFinishedState()
        {
             if (levels.levelsList.Count > currentLevel) { currentLevel=currentLevel+1; }
            ChangeState(new LevelFinishedState(signalBus, this));
        }
        public void ChangeToEnemyState()
        {
            ChangeState(new GameEnemyState(signalBus));
        }
        public void ChangeState(IGameStates newGameState)
        {
            previousGameState = currentGameState;
            if (previousGameState != null) { previousGameState.OnStateExit(); }
            currentGameState = newGameState;
            currentGameState.OnStateEnter();
        }
        public void Initialize()
        {
            ChangeToLoadLevelState();
            //signalBus.TryFire(new GameStartSignal());
        }
    }
}