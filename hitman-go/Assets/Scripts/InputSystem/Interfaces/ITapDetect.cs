using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace InputSystem
{
    public interface ITapDetect
    {
        GameObject ReturnObject(Vector2 position, LayerMask layerMask);
        GameObject ReturnObject(Vector2 position);
    }
}