using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Common;

namespace InteractableSystem
{
    [System.Serializable]
    public struct InteractableItem
    {
        public string name;
        public InteractablePickup interactablePickup;
        public InteractableView interactableView;
    }

    [CreateAssetMenu(fileName = "InteractableObjList",
                     menuName = "Custom Objects/InteractableItems",
                     order = 3)]
    public class InteractableScriptableObj : ScriptableObject
    {
        public List<InteractableItem> interactableItems;
    }
}