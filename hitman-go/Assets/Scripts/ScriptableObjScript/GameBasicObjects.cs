using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using CameraSystem;

namespace ScriptableObjSystem
{
    [CreateAssetMenu(fileName = "GameBasicObj", menuName = "Custom Objects/GameBasicObj", order = 6)]
    public class GameBasicObjects : ScriptableObject
    {
        public CameraScript CameraScript;
    }
}