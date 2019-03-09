using UnityEngine;
using System.Threading.Tasks;
using Common;
using Zenject;
using System.Collections;
using InteractableSystem;

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
        bool CheckForKillablePlayer(EnemyType enemyType);
        bool CheckForRange( int _nodeID);
        void ChangeToEnemyState();
        int GetTargetNode();
        void FireLevelFinishedSignal();
        void SetTargetTap(int v);
        SignalBus GetSignalBus();
    }

}

