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
        private Texture staticEnemy, petrolEnemy, knifeEnemy, biDirectionalEnemy, circularEnemy;
        [SerializeField]
        private GameObject sniperRifle;

        public Animator animator;
        public AnimationCurve moveAnimationCurve;
       
        public float durationOfMoveAnimation;

        private void Start()
        {
            sniperRifle.SetActive(false);
        }

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

        async public Task PlayAnimation(PlayerStates state)
        {
            if (state == PlayerStates.SHOOTING)
            {
                sniperRifle.SetActive(true);
                animator.StartPlayback();
                animator.Play("Shoot");
            } else if (state == PlayerStates.DEAD)
            {
                animator.Play("Death");
                await new WaitForSeconds(2f);
            }
        }

        private void TestAnimation()
        {
           // animator.Play("Shoot");
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
                    _renderer.material.mainTexture = staticEnemy;
                    break;
                case EnemyType.PATROLLING:
                    _renderer.material.mainTexture = petrolEnemy;
                    break;
                case EnemyType.ROTATING_KNIFE:
                    _renderer.material.mainTexture = knifeEnemy;
                    break;
                case EnemyType.SNIPER:
                    break;
                case EnemyType.BIDIRECTIONAL:
                    _renderer.material.mainTexture = biDirectionalEnemy;
                    break;
                case EnemyType.DOGS:
                    break;
                case EnemyType.CIRCULAR_COP:
                    _renderer.material.mainTexture = circularEnemy;
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
                this.transform.LookAt(_location);
                this.transform.localPosition = Vector3.Lerp(this.transform.localPosition, _location, moveAnimationCurve.Evaluate(t/ durationOfMoveAnimation));                
                await new WaitForEndOfFrame();
            }            
        }

        public void StopAnimation(PlayerStates playerState)
        {
            Debug.Log("Stop Animation Called");
            if(playerState==PlayerStates.SHOOTING)
            {
                sniperRifle.SetActive(false);
                animator.Play("DefaultIdleState");
            }
        }
    }
}