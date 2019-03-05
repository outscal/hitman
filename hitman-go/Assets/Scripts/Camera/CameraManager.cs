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
        private List<CameraScriptableObj> cameraDataList;
        private IPathService pathService;
        private IGameService gameService;

        public CameraManager(SignalBus signalBus, GameBasicObjects gameBasicObjects, IPathService pathService)
        {
            this.signalBus = signalBus;
            this.pathService = pathService;
            this.gameBasicObjects = gameBasicObjects;
            signalBus.Subscribe<GameStartSignal>(GameStarted);
            //signalBus.Subscribe<StateChangeSignal>()
        }

        public void SetNodeID(int nodeID)
        {
            for (int i = 0; i < cameraDataList.Count; i++)
            {
                if(cameraDataList[i].cameraData.nodeID == nodeID)
                {
                    cameraScript.MoveToNode(cameraDataList[i].cameraData);
                }
            }
        }

        void GameStarted()
        {
            cameraDataList = pathService.GetCameraDataList();
            

            if (cameraScript == null)
            {
                GameObject cameraObj = GameObject.Instantiate<GameObject>(gameBasicObjects.CameraScript.gameObject);
                cameraScript = cameraObj.GetComponent<CameraScript>();
                //cameraObj.transform.position = cameraData.cameraData.position;
                //cameraData = Resources.Load("Camera/CameraDataLevel2") as CameraScriptableObj;
                cameraScript.SetCameraSettings(cameraDataList[0]);
            }
            else if (cameraScript != null)
                cameraScript.SetCameraSettings(cameraDataList[0]);
        }

    }
}