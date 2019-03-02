using UnityEngine;
using System.Collections.Generic;

namespace Enemy
{
    public interface IEnemyFactory
    {
        void SpawnEnemies(EnemyScriptableObjectList _list);
        List<IEnemyController> GetEnemyList();
    }
}