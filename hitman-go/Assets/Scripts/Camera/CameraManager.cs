using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Common;
using Zenject;
using ScriptableObjSystem;
using GameState;

namespace CameraSystem
{
    public class CameraManager : ICamera
    {
        readonly SignalBus signalBus;
        private CameraScript cameraScript;
        private GameBasicObjects gameBasicObjects;

        public CameraManager(SignalBus signalBus, GameBasicObjects gameBasicObjects)
        {
            this.signalBus = signalBus;
            this.gameBasicObjects = gameBasicObjects;
            signalBus.Subscribe<GameStartSignal>(GameStarted);
        }

        void GameStarted()
        {
            GameObject cameraObj = GameObject.Instantiate<GameObject>(gameBasicObjects.CameraScript.gameObject);
            cameraScript = cameraObj.GetComponent<CameraScript>();
            cameraScript.SetCameraSettings();
        }

    }
}