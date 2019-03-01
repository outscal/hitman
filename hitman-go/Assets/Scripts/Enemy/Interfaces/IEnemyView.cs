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
        void MoveToLocation(Vector3 pos);
        void RotateEnemy(Vector3 pos);
        void AlertEnemyView();
        void DisableAlertView();
        void LookAtNode(Vector3 pos);
    }
}