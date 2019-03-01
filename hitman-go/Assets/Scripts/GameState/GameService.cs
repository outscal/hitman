using Common;
using Enemy;
using GameState;
using InputSystem;
using PathSystem;
using Player;
using StarSystem;
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
        IStarService starService;

        public GameService(SignalBus signalBus, ScriptableLevels levels, IPathService pathService, IStarService starService)
        {
            this.pathService = pathService;
            this.levels = levels;
            this.starService = starService;
            this.signalBus = signalBus;
            signalBus.Subscribe<StateChangeSignal>(ChangeState);
            //pathService.DrawGraph(levels.levelsList[currentLevel]);
        }
        public GameStatesType GetCurrentState()
        {
            return currentGameState.GetStatesType();
        }
        public void ChangeState(StateChangeSignal signal)
        {
            Debug.Log(signal.newGameState);
            previousGameState = currentGameState;
            if (previousGameState != null) { previousGameState.OnStateExit(); }
            switch (signal.newGameState)
            {
                case GameStatesType.PLAYERSTATE: currentGameState = new GamePlayerState(); break;
                case GameStatesType.ENEMYSTATE: currentGameState = new GameEnemyState(); break;
                case GameStatesType.GAMEOVERSTATE: currentGameState = new GameOverState(signalBus); break;
                case GameStatesType.LOADLEVELSTATE:
                    currentGameState = new LoadLevelState(signalBus, levels.levelsList[currentLevel], pathService);
                    starService.SetTotalEnemyandMaxPlayerMoves(levels.levelsList[currentLevel].noOfEnemies, levels.levelsList[currentLevel].maxPlayerMoves);
                    break;
                case GameStatesType.LEVELFINISHEDSTATE:
                    if (levels.levelsList.Count > currentLevel) { ++currentLevel; }
                    currentGameState = new LoadLevelState(signalBus, levels.levelsList[currentLevel], pathService);
                    break;
            }
            currentGameState.OnStateEnter();
        }
        public void Initialize()
        {
            ChangeState(new StateChangeSignal() { newGameState = GameStatesType.LOADLEVELSTATE });
            //signalBus.TryFire(new GameStartSignal());
        }
    }
}