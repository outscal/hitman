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
        [SerializeField]private LineRenderer lineRenderer;
        

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
            lineRenderer.SetPosition(0, this.transform.localPosition);
        }
     
        private void PerformSniperRaycast()
        {              

            if (Physics.Raycast(ray, out raycastHit, 100f))
            {
                   lineRenderer.SetPosition(1,raycastHit.point);
                if (raycastHit.collider.GetComponent<IPlayerView>() != null)
                {                   
                    enemyController.KillPlayer();
                }
            }
        }
    }
}