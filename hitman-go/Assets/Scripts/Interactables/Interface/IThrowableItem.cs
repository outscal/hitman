﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Common;

namespace InteractableSystem
{
    public interface IThrowableItem
    {
        void ThrowAction(int targetNodeID);
    }
}