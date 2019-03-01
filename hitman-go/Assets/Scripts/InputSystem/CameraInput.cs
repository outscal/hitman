using UnityEngine;
using Common;
using PathSystem.NodesScript;
using Player;
using CameraSystem;

namespace InputSystem
{
    public class CameraInput : IInputComponent
    {
        private IInputService inputService;
        //private GameObject camera;
        private GameObject gameObject;
        private float speed = 6f;
        //private float minDragDistance = Screen.height * 2 / 100;

        Vector2 startTouch, currentTouch;
        //Vector2 firstTouch, secondTouch;


        public void OnInitialized(IInputService inputService)
        {
            this.inputService = inputService;
        }

        public void OnTick()
        {
            if (Input.touchCount >= 1)
            {
                Touch touch = Input.GetTouch(0);

                if(touch.phase == TouchPhase.Began)
                {
                    startTouch = touch.position;
                    currentTouch = touch.position;
                    if (inputService != null)
                        gameObject = inputService.GetTapDetect().ReturnObject(touch.position);
                }

                if (Input.touchCount == 1)
                {
                    if (touch.phase == TouchPhase.Moved)
                    {
                        if (gameObject != null)
                        {
                            currentTouch = touch.position;
                            Vector3 panValue = currentTouch - startTouch;

                            if (gameObject.GetComponent<NodeControllerView>() == null
                                && gameObject.GetComponent<PlayerView>() == null)
                            {
                                //if (camera != null)
                                //{
                                //    camera.transform.Translate(touchPosition.x * .5f,
                                //                                touchPosition.y * .5f,
                                //                                0f);
                                //}

                                inputService.GetCameraManager()
                                .GetCameraController()
                                    .PanCamera(panValue);

                            }
                        }
                    }
                }
                else if(Input.touchCount >= 2)
                {
                    Debug.Log("[CameraInput] Zoom Camera");
                }

                if (touch.phase == TouchPhase.Ended)
                {
                    if (gameObject != null)
                    {
                        gameObject = null;
                    }
                }

            }
        }

        public void StartPosition(Vector3 pos)
        {

        }
    }
}