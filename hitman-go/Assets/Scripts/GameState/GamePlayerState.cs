
using Common;
using GameState;

namespace GameState
{
    public class GamePlayerState : IGameStates
    {
        public GameStatesType GetStatesType()
        {
            return GameStatesType.PLAYERSTATE;
        }

        public void OnStateEnter()
        {
            
        }
        public void OnStateExit()
        {
            
        }
    }
}