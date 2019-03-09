using UnityEngine;
using System.Collections;
using Common;

namespace Enemy
{
    public interface IEnemyState
    {

        void OnStateEnter();
        void OnStateExit();
        EnemyStates GetCurrentState();
    }
}