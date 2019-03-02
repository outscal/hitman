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
        public void OnStateEnter()
        {
            signalBus.TryFire(new StateChangeSignal() { newGameState = GetStatesType()});
            signalBus.TryFire(new ResetSignal());
            service.ChangeToLoadLevelState();
        }

        public void OnStateExit()
        {
           
        }
    }
}