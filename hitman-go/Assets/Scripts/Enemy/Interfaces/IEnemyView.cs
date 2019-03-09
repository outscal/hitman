using UnityEngine;
using System.Threading.Tasks;
using System.Collections;
using Common;

namespace Enemy
{
    public interface IEnemyView
    {
        GameObject GetGameObject();
        void DisableEnemy();
        void Reset();
        void SetPosition(Vector3 pos);
        Task MoveToLocation(Vector3 pos);
        Task RotateEnemy(Vector3 pos);
        Task RotateInOppositeDirection();
        void AlertEnemyView();
        void DisableAlertView();
        Task PerformRaycast();
        void SetRayDirection(Directions directions);
        void SetCurrentController(IEnemyController controller);
        void StopRaycast();
        Task PlayAnimation(EnemyStates enemyStates);
    }
}