using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Player;
using Zenject;
using GameState;
using PathSystem;
using PathSystem.NodesScript;
using Player;

namespace CameraSystem
{
    public class CameraScript : MonoBehaviour
    {
        public GameObject playerTarget, platformTarget, cameraObj;
        
        Vector3 targetPosition;
        public float smoothFactor = 0.5f, radius = 2f;
        public bool rotateAroundPoint = true;
        public float rotationSpeed = 5f;
        public float perspectiveZoomSpeed;

        private Vector3 cameraOffsetDistance, centerPoint;

        private Vector3 startPos, currentPos;

        private Touch touchOne, touchZero;

        private float fieldOfViewRatio;
        private bool zooming = false;
        private bool startCamera;
        GameObject gameObject;

        // Start is called before the first frame update
        void Start()
        {
            //SetCameraSettings();
        }

        public void SetCameraSettings()
        {
            playerTarget = FindObjectOfType<PlayerView>().gameObject;
            platformTarget = GameObject.FindWithTag("Platform").gameObject;

            if(playerTarget != null && platformTarget != null)
            {
                centerPoint = (playerTarget.transform.position + platformTarget.transform.position) / 2;
                //transform.position = playerTarget.transform.position - new Vector3()
                cameraOffsetDistance = cameraObj.transform.position - centerPoint;
                fieldOfViewRatio = cameraObj.GetComponent<Camera>().fieldOfView /
                                   Vector3.Distance(playerTarget.transform.position, platformTarget.transform.position);
                startCamera = true;
            }

        }

        // Update is called once per frame
        void LateUpdate()
        {
            if (startCamera == false) return;

            centerPoint = (playerTarget.transform.position + platformTarget.transform.position) / 2;
            if (zooming == false)
            {
                float fov = fieldOfViewRatio *
                    Vector3.Distance(playerTarget.transform.position, platformTarget.transform.position);
                cameraObj.GetComponent<Camera>().fieldOfView = iTween.FloatUpdate(cameraObj.GetComponent<Camera>().fieldOfView,
                fov, 0.5f);
            }

            if (Input.touchCount == 0)
            {
                if (zooming == true)
                    zooming = false;

                if (gameObject != null)
                    gameObject = null;
            }

            if (Input.touchCount >= 1)
            {
                Touch touch = Input.GetTouch(0);

                if (touch.phase == TouchPhase.Began)
                {
                    gameObject = ReturnObject(touch.position);

                    startPos = touch.deltaPosition;
                    currentPos = startPos;
                }

                if (gameObject.GetComponent<NodeControllerView>() == null &&
                   gameObject.GetComponent<PlayerView>() == null)
                {
                    if (Input.touchCount == 1)
                    {
                        if (touch.phase == TouchPhase.Moved)
                        {
                            currentPos = touch.deltaPosition;
                            float x = currentPos.x - startPos.x;
                            Quaternion camAngle = Quaternion.AngleAxis(x * rotationSpeed, Vector3.up);
                            cameraOffsetDistance = camAngle * cameraOffsetDistance;
                        }

                        Vector3 newPos = centerPoint + cameraOffsetDistance;

                        cameraObj.transform.position = Vector3.Slerp(cameraObj.transform.position, newPos, smoothFactor);
                    }
                    else if (Input.touchCount > 1)
                    {
                        if (zooming == false) zooming = true;
                        touchOne = Input.GetTouch(1);
                        touchZero = Input.GetTouch(0);

                        Vector2 touchZeroPreviousPosition = touchZero.position - touchZero.deltaPosition;

                        Vector2 touchOnePreviousPosition = touchOne.position - touchOne.deltaPosition;

                        float prevTouchDeltaMag = (touchZeroPreviousPosition - touchOnePreviousPosition).magnitude;
                        float TouchDeltaMag = (touchZero.position - touchOne.position).magnitude;
                        float deltaMagDiff = prevTouchDeltaMag - TouchDeltaMag;

                        cameraObj.GetComponent<Camera>().fieldOfView += deltaMagDiff * perspectiveZoomSpeed * Time.deltaTime;
                        cameraObj.GetComponent<Camera>().fieldOfView = Mathf.Clamp(cameraObj.GetComponent<Camera>().fieldOfView, .1f, 179.9f);
                    }
                }
            }

            cameraObj.transform.LookAt(centerPoint);
        }

        public GameObject ReturnObject(Vector2 position)
        {
            Ray ray = Camera.main.ScreenPointToRay(position);
            RaycastHit raycast;
            if (Physics.Raycast(ray, out raycast, Mathf.Infinity))
            {
                gameObject = raycast.collider.gameObject;
                if (gameObject != null)
                    Debug.Log("[TapDetect] GameObject:" + gameObject.name);
            }

            return gameObject;
        }

#if UNITY_EDITOR
        private void OnDrawGizmos()
        {
            Gizmos.color = Color.red;
            Gizmos.DrawSphere(centerPoint, radius);
        }
#endif

    }
}