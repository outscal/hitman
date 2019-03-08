using System.Collections;
using Common;
using UnityEngine;
using System.Threading.Tasks;

namespace Player
{
    public interface IPlayerView
    { 

       Task MoveToLocation(Vector3 location);
        void DisablePlayer();
        void Reset();
        void PlayAnimation(PlayerStates state);
        void SetDisguise(EnemyType disguiseType);
    }
}
