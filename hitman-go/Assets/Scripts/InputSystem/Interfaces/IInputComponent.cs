using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace InputSystem
{
    public interface IInputComponent
    {
        void OnInitialized(IInputService inputService);
        void StartPosition(Vector3 pos);
        void OnTick();
    }
}