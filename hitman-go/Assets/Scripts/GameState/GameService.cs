using PathSystem;
using Player;
using Enemy;
using InputSystem;
using Common;
using Zenject;
using GameState.Interface;

namespace GameState
{
    public class GameService: IGameService
    {   
        IGameStates currentGameState,previousGameState;
        public  GameService(IInputService inputService,IPlayerService playerService,IEnemyService enemyService, IPathService pathService)
        {  
            //pathService.DrawGraph();
            playerService.SpawnPlayer();
        
        }
        public GameStatesType GetCurrentState()
        {
            return currentGameState.GetStatesType();
        }
        public void ChangeState(){
            
        }
    }
}