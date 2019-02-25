using UnityEngine;
using Common;
using System.Collections;

namespace Enemy
{
    [CreateAssetMenu(fileName = "Scriptable Enemy Attributes", menuName = "Custom Objects/Enemy/EnemyAttributes", order = 0)]
    public class EnemyScriptableObject : ScriptableObject
    {
        public EnemyType enemyType;
       // public EnemyView enemyPrefab;      
    }
}