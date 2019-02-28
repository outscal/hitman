

using Common;
using GameState;
using Zenject;

namespace GameState
{
    
    public class GameOverState : IGameStates
    {
        SignalBus signalBus;
        public GameOverState(SignalBus signalBus){
            this.signalBus=signalBus;
        }
        public GameStatesType GetStatesType()
        {
            return GameStatesType.GAMEOVERSTATE;
        }

        public void OnStateEnter()
        {
            signalBus.TryFire(new StateChangeSignal() { newGameState = GameStatesType.LOADLEVELSTATE});
        }

        public void OnStateExit()
        {
           
        }
    }
}