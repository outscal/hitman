using Common;

namespace SavingSystem
{
    public interface ISave
    {
        bool ReadStarTypeForLevel(int level, StarTypes type);
        void SaveStarTypeForLevel(int level, StarTypes type, bool completed);
    }
}