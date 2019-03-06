using System;
using System.Collections.Generic;

namespace Common
{
    [Serializable]
    public class ScriptableNode
    {
        
        public NodeData node;
        public int up = -1, down = -1, left = -1, right = -1;
        public List<int> teleport = new List<int>();
        public ScriptableNode()
        {
            up = -1; down = -1; left = -1; right = -1;
        }
        public int[] GetConnections()
        {
            return new int[4] { up, down, left, right };
        }
    }
}