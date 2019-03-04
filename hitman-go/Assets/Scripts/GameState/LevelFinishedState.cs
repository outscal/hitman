using Common;
using Zenject;
namespace GameState
{
    public class LevelFinishedState : IGameStates
    {
        SignalBus signalBus;
        GameService service;
        public LevelFinishedState(SignalBus signalBus, GameService service)
        {
            this.service = service;
            this.signalBus = signalBus;
        }
        public GameStatesType GetStatesType()
        {
            return GameStatesType.LEVELFINISHEDSTATE;
        }

        public void OnStateEnter()
        {
            signalBus.TryFire(new StateChangeSignal() { newGameState = GetStatesType() });
            signalBus.TryFire(new ResetSignal());
        }
        public void OnStateExit()
        {

        }
    }
}