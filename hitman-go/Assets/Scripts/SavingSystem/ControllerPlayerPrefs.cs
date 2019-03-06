using UnityEngine;
using Common;
namespace SavingSystem
{
    public class ControllerPlayerPrefs : ISave
    {
        public int ReadMaxLevel()
        {
            return PlayerPrefs.GetInt("MaxLevel",0);
        }

        public bool ReadStarTypeForLevel(int level, StarTypes type)
        {
            Debug.Log("reading"+level + type+(PlayerPrefs.GetInt("Level" + level + "Star" + type, 0) == 1));
            return PlayerPrefs.GetInt("Level" + level + "Star" + type, 0) == 1;
        }
        public void SaveMaxLevel(int level)
        {
            PlayerPrefs.SetInt("MaxLevel", level);
        }
        public void SaveStarTypeForLevel(int level,StarTypes type,bool completed)
        {
            int complete=0;
            if(completed){
                complete=1;
            }
            Debug.Log("saving"+level+type+complete);
            PlayerPrefs.SetInt("Level"+level+"Star"+type,complete);
        }
    }
}