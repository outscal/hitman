using UnityEngine;
using Common;
using System.Collections;

namespace Player
{
    public interface IPlayerState 
    {
        
        void OnStateEnter();
        void OnStateExit();
        PlayerStates GetCurrentStateType();
    }
}