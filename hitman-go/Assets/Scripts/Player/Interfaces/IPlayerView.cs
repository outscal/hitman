using System.Collections;
using Common;
using UnityEngine;

namespace Player
{
    public interface IPlayerView 
    {
        GameObject GetGameObject();

        void MoveToLocation(Vector3 location);
        void DisablePlayer();
        void Reset();
        void PlayAnimation(PlayerStates state);
    }
}
