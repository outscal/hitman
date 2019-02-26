using UnityEngine;
using Common;
using System.Collections;

namespace Player
{
    [CreateAssetMenu(fileName = "Scriptable Player Attributes", menuName = "Custom Objects/Player/Player Attributes", order = 0)]
    public class PlayerScriptableObject : ScriptableObject
    {
        public IPlayerView playerView;
    }
}