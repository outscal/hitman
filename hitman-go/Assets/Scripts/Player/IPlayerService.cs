using UnityEngine;
using Common;
using Zenject;
using System.Collections;

namespace Player
{
    public interface IPlayerService
    {       
        void SpawnPlayer(Node node);
        void SetDirection(Directions _direction);

    }
}
