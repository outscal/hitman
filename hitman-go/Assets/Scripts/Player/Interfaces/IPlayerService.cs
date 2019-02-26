using UnityEngine;
using Common;
using Zenject;
using System.Collections;

namespace Player
{
    public interface IPlayerService
    {       

        void SpawnPlayer();

        void SetDirection(Directions _direction);

    }
}

