using Common;
namespace StarSystem
{
    public interface IStarService
    {
         bool CheckForStar(StarTypes starType);
         void SetTotalEnemyandMaxPlayerMoves(int enemy,int playerMoves);
    }
}