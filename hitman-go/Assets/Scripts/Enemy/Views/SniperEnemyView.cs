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
            
            alertSprite.enabled = false;

            
            Debug.Log(gameObject.transform.forward);

            PerformFirstRaycast();

        }

        //async public override Task RotateEnemy(Vector3 newRotation)
        //{
        //  await  base.RotateEnemy(newRotation);
        //    PerformFirstRaycast();
        //}

      async  private void PerformFirstRaycast()
        {
            lineRenderer.positionCount = 2;
            lineRenderer.SetPosition(0, transform.position);
            await new WaitForEndOfFrame();
            //lineRenderer.SetPosition(1, transform.forward*500f);
            if (Physics.Raycast(transform.localPosition, gameObject.transform.forward, out raycastHit, 500f))
            {
                lineRenderer.SetPosition(1, raycastHit.point);
            }
        }

        private void FixedUpdate()
        {
            //if(isRayCastStart)
            //{                
            //}
        }
        public override void PerformRaycast()
        {         
           // isRayCastStart = true;
                PerformSniperRaycast();

        }
      async  private void PerformSniperRaycast()
        {           
            if(!lineRenderer.enabled)
            {
                lineRenderer.enabled = true;
            }
         
                lineRenderer.SetPosition(0, transform.position);
            await new WaitForSeconds(0.5f);
            if (Physics.Raycast(transform.position, transform.forward, out raycastHit, 500f))
            {
                lineRenderer.SetPosition(1, raycastHit.point);

                if (raycastHit.collider.GetComponent<IPlayerView>() != null)
                {
                  await  enemyController.KillPlayer();
                    isRayCastStart = false;
                }
            }
         
        }

        public override void StopRaycast()
        {
           // isRayCastStart = false;
            lineRenderer.enabled = false;
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
