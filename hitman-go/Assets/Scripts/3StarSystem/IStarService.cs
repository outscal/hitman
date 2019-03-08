using Common;
namespace StarSystem
{
    public interface IStarService
    {
        void DogsKilled();
        void AllDogsKilled();
        bool CheckForStar(StarTypes starType);
        void SetTotalEnemyandMaxPlayerMoves(int enemy, int playerMoves);
    }
}