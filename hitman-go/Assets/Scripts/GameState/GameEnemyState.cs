using GameState;
using Common;

namespace GameState
{
    public class GameEnemyState:IGameStates
    {
        public GameStatesType GetStatesType()
        {
            return GameStatesType.ENEMYSTATE;
        }

        public void OnStateEnter()
        {
            
        }
        public void OnStateExit()
        {
            
        }
    }
}