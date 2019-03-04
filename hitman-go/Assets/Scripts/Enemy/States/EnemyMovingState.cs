using UnityEngine;
using Common;
using System.Collections;

namespace Enemy
{
    public class EnemyMovingState : IEnemyState
    {
        EnemyStates thisState;
        public EnemyMovingState()
        {
            thisState = EnemyStates.MOVING;
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