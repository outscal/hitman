using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Common;
using Zenject;
using GameState;
using ScriptableObjSystem;
using Player;
using RTS_Cam;

namespace CameraSystem
{
    public class CameraManager : ICameraManager
    {
        private CameraController cameraController;
        readonly SignalBus signalBus;
        private GameBasicObjects gameBasicObjects;
        private RTS_Camera cameraScript;

        public CameraManager(SignalBus signalBus, GameBasicObjects gameBasicObjects)
        {
            this.gameBasicObjects = gameBasicObjects;
            this.signalBus = signalBus;
            signalBus.Subscribe<GameStartSignal>(GameStart);
        }

        void GameStart()
        {
            //cameraController = new CameraController(gameBasicObjects.cameraPrefab);
            GameObject gameObject = GameObject.Instantiate<GameObject>(gameBasicObjects.rts_Camera.gameObject);
            cameraScript = gameObject.GetComponent<RTS_Camera>();
            //cameraScript.SetTarget()
        }

        public CameraController GetCameraController()
        {
            return cameraController;
        }
    }

}