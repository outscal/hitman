using UnityEngine;
using Common;
using System.Collections;

namespace Enemy
{
    public class EnemyConstantChaseState : IEnemyState
    {
        EnemyStates thisState;
        public EnemyConstantChaseState()
        {
            thisState = EnemyStates.CONSTANT_CHASE;
        }
        public void OnStateEnter()
        {
                        
        }

        public void OnStateExit()
        {
            
        }
        public EnemyStates GetCurrentState()
        {
            return thisState;
        }
    }
}