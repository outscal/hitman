using UnityEngine;
using Common;
using System.Collections;

namespace Enemy
{
    public class EnemyIdleState : IEnemyState
    {
        EnemyStates thisState;
        public EnemyIdleState()
        {
            thisState = EnemyStates.IDLE;
        }
        public void OnStateEnter()
        {
//            Debug.Log("in idle state enemy");
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