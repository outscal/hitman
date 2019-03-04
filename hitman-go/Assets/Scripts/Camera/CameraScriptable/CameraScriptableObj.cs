using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace CameraSystem
{
    [System.Serializable]
    public struct CameraData
    {
        public int nodeID;
        public Vector3 position;
        public Quaternion rotation;
        public float fieldOfView;
    }

    [CreateAssetMenu(fileName = "CameraData",menuName = "Custom Objects/Camera/CameraData",order = 2)]
    public class CameraScriptableObj : ScriptableObject
    {
        public CameraData cameraData;
    }
}