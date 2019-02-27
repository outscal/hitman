using UnityEngine;
using Common;
using Zenject;
using System.Collections;

namespace Player
{
    public interface IPlayerService
    {       
        void SpawnPlayer();

        void SetSwipeDirection(Directions _direction);

        void SetTargetNode(int _nodeID);
    }

}

