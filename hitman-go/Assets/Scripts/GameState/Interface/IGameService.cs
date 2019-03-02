using Common;
namespace GameState
{
    public interface IGameService
    {
         GameStatesType GetCurrentState();
         void ChangeToPlayerState();
         void ChangeToEnemyState();
         void ChangeToGameOverState();
    }
}