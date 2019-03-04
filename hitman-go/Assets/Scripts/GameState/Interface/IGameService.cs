using Common;
namespace GameState
{
    public interface IGameService
    {
         GameStatesType GetCurrentState();
         void ChangeToPlayerState();
         int GetCurrentLevel();
         void ChangeToEnemyState();
         void ChangeToGameOverState();
         void ChangeToLoadLevelState();
         void IncrimentLevel();
    }
}