using System;
using Player;
using System.Collections;
using System.Threading.Tasks;
using UnityEngine;

namespace Enemy
{
    public class EnemyView : MonoBehaviour, IEnemyView
    {
        [SerializeField]
        private SpriteRenderer alertSprite;

        private IEnemyController enemyController;
        private bool isRayCastStart = false;
        private Ray ray;
        private RaycastHit raycastHit;

        public void AlertEnemyView()
        {
            alertSprite.enabled = true;

        }
        public void DisableAlertView()
        {
            alertSprite.enabled = false;
        }
        public void DisableEnemy()
        {
            //gameObject.SetActive(false);
            Destroy(gameObject);
        }
        private void Start()
        {
            alertSprite.enabled = false;
            ray.direction = this.transform.forward;
        }
        private void FixedUpdate()
        {
            if (isRayCastStart)
            {
                PerformSniperRaycast();
            }
        }

        private void PerformSniperRaycast()
        {
            if (Physics.Raycast(ray, out raycastHit, 100f))
            {
                if(raycastHit.collider.GetComponent<IPlayerView>()!=null)
                {
                    enemyController.KillPlayer();
                }
            }
        }

        public GameObject GetGameObject()
        {
            return this.gameObject;
        }

        async public void MoveToLocation(Vector3 location)
        {
            iTween.MoveTo(gameObject, location, 0.5f);
            await new WaitForSeconds(0.5f);
        }

        public void Reset()
        {
            Destroy(this.gameObject);
        }

        async public Task RotateEnemy(Vector3 newRotation)
        {
            iTween.RotateTo(gameObject, newRotation, 0.5f);
            await new WaitForSeconds(0.5f);
        }

        public void SetPosition(Vector3 pos)
        {
            transform.position = pos;
        }

        async public Task RotateInOppositeDirection()
        {
            if (this.transform.localEulerAngles.y == 0)
            {
                await RotateEnemy(new Vector3(0, 180, 0));
            }
            else
            {
                await RotateEnemy(new Vector3(0, -this.transform.localEulerAngles.y, 0));
            }
        }

        public void PerformRaycast()
        {
            isRayCastStart = true;
        }

        public void SetCurrentController(IEnemyController controller)
        {
            enemyController = controller;
        }
    }
}