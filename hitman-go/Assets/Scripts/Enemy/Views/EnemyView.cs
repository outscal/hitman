using System;
using Player;
using System.Collections;
using System.Threading.Tasks;
using UnityEngine;
using Common;

namespace Enemy
{
    public class EnemyView : MonoBehaviour, IEnemyView
    {
        [SerializeField]
        public SpriteRenderer alertSprite;

        protected IEnemyController enemyController;      

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
            iTween.RotateTo(gameObject, newRotation, 0.2f);
            await new WaitForSeconds(0.2f);
        }

        public void SetPosition(Vector3 pos)
        {
            transform.position = pos;
        }

        async public Task RotateInOppositeDirection()
        {
            if (this.transform.localEulerAngles.y == 0)
            {
                 RotateEnemy(new Vector3(0, 180, 0));
            }
            else
            {
                 RotateEnemy(new Vector3(0, -this.transform.localEulerAngles.y, 0));
            }
             new WaitForEndOfFrame();
        }
      

        public void SetCurrentController(IEnemyController controller)
        {
            enemyController = controller;
        }
        public virtual void PerformRaycast()
        {

        }
        public virtual void SetRayDirection(Directions directions)
        {

        }
    }
}