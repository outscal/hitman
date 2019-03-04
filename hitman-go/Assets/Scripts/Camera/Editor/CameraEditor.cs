using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

namespace CameraSystem
{
    [CustomEditor(typeof(CameraScript))]
    public class CameraEditor : Editor
    {
        public override void OnInspectorGUI()
        {
            DrawDefaultInspector();

            CameraScript myScript = (CameraScript)target;
            if (GUILayout.Button("Save Camera Data"))
            {
                CreateScriptableObject(myScript);
            }

        }

        void CreateScriptableObject(CameraScript cameraScript)
        {
            CameraScriptableObj asset = ScriptableObject.CreateInstance<CameraScriptableObj>();

            asset.cameraData = cameraScript.GetCameraData();

            AssetDatabase.CreateAsset(asset, "Assets/Resources/Camera/CameraData.asset");
            AssetDatabase.SaveAssets();
        }
    }
}