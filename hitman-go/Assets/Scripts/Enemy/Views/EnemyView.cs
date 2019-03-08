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
            gameObject.SetActive(false);
            //Destroy(gameObject);
        }
        private void Start()
        {
            alertSprite.enabled = false;
           
        }    
      

        public GameObject GetGameObject()
        {
            return this.gameObject;
        }

        async public Task MoveToLocation(Vector3 location)
        {
            Debug.Log("Starting move"+gameObject.GetHashCode(),gameObject);
            iTween.MoveTo(gameObject, location, 0.3f);
            await new WaitForSeconds(0.3f);
            Debug.Log("end move" + gameObject.GetHashCode(),gameObject);
        }

        public void Reset()
        {
            //DisableEnemy();
            Destroy(this.gameObject);
        }

        async public virtual Task RotateEnemy(Vector3 newRotation)
        {            
            Debug.Log("start rotate" + gameObject.GetHashCode(),gameObject);
            iTween.RotateTo(this.gameObject, newRotation, 0.2f);

            await new WaitForSeconds(0.2f);
            Debug.Log("end rotate" + gameObject.GetHashCode(),gameObject);
        }

        public void SetPosition(Vector3 pos)
        {
            transform.position = pos;
        }

        async public Task RotateInOppositeDirection()
        {
            RotateEnemy(new Vector3(0, 180+Mathf.Abs(this.transform.localEulerAngles.y), 0));
             
        }
      

        public void SetCurrentController(IEnemyController controller)
        {
            enemyController = controller;
        }
      async  public virtual Task PerformRaycast()
        {
            await new WaitForEndOfFrame();
        }
        public virtual void SetRayDirection(Directions directions)
        {

        }

        public virtual void StopRaycast()
        {
           
        }
    }
}