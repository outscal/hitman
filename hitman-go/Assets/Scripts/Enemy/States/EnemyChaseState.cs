using UnityEngine;
using Common;
using System.Collections;

namespace Enemy
{
    public class EnemyChaseState : IEnemyState
    {
        EnemyStates thisState;
        public EnemyChaseState()
        {
            thisState = EnemyStates.CHASE;
        }
        public void OnStateEnter()
        {
            
            Debug.Log("In chase State enemy");
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