using UnityEngine;
using System.Collections;

namespace Enemy
{
    public class EnemyView : MonoBehaviour, IEnemyView
    {
        public void DisableEnemy()
        {
            gameObject.SetActive(false);
        }

        public GameObject GetGameObject()
        {
            return this.gameObject;
        }
    }
}