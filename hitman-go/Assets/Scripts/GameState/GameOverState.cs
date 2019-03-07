using System;
using System.Threading.Tasks;
using Common;
using GameState;
using Zenject;

namespace GameState
{
    
    public class GameOverState : IGameStates
    {
        SignalBus signalBus;
        GameService service;
        public GameOverState(SignalBus signalBus, GameService service){
            this.signalBus=signalBus;
            this.service=service;
        }
        public GameStatesType GetStatesType()
        {
            return GameStatesType.GAMEOVERSTATE;
        }
        public async void OnStateEnter()
        {
            signalBus.TryFire(new StateChangeSignal() { newGameState = GetStatesType()});
            await Task.Delay(TimeSpan.FromSeconds(2));
            service.ChangeToLoadLevelState();
        }

        public void OnStateExit()
        {
           
        }
    }
}