using Common;

namespace SavingSystem
{
    public interface ISave
    {
        bool ReadStarTypeForLevel(int level, StarTypes type);
        void SaveMaxLevel(int level);
        int ReadMaxLevel();
        void SaveStarTypeForLevel(int level, StarTypes type, bool completed);
    }
}