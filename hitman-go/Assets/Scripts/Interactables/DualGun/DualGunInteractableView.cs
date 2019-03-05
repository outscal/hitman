using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Common;

namespace InteractableSystem
{
    public class DualGunInteractableView : InteractableView
    {
        private DualGunInteractableController dualGunInteractableController;

        public void SetController(DualGunInteractableController dualGunInteractableController)
        {
            this.dualGunInteractableController = dualGunInteractableController;
        }

#if UNITY_EDITOR
        private void OnDrawGizmos()
        {
            Gizmos.color = Color.red;
            //up
            Gizmos.DrawLine(transform.position, transform.position + Vector3.back * 6f);
            //down
            Gizmos.DrawLine(transform.position, transform.position + Vector3.forward * 6f);
            //right
            Gizmos.DrawLine(transform.position, transform.position + Vector3.right * 6f);
            //left
            Gizmos.DrawLine(transform.position, transform.position + Vector3.left * 6f);
        }
#endif
    }
}