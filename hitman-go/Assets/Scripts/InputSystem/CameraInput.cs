using UnityEngine;
using Common;
using PathSystem.NodesScript;
using Player;

namespace InputSystem
{
    public class CameraInput : IInputComponent
    {
        private IInputService inputService;
        private GameObject camera;
        private GameObject gameObject;
        private float speed = 6f;

        public void OnInitialized(IInputService inputService)
        {
            this.inputService = inputService;
        }

        public void OnTick()
        {
            if(camera == null)
            {
                camera = GameObject.FindObjectOfType<Camera>().gameObject;
                Debug.Log("[CameraInput] Camera Found");
            }

            if (Input.touchCount >= 1)
            {
                Touch touch = Input.GetTouch(0);

                if (touch.phase == TouchPhase.Began)
                {
                    gameObject = inputService.GetTapDetect().ReturnObject(touch.position, 0);

                }
                else if(touch.phase == TouchPhase.Moved)
                {
                    if (gameObject != null)
                    {
                        Vector2 touchPosition = touch.position;
                        if (gameObject.GetComponent<NodeControllerView>() == null
                        && gameObject.GetComponent<PlayerView>() == null)
                        {
                            if (camera != null)
                            {
                                camera.transform.Translate(-touchPosition.x * 6f,
                                                            touchPosition.y * 6f,
                                                            1f);
                            }
                        }
                    }
                }
                else if (touch.phase == TouchPhase.Ended)
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