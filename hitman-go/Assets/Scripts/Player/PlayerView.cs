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
<<<<<<< HEAD

        public void MoveToLocation(Vector3 _location)
        {
            this.transform.LookAt(_location);
            this.transform.localPosition = Vector3.Lerp(this.transform.localPosition, _location, 1f);
        }
=======
        
>>>>>>> fce40dc3f5bda65f2644f01f554157912183462a

        private void OnCollisionEnter(Collision collision)
        {
            if(collision.gameObject.GetComponent<IEnemyView>()!=null)
            {
                
            }
        }
    }
}