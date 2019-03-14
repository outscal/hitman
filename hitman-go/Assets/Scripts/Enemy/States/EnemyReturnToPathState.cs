using UnityEngine;
using Common;
using System.Collections;

namespace Enemy
{
    public class EnemyReturnToPathState : IEnemyState
    {
        EnemyStates thisState;
        public EnemyReturnToPathState()
        {
            thisState = EnemyStates.RETURN_TO_PATH;
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