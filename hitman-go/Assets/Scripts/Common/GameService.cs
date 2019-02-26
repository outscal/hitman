using PathSystem;
using Player;
using Enemy;
using InputSystem;

namespace Common
{
    public class GameService
    {
        Node playerSpawnNode;
        
        public  GameService(IInputService inputService,IPlayerService playerService,IEnemyService enemyService, IPathService pathService)
        {  
            //pathService.DrawGraph();
            playerService.SpawnPlayer(playerSpawnNode);
        }
    }
}