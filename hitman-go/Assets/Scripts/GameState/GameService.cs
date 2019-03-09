using System.Collections.Generic;
using Common;
using Enemy;
using GameState;
using InputSystem;
using PathSystem;
using Player;
using SavingSystem;
using StarSystem;
using UnityEngine;
using Zenject;

namespace GameState
{
    public class GameService : IGameService, IInitializable
    {
        IGameStates currentGameState;
        IGameStates previousGameState;
        ISaveService saveService;
        IStarService starService;
        readonly SignalBus signalBus;
        ScriptableLevels levels;
        int currentLevel = 0, maxLevel = 0;
        IPathService pathService;

        public GameService(SignalBus signalBus, ScriptableLevels levels, IPathService pathService, ISaveService saveService, IStarService starService)
        {
            this.pathService = pathService;
            this.levels = levels;
            this.signalBus = signalBus;
            this.starService = starService;
            this.saveService = saveService;
            signalBus.Subscribe<LevelFinishedSignal>(ChangeToLevelFinishedState);
            //pathService.DrawGraph(levels.levelsList[currentLevel]);
        }
        public void SetCurrentLevel(int level){
            currentLevel=level;
            maxLevel = level;
        }
        public List<StarData> GetStarsForLevel(int level)
        {
            return new List<StarData>(levels.levelsList[level].stars);
        }
        public GameStatesType GetCurrentState()
        {
            return currentGameState.GetStatesType();
        }
        public void ChangeToPlayerState()
        {
            ChangeState(new GamePlayerState(signalBus));
        }
        public void ChangeToLobbyState(){
            ChangeState(new GameLobbyState(signalBus));
        }
        public void ChangeToGameOverState()
        {
            ChangeState(new GameOverState(signalBus, this));
        }
        public void IncrimentLevel()
        {
            if (levels.levelsList.Count > currentLevel) { currentLevel = currentLevel + 1; }
        }
        public void ChangeToLoadLevelState()
        {
            //            Debug.Log(currentLevel);
            starService.SetTotalEnemyandMaxPlayerMoves(levels.levelsList[currentLevel].noOfEnemies, levels.levelsList[currentLevel].maxPlayerMoves);
            ChangeState(new LoadLevelState(signalBus, levels.levelsList[currentLevel], pathService, this));
        }
        public void IncrimentMaxLevel()
        {
            if (levels.levelsList.Count > maxLevel) { maxLevel = maxLevel + 1; }
            saveService.SaveMaxLevel(maxLevel);
            
        }
        public void ChangeToLevelFinishedState()
        {
            ChangeState(new LevelFinishedState(signalBus, this,saveService,pathService,currentLevel,starService));
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
            //            Debug.Log("CurrentGame State is "+newGameState.GetStatesType());
        }

        public void Initialize()
        {
            ChangeToLobbyState();
            //signalBus.TryFire(new GameStartSignal());
        }

        public int GetCurrentLevel()
        {
            return currentLevel;
        }

        public int GetNumberOfLevels()
        {
            return levels.levelsList.Count;
        }
    }
}