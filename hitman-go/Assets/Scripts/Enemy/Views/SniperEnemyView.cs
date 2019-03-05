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
        private Ray ray = new Ray();
        RaycastHit raycastHit;

        [SerializeField] private LineRenderer lineRenderer;


        void Start()
        {

            Debug.Log(enemyController.GetDirection());
            //ray.origin = new Vector3(this.transform.position.x, 1f, this.transform.position.z);
            //ray.direction = this.transform.forward;
            alertSprite.enabled = false;

            lineRenderer.positionCount = 2;
            lineRenderer.SetPosition(0, transform.position);
            lineRenderer.SetPosition(1, transform.position);
     

            if (Physics.Raycast(transform.position, transform.forward, out raycastHit, 50f))
            {
                Debug.Log("hit regis");
                lineRenderer.SetPosition(1, raycastHit.point);
            }

        }
       

        public override void PerformRaycast()
        {
            PerformSniperRaycast();
            // isRayCastStart = true;

        }
        private void PerformSniperRaycast()
        {           
            // if(Physics.Raycast(ray.origin,ray.direction,out raycastHit,50f))
            if (Physics.Raycast(transform.position, transform.forward, out raycastHit, 50f))
            {
                Debug.Log("hit regis");
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
            Gizmos.DrawLine(transform.position, transform.position + ray.direction * 6f);
            //down
            //Gizmos.color = Color.green;
            //Gizmos.DrawLine(transform.position, transform.position + Vector3.forward * 6f);
            ////right
            //Gizmos.color = Color.black;
            //Gizmos.DrawLine(transform.position, transform.position + Vector3.right * 6f);
            ////left
            //Gizmos.color = Color.blue;
            //Gizmos.DrawLine(transform.position, transform.position + Vector3.left * 6f);
        }
#endif
    }
}
