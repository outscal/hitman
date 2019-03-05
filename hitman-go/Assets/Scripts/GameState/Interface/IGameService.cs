using Common;
namespace GameState
{
    public interface IGameService
    {
         GameStatesType GetCurrentState();
         void ChangeToPlayerState();
         int GetCurrentLevel();
         void ChangeToLobbyState();
         void SetCurrentLevel(int level);
         void ChangeToEnemyState();
         void ChangeToGameOverState();
         void ChangeToLoadLevelState();
         void IncrimentLevel();
    }
}