using Zenject;
using Common;

namespace GameState
{
    
    public class GameLobbyState:IGameStates
    {
        SignalBus signalBus;
        public GameLobbyState(SignalBus signalBus){
            this.signalBus=signalBus;
        }
         public GameStatesType GetStatesType()
        {
            return GameStatesType.LOBBYSTATE;
        }

        public void OnStateEnter()
        {
            signalBus.TryFire(new StateChangeSignal() { newGameState = GetStatesType()});
        }
        public void OnStateExit()
        {
            
        }
    }
}