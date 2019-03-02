using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Common;
using Zenject;
using ScriptableObjSystem;
using GameState;
using PathSystem;

namespace CameraSystem
{
    public class CameraManager : ICamera
    {
        readonly SignalBus signalBus;
        private CameraScript cameraScript;
        private GameBasicObjects gameBasicObjects;
        private CameraScriptableObj cameraData;
        private IPathService pathService;

        public CameraManager(SignalBus signalBus, GameBasicObjects gameBasicObjects, IPathService pathService)
        {
            this.signalBus = signalBus;
            this.pathService = pathService;
            this.gameBasicObjects = gameBasicObjects;
            signalBus.Subscribe<GameStartSignal>(GameStarted);
        }

        void GameStarted()
        {
            //cameraData = pathService.GetCameraData();
            if (cameraScript == null)
            {
                GameObject cameraObj = GameObject.Instantiate<GameObject>(gameBasicObjects.CameraScript.gameObject);
                cameraScript = cameraObj.GetComponent<CameraScript>();
                //cameraObj.transform.position = cameraData.cameraData.position;
                cameraData = Resources.Load("Camera/CameraDataLevel2") as CameraScriptableObj;
                cameraScript.SetCameraSettings(cameraData);
            }
            //else if (cameraScript != null)
                cameraScript.SetCameraSettings(cameraData);
        }

    }
}