using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using CameraSystem;
using RTS_Cam;

namespace ScriptableObjSystem
{
    [CreateAssetMenu(fileName = "GameBasicObj", menuName = "Custom Objects/GameBasicObj", order = 6)]
    public class GameBasicObjects : ScriptableObject
    {
        //public CameraView cameraPrefab;
        public CameraScript cameraPrefab;
        public RTS_Camera rts_Camera;
    }
}