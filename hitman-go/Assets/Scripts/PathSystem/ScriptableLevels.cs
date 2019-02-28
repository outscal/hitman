using System.Collections.Generic;
using UnityEngine;
namespace PathSystem
{
     [CreateAssetMenu(fileName = "ScriptableLevels", menuName = "Custom Objects/Graph/ScriptableLevels", order = 0)]

    public class ScriptableLevels:ScriptableObject
    {
        public List<ScriptableGraph> levelsList;
    }
}