
using Common;
using GameState.Interface;

namespace GameState
{
    public class GamePlayerState : IGameStates
    {
        public GameStatesType GetStatesType()
        {
            return GameStatesType.PLAYERSTATE;
        }

        public void OnStateEneter()
        {
            
        }
        public void OnStateExit()
        {
            
        }
    }
}