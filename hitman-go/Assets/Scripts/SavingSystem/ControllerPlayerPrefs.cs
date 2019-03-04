using UnityEngine;
using Common;
namespace SavingSystem
{
    public class ControllerPlayerPrefs : ISave
    {
        public bool ReadStarTypeForLevel(int level,StarTypes type)
        {
            return  PlayerPrefs.GetInt("Level"+level+"Star"+type,0)==1;
        }

        public void SaveStarTypeForLevel(int level,StarTypes type,bool completed)
        {
            int complete=0;
            if(completed){
                complete=1;
            }
            PlayerPrefs.SetInt("Level"+level+"Star"+type,complete);
        }
    }
}