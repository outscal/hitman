using Common;
namespace GameState
{
    public interface IGameStates
    {
         GameStatesType GetStatesType();
         void OnStateEnter();
         void OnStateExit();
    }
}