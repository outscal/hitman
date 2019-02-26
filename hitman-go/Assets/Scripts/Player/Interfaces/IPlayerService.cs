using UnityEngine;
using Common;
using Zenject;
using System.Collections;

namespace Player
{
    public interface IPlayerService
    {       
<<<<<<< HEAD:hitman-go/Assets/Scripts/Player/Interfaces/IPlayerService.cs

        void SpawnPlayer();

=======
        void SpawnPlayer(Node node);
>>>>>>> Swapnil/Input_System:hitman-go/Assets/Scripts/Player/IPlayerService.cs
        void SetDirection(Directions _direction);

    }
}

