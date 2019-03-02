using UnityEngine;
using Common;
using System.Threading.Tasks;
using System.Collections;

namespace Enemy
{
    public interface IEnemyController
    {
        void Reset();
        int GetCurrentID();
        void DisableEnemy();
        void SetID(int _id);
        Task Move();
        EnemyType GetEnemyType();
        void AlertEnemy(int _destinationID);
    }
}