using UnityEngine;
using Common;
using Zenject;
using System.Collections;
using System.Threading.Tasks;

namespace Enemy
{
    public interface IEnemyService
    {
        int GetPlayerNodeID();
        void TriggerPlayerDeath();
     
        bool CheckForKillablePlayer();
    }
}
