using UnityEngine;
using System.Collections.Generic;

namespace Enemy
{
    public interface IEnemyFactory
    {
        List<IEnemyController> SpawnEnemies(EnemyScriptableObjectList _list);
        
    }
}