using UnityEngine;
using System.Collections;

namespace Enemy
{
    public class EnemyView : MonoBehaviour, IEnemyView
    {
        public GameObject GetGameObject()
        {
            return this.gameObject;
        }
    }
}