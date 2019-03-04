using UnityEngine;
using System.Collections;
using System.Threading.Tasks;
using Player;

namespace Enemy
{
    public class SniperEnemyView : EnemyView
    {
        private bool isRayCastStart = false;
        private Ray ray;
        private RaycastHit raycastHit;
        

        public override void PerformRaycast()
        {
            isRayCastStart = true;
        }
        private void FixedUpdate()
        {
            if (isRayCastStart)
            { PerformSniperRaycast(); }
            
        }

        // Use this for initialization
        void Start()
        {
            alertSprite.enabled = false;
            ray.direction = this.transform.forward;
        }
     
        private void PerformSniperRaycast()
        {
                Debug.DrawRay(this.gameObject.transform.localPosition,this.transform.forward*100f,Color.red);

            if (Physics.Raycast(ray, out raycastHit, 100f))
            {
                if (raycastHit.collider.GetComponent<IPlayerView>() != null)
                {
                    enemyController.KillPlayer();
                }
            }
        }
    }
}