using UnityEngine;
using Zenject;
using Common;
using PathSystem.NodesScript;

namespace InputSystem
{
    public class KeyboardInput : IInputComponent
    {
        private IInputService inputService;
        private int nodeLayer = 1 << 9;

        public KeyboardInput()
        {
            Debug.Log("<color=green>[KeyboardInput] Created:</color>");
        }

        void DetectTap()
        {
            if (Input.GetMouseButtonDown(0))
            {
                GameObject gameObject = inputService.GetTapDetect().ReturnObject(Input.mousePosition);//, nodeLayer);
                if(gameObject==null)
                {
                    return;
                }
                if (gameObject.GetComponent<NodeControllerView>() != null)
                {
                    inputService.PassNodeID(gameObject.GetComponent<NodeControllerView>().nodeID);
                }
            }
        }

        public void OnInitialized(IInputService inputService)
        {
            this.inputService = inputService;
        }

        public void OnTick()
        {
            DetectTap();
            DecideDirection();
        }

        void DecideDirection()
        {
            if (Input.GetKeyDown(KeyCode.A) || Input.GetKeyDown(KeyCode.LeftArrow))
            {

                inputService.PassDirection(Directions.LEFT);
            }
            else if (Input.GetKeyDown(KeyCode.D) || Input.GetKeyDown(KeyCode.RightArrow))
            {

                inputService.PassDirection(Directions.RIGHT);
            }

            if (Input.GetKeyDown(KeyCode.S) || Input.GetKeyDown(KeyCode.DownArrow))
            {
               
                inputService.PassDirection(Directions.DOWN);
            }
            else if (Input.GetKeyDown(KeyCode.W) || Input.GetKeyDown(KeyCode.UpArrow))
            {
               
                inputService.PassDirection(Directions.UP);
            }
        }

        public void StartPosition(Vector3 pos)
        {

        }
    }
}