using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace CameraSystem
{
    public class CameraController
    {
        private CameraView cameraView;
        private float panSpeed = 0.5f, zoomSpeed = 4f;

        public CameraController(CameraView cameraPrefab)
        {
            GameObject gameObject = GameObject.Instantiate<GameObject>(cameraPrefab.gameObject);
            cameraView = gameObject.GetComponent<CameraView>();
        }

        public void PanCamera(Vector3 position)
        {
            Vector3 targetPos = new Vector3(position.x, position.y, 0);
            //cameraView.transform.Translate(targetPos, Space.Self);

            cameraView.transform.position += (targetPos * Time.deltaTime);
        }

        public void ZoomCamera(Vector3 zoomValue)
        {
            Vector3 zoomPos = zoomValue.y * zoomSpeed * cameraView.transform.forward;
            cameraView.transform.Translate(zoomPos, Space.Self);
        }
    }
}