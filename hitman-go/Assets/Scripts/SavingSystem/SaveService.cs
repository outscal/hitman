namespace SavingSystem
{
    public class SaveService
    {
        ISave saveController;
        public SaveService()
        {
           saveController=new ControllerPlayerPrefs();
        }
        public void SaveRewardsData()
        {
            
        }
        public void SaveAchievementsData()
        {
           
        }
        public void ReadRewardData()
        {
            
        }
        public void ReadAchievementData()
        {

        }
    }
}