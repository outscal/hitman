using Common;
using Player;
using System.Collections;
using System.Threading.Tasks;
using UnityEngine;

namespace Enemy
{
    public class SniperEnemyView : EnemyView
    {
        private bool isRayCastStart = false;
        private Ray ray;
        private RaycastHit raycastHit;

        [SerializeField] private LineRenderer lineRenderer;


        void Start()
        {


            SetRayDirection(this.enemyController.GetDirection());
            alertSprite.enabled = false;

            lineRenderer.positionCount = 2;
            lineRenderer.SetPosition(0, ray.direction);

        }
        public override void SetRayDirection(Directions directions)
        {
            switch (directions)
            {
                case Directions.RIGHT:
                    ray.direction = this.transform.right;
                    break;
                case Directions.UP:
                    ray.direction = this.transform.forward;
                    break;
                case Directions.LEFT:
                    ray.direction = -this.transform.right;
                    break;
                case Directions.DOWN:
                    ray.direction = -this.transform.forward;
                    break;
            }
        }

        private void Update()
        {


            //if (isRayCastStart)
            // { PerformSniperRaycast(); }

        }

        public override void PerformRaycast()
        {
            //  isRayCastStart = true;
            PerformSniperRaycast();

        }
        private void PerformSniperRaycast()
        {
            lineRenderer.SetPosition(1, ray.direction * 500f);

            if (Physics.Raycast(ray.origin, ray.direction, out raycastHit, Mathf.Infinity))
            {
                lineRenderer.SetPosition(1, raycastHit.point);
                if (raycastHit.collider.GetComponent<IPlayerView>() != null)
                {
                    enemyController.KillPlayer();
                }
            }
        }

#if UNITY_EDITOR
        private void OnDrawGizmos()
        {
            Gizmos.color = Color.red;
            //up
            Gizmos.DrawLine(transform.position, transform.position + Vector3.back * 6f);
            //down
            Gizmos.DrawLine(transform.position, transform.position + Vector3.forward * 6f);
            //right
            Gizmos.DrawLine(transform.position, transform.position + Vector3.right * 6f);
            //left
            Gizmos.DrawLine(transform.position, transform.position + Vector3.left * 6f);
        }
#endif
    }
}
