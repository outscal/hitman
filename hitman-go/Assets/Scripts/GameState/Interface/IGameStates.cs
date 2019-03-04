using Common;
namespace GameState.Interface
{
    public interface IGameStates
    {
         GameStatesType GetStatesType();
         void OnStateEneter();
         void OnStateExit();
    }
}