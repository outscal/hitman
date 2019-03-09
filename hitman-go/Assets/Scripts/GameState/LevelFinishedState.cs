using System.Collections.Generic;
using Common;
using PathSystem;
using SavingSystem;
using StarSystem;
using UnityEngine;
using Zenject;
namespace GameState
{
    public class LevelFinishedState : IGameStates
    {
        SignalBus signalBus;
        GameService service;
        ISaveService saveService;
        IPathService pathService;
        int currentLevel;
        IStarService starService;
        public LevelFinishedState(SignalBus signalBus, GameService service,ISaveService SaveService,IPathService pathService,int level,IStarService starService)
        {
            this.starService = starService;
            currentLevel = level;
            this.saveService = SaveService;
            this.pathService = pathService;
            this.service = service;
            this.signalBus = signalBus;
        }
        public GameStatesType GetStatesType()
        {
            return GameStatesType.LEVELFINISHEDSTATE;
        }

        public void OnStateEnter()
        {
            List<StarData> stars = pathService.GetStarsForLevel();
            for (int i = 0; i < stars.Count; i++)
            {
                Debug.Log(stars[i].type);
                saveService.SaveStarTypeForLevel(currentLevel, stars[i].type, starService.CheckForStar(stars[i].type));
            }
           service.IncrimentMaxLevel();
            signalBus.TryFire(new StateChangeSignal() { newGameState = GetStatesType() });
        }
        public void OnStateExit()
        {

        }
    }
}