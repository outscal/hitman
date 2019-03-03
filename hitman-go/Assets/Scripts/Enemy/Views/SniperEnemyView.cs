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
            PerformSniperRaycast();
        }

        // Use this for initialization
        void Start()
        {
            alertSprite.enabled = false;
            ray.direction = this.transform.forward;
        }

        // Update is called once per frame
        void FixedUpdate()
        {
            //if(isRayCastStart)
            //{
            //    PerformSniperRaycast();
            //}
        }

        private void PerformSniperRaycast()
        {
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