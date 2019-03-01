using UnityEngine;
using Common;
using InteractableSystem;
using System.Collections;

namespace Player
{
    public interface IPlayerState 
    {
        
        void OnStateEnter(PlayerStates playerStates = PlayerStates.NONE,IInteractableController controller=null);        
        void OnStateExit();
        PlayerStates GetCurrentStateType();
    }
}