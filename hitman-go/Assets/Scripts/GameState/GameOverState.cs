

using Common;
using GameState.Interface;

namespace GameState
{
    public class GameOverState : IGameStates
    {
        public GameStatesType GetStatesType()
        {
            return GameStatesType.GAMEOVERSTATE;
        }

        public void OnStateEneter()
        {
           
        }

        public void OnStateExit()
        {
           
        }
    }
}