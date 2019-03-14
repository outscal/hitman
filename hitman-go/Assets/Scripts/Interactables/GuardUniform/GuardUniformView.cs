using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Common;

namespace InteractableSystem
{
    public class GuardUniformView : InteractableView
    {
        private GuardUniformController guardUniformController;
        [SerializeField] private Renderer _renderer;
        [SerializeField]
        private Texture staticEnemy, petrolEnemy, knifeEnemy, biDirectionalEnemy
            , circularEnemy;


        public override void SetController(InteractableController interactableController)
        {
            this.guardUniformController = (GuardUniformController)interactableController;
            
            switch (guardUniformController.GetEnemyType())
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
    }
}