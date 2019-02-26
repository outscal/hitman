using UnityEngine;
using Common;
using Zenject;
using System.Collections;

namespace Player
{
    public interface IPlayerService
    {       

        void SpawnPlayer(Vector3 spawnLocation);
        void SetDirection(Directions _direction);

    }
}
