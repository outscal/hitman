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
        private Color staticEnemy, petrolEnemy, knifeEnemy, biDirectionalEnemy
            , circularEnemy;


        public override void SetController(InteractableController interactableController)
        {
            this.guardUniformController = (GuardUniformController)interactableController;
            Debug.Log("[GuardUniformView] EnemyType:" + guardUniformController.GetEnemyType());
            switch (guardUniformController.GetEnemyType())
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
    }
}