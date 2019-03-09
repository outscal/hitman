using UnityEngine;
using Player;
using PathSystem.NodesScript;

namespace CameraSystem
{
    public class CameraScript : MonoBehaviour
    {
        public GameObject cameraObj;
        public float rotationSpeed = 5f;
        public float perspectiveZoomSpeed;

        private Vector3 cameraOffsetDistance;

        private Vector3 startPos, currentPos;

        private Touch touchOne, touchZero;

        private float fieldOfViewRatio;
        private bool zooming = false;
        private bool startCamera;
        GameObject gameObject;
        CameraData cameraData;

        public void SetCameraSettings(CameraScriptableObj cameraData)
        {
            transform.position = cameraData.cameraData.position;
            transform.rotation = cameraData.cameraData.rotation;
            cameraObj.GetComponent<Camera>().fieldOfView = cameraData.cameraData.fieldOfView;
        }

        void LateUpdate()
        {
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
                        if (Input.touchCount > 1)
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

        public void MoveToNode(CameraData _cameraData)
        {
            this.cameraData = _cameraData;
            iTween.MoveTo(cameraObj, cameraData.position, 0.8f);
            iTween.RotateTo(cameraObj, cameraData.rotation.eulerAngles, 0.8f);
            float fov;
            float currentFOV = cameraObj.GetComponent<Camera>().fieldOfView;
            float targetFOV = cameraData.fieldOfView;
            fov = iTween.FloatUpdate(currentFOV, targetFOV, 5f);
            cameraObj.GetComponent<Camera>().fieldOfView = fov;
        }

        //Script used by Editor
        public CameraData GetCameraData()
        {
            cameraData = new CameraData();

            if (cameraObj != null)
            {
                cameraData.fieldOfView = cameraObj.GetComponent<Camera>().fieldOfView;
                cameraData.position = cameraObj.transform.position;
                cameraData.rotation = cameraObj.transform.rotation;
            }
            return cameraData;
        }

    }
}