
using Common;
using GameState;
using Zenject;
namespace GameState
{
    public class GamePlayerState : IGameStates
    {
        SignalBus signalBus;
        public GamePlayerState(SignalBus signalBus){
            this.signalBus=signalBus;
        }
        public GameStatesType GetStatesType()
        {
            return GameStatesType.PLAYERSTATE;
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