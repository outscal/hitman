using UnityEngine;
using Enemy;
using System.Threading.Tasks;
using System.Collections;
using Common;

namespace Player
{
    public class PlayerView : MonoBehaviour, IPlayerView
    {
        [SerializeField]
        private Renderer _renderer;
        [SerializeField]
        private Color staticEnemy, petrolEnemy, knifeEnemy, biDirectionalEnemy, circularEnemy;
        public AnimationCurve moveAnimationCurve;
  
        public float durationOfMoveAnimation;
      

        public void DisablePlayer()
        {
            gameObject.SetActive(false);
        }
      
        
       async public Task MoveToLocation(Vector3 _location)
        {
            //this.transform.LookAt(_location);
            //// this.transform.localPosition = Vector3.Lerp(this.transform.localPosition, _location, 1f);
            //iTween.MoveTo(this.gameObject, _location, 0.2f);
            //await new WaitForSeconds(0.2f);
            await MoveInCurve(_location);
        }

        public void PlayAnimation(PlayerStates state)
        {
           // TestCurve();
        }

        public void Reset()
        {
            Destroy(gameObject);
        }

        public void SetDisguise(EnemyType disguiseType)
        {
            switch (disguiseType)
            {
                case EnemyType.STATIC:
                    _renderer.material.color = staticEnemy;
                    break;
                case EnemyType.PATROLLING:
                    _renderer.material.color = petrolEnemy;
                    break;
                case EnemyType.ROTATING_KNIFE:
                    _renderer.material.color = knifeEnemy;
                    break;
                case EnemyType.SNIPER:
                    break;
                case EnemyType.BIDIRECTIONAL:
                    _renderer.material.color = biDirectionalEnemy;
                    break;
                case EnemyType.DOGS:
                    break;
                case EnemyType.CIRCULAR_COP:
                    _renderer.material.color = circularEnemy;
                    break;
                case EnemyType.GUARD_TORCH:
                    break;
                case EnemyType.TARGET:
                    break;
                default:
                    break;
            }
        }

        async private Task MoveInCurve(Vector3 _location)
        {
            float t = 0;
            while(t<durationOfMoveAnimation)
            {
                t += Time.deltaTime;
                // transform.position = new Vector3(transform.position.x, animationCurve.Evaluate(t / 15) * 10, transform.position.z);
                this.transform.LookAt(_location);
                this.transform.localPosition = Vector3.Lerp(this.transform.localPosition, _location, moveAnimationCurve.Evaluate(t / durationOfMoveAnimation));
                await new WaitForEndOfFrame();
            }            
        }
    }
}