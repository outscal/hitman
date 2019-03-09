using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Player;
using Zenject;
using GameState;
using PathSystem.NodesScript;

namespace CameraSystem
{
    public class CameraScript : MonoBehaviour
    {
        public GameObject playerTarget, platformTarget, cameraObj;
        public List<Transform> targets;
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

        public void SetCameraSettings(CameraScriptableObj cameraData)
        {
            //playerTarget = FindObjectOfType<PlayerView>().gameObject;
            //platformTarget = GameObject.FindWithTag("Platform").gameObject;

            //if(playerTarget != null && platformTarget != null)
            //{
            //    centerPoint = (playerTarget.transform.position + platformTarget.transform.position) / 2;
            //    cameraOffsetDistance = cameraObj.transform.position - centerPoint;
            //    fieldOfViewRatio = cameraObj.GetComponent<Camera>().fieldOfView /
            //                       Vector3.Distance(playerTarget.transform.position, platformTarget.transform.position);
            //    startCamera = true;
            //}

            transform.position = cameraData.cameraData.position;
            transform.rotation = cameraData.cameraData.rotation;
            cameraObj.GetComponent<Camera>().fieldOfView = cameraData.cameraData.fieldOfView;

        }

        void SetCameraCentre()
        {
            targets = new List<Transform>();
            foreach (NodeControllerView target in FindObjectsOfType<NodeControllerView>())
            {
                Transform node = target.gameObject.transform;
                targets.Add(node);
            }

            centerPoint = GetCenterPoint();

            cameraOffsetDistance = cameraObj.transform.position - centerPoint;
            //fieldOfViewRatio = cameraObj.GetComponent<Camera>().fieldOfView /
                               //Vector3.Distance(playerTarget.transform.position, platformTarget.transform.position);
            startCamera = true;
        }

        // Update is called once per frame
        void LateUpdate()
        {
            //if (startCamera == false) return;

            //centerPoint = (playerTarget.transform.position + platformTarget.transform.position) / 2;
            //if (zooming == false)
            //{
            //    float fov = fieldOfViewRatio *
            //        Vector3.Distance(playerTarget.transform.position, platformTarget.transform.position);
            //    if (fov < 35)
            //        fov = 35;
            //    cameraObj.GetComponent<Camera>().fieldOfView = iTween.FloatUpdate(cameraObj.GetComponent<Camera>().fieldOfView,
            //    fov, 0.5f);
            //}

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

                if (gameObject != null)
                {
                    if (gameObject.GetComponent<NodeControllerView>() == null &&
                       gameObject.GetComponent<PlayerView>() == null)
                    {
                        if (Input.touchCount == 1)
                        {
                            //if (touch.phase == TouchPhase.Moved)
                            //{
                            //    currentPos = touch.deltaPosition;
                            //    float x = currentPos.x - startPos.x;
                            //    Quaternion camAngle = Quaternion.AngleAxis(x * rotationSpeed, Vector3.up);
                            //    cameraOffsetDistance = camAngle * cameraOffsetDistance;
                            //}

                            //Vector3 newPos = centerPoint + cameraOffsetDistance;

                            //cameraObj.transform.position = Vector3.Slerp(cameraObj.transform.position, newPos, smoothFactor);
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
                            cameraObj.GetComponent<Camera>().fieldOfView = Mathf.Clamp(cameraObj.GetComponent<Camera>().fieldOfView, 35f, 100f);
                        }
                    }
                }
            }

            //cameraObj.transform.LookAt(centerPoint);
        }

        public GameObject ReturnObject(Vector2 position)
        {
            Ray ray = Camera.main.ScreenPointToRay(position);
            RaycastHit raycast;
            if (Physics.Raycast(ray, out raycast, Mathf.Infinity))
            {
                gameObject = raycast.collider.gameObject;
              
                    
            }

            return gameObject;
        }

        Vector3 GetCenterPoint()
        {
            if(targets.Count == 1)
            {
                return targets[0].position;
            }

            var bounds = new Bounds(targets[0].position, Vector3.zero);

            for (int i = 0; i < targets.Count; i++)
            {
                bounds.Encapsulate(targets[i].position);
            }

            return bounds.center;

        }

        public void MoveToNode(CameraData cameraData)
        {
            iTween.MoveTo(cameraObj, cameraData.position, 0.8f);
            iTween.RotateTo(cameraObj, cameraData.rotation.eulerAngles, 0.8f);
            float fov;
            float currentFOV = cameraObj.GetComponent<Camera>().fieldOfView;
            float targetFOV = cameraData.fieldOfView;
            fov = iTween.FloatUpdate(currentFOV, targetFOV, 2f);
            cameraObj.GetComponent<Camera>().fieldOfView = fov;
        }

        //Script used by Editor
        public CameraData GetCameraData()
        {
            CameraData cameraData = new CameraData();

            if (cameraObj != null)
            {
                cameraData.fieldOfView = cameraObj.GetComponent<Camera>().fieldOfView;
                cameraData.position = cameraObj.transform.position;
                cameraData.rotation = cameraObj.transform.rotation;
            }
            return cameraData;
        }

        //#if UNITY_EDITOR
        //        private void OnDrawGizmos()
        //        {
        //            Gizmos.color = Color.red;
        //            Gizmos.DrawSphere(centerPoint, radius);
        //        }
        //#endif

    }
}