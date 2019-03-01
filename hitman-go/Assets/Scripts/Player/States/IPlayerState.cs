using UnityEngine;
using Common;
using System.Threading.Tasks;
using InteractableSystem;
using System.Collections;

namespace Player
{
    public interface IPlayerState 
    {
        
        Task OnStateEnter(PlayerStates playerStates = PlayerStates.NONE,IInteractableController controller=null);        
        void OnStateExit();
        PlayerStates GetCurrentStateType();
    }
}