using System.Collections;
using UnityEngine;

namespace Player
{
    public interface IPlayerView 
    {
        GameObject GetGameObject();

        void MoveToLocation(Vector3 location);
        void DisablePlayer();
        void Reset();
    }
}
