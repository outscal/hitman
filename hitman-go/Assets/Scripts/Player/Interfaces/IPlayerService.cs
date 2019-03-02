using UnityEngine;
using System.Threading.Tasks;
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

        int GetPlayerNodeID();

        void OnGameStart();
        bool PlayerDeathStatus();
        bool CheckForKillablePlayer();
        bool CheckForRange( int _nodeID);
        void ChangeToEnemyState();
        Task<int> GetTargetNode();
    }

}

