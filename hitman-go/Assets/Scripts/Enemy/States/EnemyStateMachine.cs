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
                    break;

                case EnemyStates.CHASE:
                    currentState = new EnemyChaseState();
                    
                    break;
                case EnemyStates.MOVING:
                    currentState = new EnemyMovingState();
                    break;
                case EnemyStates.CONSTANT_CHASE:
                    currentState = new EnemyConstantChaseState();
                    break;
                case EnemyStates.RETURN_TO_PATH:
                    currentState = new EnemyReturnToPathState();
                    break;
                case EnemyStates.DEATH:
                    currentState = null;
                    break;

            }
                    currentState.OnStateEnter();
        }
        public EnemyStates GetEnemyState()
        {
            return currentState.GetCurrentState();
        }
    }
}