using UnityEngine;
using System.Collections;

namespace Enemy
{
    public class EnemyView : MonoBehaviour, IEnemyView
    {
        public void DisableEnemy()
        {
            //gameObject.SetActive(false);
            Destroy(gameObject);
        }

        public GameObject GetGameObject()
        {
            return this.gameObject;
        }

        public void Reset()
        {
            Destroy(this.gameObject);
        }
    }
}