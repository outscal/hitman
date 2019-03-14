using GameState;
using Common;
using Zenject;

namespace GameState
{
    public class GameEnemyState:IGameStates
    {
        SignalBus signalBus;
        public GameEnemyState(SignalBus signalBus){
            this.signalBus=signalBus;
        }
        public GameStatesType GetStatesType()
        {
            return GameStatesType.ENEMYSTATE;
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