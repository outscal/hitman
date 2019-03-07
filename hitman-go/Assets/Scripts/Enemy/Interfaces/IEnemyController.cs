using UnityEngine;
using Common;
using System.Threading.Tasks;
using System.Collections;

namespace Enemy
{
    public interface IEnemyController
    {
        void Reset();
        int GetCurrentNodeID();
        void DisableEnemy();
        void SetID(int _id);
        Task Move();
        EnemyType GetEnemyType();
        void AlertEnemy(int _destinationID);
        Task KillPlayer();
        void ChangeState(EnemyStates _state);
        EnemyStates GetEnemyState();
        Directions GetDirection();
        bool IsKillable(KillMode killMode);
    }
}