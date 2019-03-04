using Common;
namespace SavingSystem
{
    public interface ISaveService
    {
         void SaveStarTypeForLevel(int level,StarTypes type,bool completed);
         bool ReadStarTypeForLevel(int level,StarTypes type);
    }
}