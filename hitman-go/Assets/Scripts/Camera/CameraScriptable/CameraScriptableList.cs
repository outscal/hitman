using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace CameraSystem
{
    [CreateAssetMenu(fileName = "CameraDataList", menuName = "Custom Objects/Camera/CameraDataList", order = 1)]
    public class CameraScriptableList : ScriptableObject
    {
        public List<CameraScriptableObj> cameraObjectList;
    }
}