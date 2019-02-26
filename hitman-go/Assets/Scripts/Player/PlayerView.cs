using UnityEngine;
using Enemy;
using System.Collections;

namespace Player
{
    public class PlayerView : MonoBehaviour, IPlayerView
    {
        public GameObject GetGameObject()
        {
            return this.gameObject;
        }

        private void OnCollisionEnter(Collision collision)
        {
            if(collision.gameObject.GetComponent<IEnemyView>()!=null)
            {
                
            }
        }
    }
}