using UnityEngine;
namespace SavingSystem
{
    public class ControllerPlayerPrefs : ISave
    {
        public void ReadAchievementData()
        {

            PlayerPrefs.GetInt("progress", 0);

        }

        public void ReadRewardData()
        {


        }

        public void SaveAchievementsData()
        {

        }
        public void SaveRewardsData()
        {

        }
    }
}