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
        public GameObject alertSprite;
        public Animator animator;
        protected IEnemyController enemyController;
        //public AnimationCurve moveAnimationCurve;
        public float enemyDeathDuration;
        //public float durationOfMoveAnimation;

        public void AlertEnemyView()
        {
            alertSprite.SetActive(true);       
        }
        public void DisableAlertView()
        {
            alertSprite.SetActive(false);
        }
        public void DisableEnemy()
        {
            gameObject.SetActive(false);          
        }
        private void Start()
        {
            alertSprite.SetActive(false);           
        }       
        public GameObject GetGameObject()
        {
            return this.gameObject;
        }

        async public Task MoveToLocation(Vector3 location)
        {
            
            iTween.MoveTo(this.gameObject, location, 1f);
            await new WaitForSeconds(1f);
        }
        public void Reset()
        {
            //DisableEnemy();
            Destroy(this.gameObject);
        }

        async public virtual Task RotateEnemy(Vector3 newRotation)
        {            
            iTween.RotateTo(this.gameObject, newRotation, 0.1f);
            await new WaitForSeconds(0.1f);            
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

        //async protected Task MoveInCurve(Vector3 _location)
        //{
        //    float t = 0;
        //    while (t < durationOfMoveAnimation)
        //    {
        //        t += Time.deltaTime;
        //        this.transform.LookAt(_location);
        //        this.transform.localPosition = Vector3.Lerp(this.transform.localPosition, _location, moveAnimationCurve.Evaluate(t / durationOfMoveAnimation));
        //        await new WaitForEndOfFrame();
        //    }
        //}

        async public Task PlayAnimation(EnemyStates enemyStates)
        {
            if(enemyStates==EnemyStates.DEATH)
            {
                this.animator.Play("Death");
                await new WaitForSeconds(enemyDeathDuration);

            }
            //await new WaitForEndOfFrame();
        }
    }
}