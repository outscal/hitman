using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace InputSystem
{
    public interface IInputComponent
    {
        void OnInitialized(IInputService inputService);

        void DetectTap();

        void OnTick();
    }
}