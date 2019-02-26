using PathSystem;
using Player;
using Enemy;
using InputSystem;
using Zenject;

namespace Common
{
    public class GameService
    {
        Node playerSpawnNode;
        
        public  GameService(IInputService inputService,IPlayerService playerService,IEnemyService enemyService, IPathService pathService)
        {  
            //pathService.DrawGraph();
            playerService.SpawnPlayer();
        
        }
    }
}