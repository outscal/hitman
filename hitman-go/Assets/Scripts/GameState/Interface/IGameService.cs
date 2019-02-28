using Common;
namespace GameState
{
    public interface IGameService
    {
         GameStatesType GetCurrentState();
    }
}