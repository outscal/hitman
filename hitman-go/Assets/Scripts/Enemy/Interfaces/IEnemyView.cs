using UnityEngine;
using System.Collections;

namespace Enemy
{
    public interface IEnemyView
    {
        GameObject GetGameObject();
        void DisableEnemy();
        void Reset();
        void SetPosition(Vector3 pos);
        void MoveToLocation(Vector3 vector3);
        void RotateEnemy(Vector3 vector3);
        void AlertEnemyView();
        void DisableAlertView();
    }
}