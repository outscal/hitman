using UnityEngine;
using Common;
using Zenject;
using System.Collections;

namespace Enemy
{
    public interface IEnemyService
    {
        int GetPlayerNodeID();
        void TriggerPlayerDeath();
        bool CheckForEnemyPresence(int playerNodeID);
        bool CheckForKillablePlayer();
    }
}
