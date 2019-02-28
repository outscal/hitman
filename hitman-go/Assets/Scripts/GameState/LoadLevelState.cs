using Common;
using PathSystem;
using UnityEngine;
using Zenject;
namespace GameState
{
    public class LoadLevelState : IGameStates
    {
        SignalBus signalBus;
        ScriptableGraph Graph;
        IPathService pathService;
        public LoadLevelState(SignalBus signalBus, ScriptableGraph Graph, IPathService pathService)
        {
            this.signalBus = signalBus;
            this.Graph = Graph;
            this.pathService = pathService;
        }
        public GameStatesType GetStatesType()
        {
            return GameStatesType.LOADLEVELSTATE;
        }
        public void OnStateEnter()
        {
            signalBus.TryFire(new NewLevelLoadedSignal()); 
            pathService.DestroyPath();
            pathService.DrawGraph(Graph);
            signalBus.TryFire(new StateChangeSignal() { newGameState = GameStatesType.PLAYERSTATE });
        }
        public void OnStateExit()
        {
            signalBus.TryFire(new GameStartSignal());
        }
    }
}