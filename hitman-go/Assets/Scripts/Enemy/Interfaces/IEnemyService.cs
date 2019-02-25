using UnityEngine;
using Common;
using Zenject;
using System.Collections;

namespace Enemy
{
    public interface IEnemyService
    {
        void MoveToNode(Node node);
        void SpawnEnemy(Node node,EnemyScriptableObject enemyScriptableObject);

    }
}
