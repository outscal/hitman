using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Player;

namespace CameraSystem
{
    public class CameraScript : MonoBehaviour
    {
        public GameObject player; 
        public bool startCamera;
        Vector3 StartPosition;
        Vector2 DragStartPosition;
        Vector2 DragNewPosition;
        Vector2 Finger0Position;
        float DistanceBetweenFingers;
        bool isZooming, isPanning;
        private Vector3 difference;
        private float minDistanceFromPlayer = 2f;

        public void SetCamera()
        {
            difference = player.transform.position - transform.position;
            startCamera = true;
            Vector3 targetPos = player.transform.position - difference;
            targetPos.x = player.transform.position.x;
            targetPos.z = player.transform.position.z + 1f;
            transform.position = targetPos;
            iTween.MoveTo(this.gameObject, targetPos, 0.5f);
        }

        private void LateUpdate()
        {
            if(player == null)
            {
                player = FindObjectOfType<PlayerView>().gameObject;
                SetCamera();
            }

            if (player != null)
            {
                if (isPanning == false)
                {
                    Vector3 targetPos = player.transform.position - difference;
                    if (Vector3.Distance(player.transform.position - difference, transform.position) >= minDistanceFromPlayer)
                    {
                        targetPos.x = player.transform.position.x;
                        targetPos.z = player.transform.position.z + 1f;
                        transform.position = targetPos;
                        iTween.MoveTo(this.gameObject, targetPos, 0.5f);
                    }
                }
            }
        }

        // Update is called once per frame
        void Update()
        {
            if (startCamera == false) return;

            if (Input.touchCount == 0)
            {
                if (isZooming == true)
                    isZooming = false;
                if (isPanning == true)
                    isPanning = false;
            }

            if (Input.touchCount == 1)
            {
                if (!isZooming)
                {
                    if (Input.GetTouch(0).phase == TouchPhase.Moved)
                    {
                        if (isPanning == false) isPanning = true;
                        Vector3 NewPosition = GetWorldPosition();
                        Vector3 PositionDifference = NewPosition - StartPosition;
                        gameObject.transform.Translate(PositionDifference);
                    }
                    StartPosition = GetWorldPosition();
                }
            }
            else if (Input.touchCount == 2)
            {
                if (Input.GetTouch(1).phase == TouchPhase.Moved)
                {
                    isZooming = true;

                    DragNewPosition = GetWorldPositionOfFinger(1);
                    Vector2 PositionDifference = DragNewPosition - DragStartPosition;

                    if (Vector2.Distance(DragNewPosition, Finger0Position) < DistanceBetweenFingers)
                        gameObject.GetComponent<Camera>().orthographicSize += (PositionDifference.magnitude);

                    if (Vector2.Distance(DragNewPosition, Finger0Position) >= DistanceBetweenFingers)
                        gameObject.GetComponent<Camera>().orthographicSize -= (PositionDifference.magnitude);

                    DistanceBetweenFingers = Vector2.Distance(DragNewPosition, Finger0Position);
                }
                DragStartPosition = GetWorldPositionOfFinger(1);
                Finger0Position = GetWorldPositionOfFinger(0);
            }
        }

        Vector2 GetWorldPosition()
        {
            return gameObject.GetComponent<Camera>().ScreenToWorldPoint(Input.mousePosition);
        }

        Vector2 GetWorldPositionOfFinger(int FingerIndex)
        {
            return gameObject.GetComponent<Camera>().ScreenToWorldPoint(Input.GetTouch(FingerIndex).position);
        }
    }
}