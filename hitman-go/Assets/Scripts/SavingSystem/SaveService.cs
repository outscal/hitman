using Common;

namespace SavingSystem
{
    public class SaveService:ISaveService
    {
        ISave saveController;
        public SaveService()
        {
           saveController=new ControllerPlayerPrefs();
        }
        public void SaveStarTypeForLevel(int level,StarTypes type,bool completed){
            saveController.SaveStarTypeForLevel(level,type,completed);
        }
        public bool ReadStarTypeForLevel(int level,StarTypes type){
            return saveController.ReadStarTypeForLevel(level,type);
        }

        public void SaveMaxLevel(int level)
        {
            saveController.SaveMaxLevel(level);
        }

        public int ReadMaxLevel()
        {
           return saveController.ReadMaxLevel();
        }
    }
}