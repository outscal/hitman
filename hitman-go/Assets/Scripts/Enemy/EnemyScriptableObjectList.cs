using UnityEngine;
using Common;
using System.Collections.Generic;

namespace Enemy
{
    [CreateAssetMenu(fileName = "Scriptable Enemy List", menuName = "Custom Objects/Enemy/Enemy List", order = 1)]
    public class EnemyScriptableObjectList : ScriptableObject
    {
        public List<EnemyScriptableObject> enemyList = new List<EnemyScriptableObject>();
    }
}
