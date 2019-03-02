using UnityEngine;
using Common;
using System.Collections;

namespace Enemy
{
    public class EnemyStateMachine
    {
        private IEnemyState currentState;
        private IEnemyState previousState;

        public EnemyStateMachine()
        {
            currentState = new EnemyIdleState();
            currentState.OnStateEnter();
        }

        public void ChangeEnemyState(EnemyStates _enemyState)
        {
            previousState = currentState;
            if (previousState != null)
            {
                previousState.OnStateExit();
            }
            switch (_enemyState)
            {
                case EnemyStates.IDLE:
                    currentState = new EnemyIdleState();
                    currentState.OnStateEnter();
                    break;

                case EnemyStates.CHASE:
                    currentState = new EnemyChaseState();
                    currentState.OnStateEnter();
                    break;
            }
        }
        public EnemyStates GetEnemyState()
        {
            return currentState.GetCurrentState();
        }
    }
}