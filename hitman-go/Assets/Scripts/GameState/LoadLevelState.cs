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
        GameService service;
        public LoadLevelState(SignalBus signalBus, ScriptableGraph Graph, IPathService pathService,GameService service)
        {
            this.service=service;
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
            signalBus.TryFire(new ResetSignal());
            signalBus.TryFire(new StateChangeSignal() { newGameState = GetStatesType()});
            pathService.DestroyPath();
            pathService.DrawGraph(Graph);
            service.ChangeToPlayerState();
           
        }
        public void OnStateExit()
        {
            signalBus.TryFire(new GameStartSignal());
        }
    }
}