using UnityEngine;
using Common;
using Zenject;
using System.Collections;
using Player;

namespace Enemy
{
    public interface IEnemyService
    {

        void SpawnEnemy(EnemyScriptableObjectList enemyScriptableObject);
        void PerformMovement();
        int GetPlayerNodeID();
        void TriggerPlayerDeath();
        void SetPlayerService(IPlayerService _playerService);
        bool CheckForEnemyPresence(int playerNodeID);
    }
}
