using GameState.Interface;
using Common;

namespace GameState
{
    public class GameEnemyState:IGameStates
    {
        public GameStatesType GetStatesType()
        {
            return GameStatesType.ENEMYSTATE;
        }

        public void OnStateEneter()
        {
            
        }
        public void OnStateExit()
        {
            
        }
    }
}