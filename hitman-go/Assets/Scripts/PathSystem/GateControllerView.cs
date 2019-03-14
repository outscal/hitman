using UnityEngine;
using Common;
namespace PathSystem
{
    public class GateControllerView:MonoBehaviour
    {
        KeyTypes key;
        int Node1st,Node2nd;
        public void SetGate(KeyTypes key,int node1,int node2){
            this.key=key;
            Node1st=node1;
            Node2nd=node2;
        }
        public KeyTypes GetKey()
        {
            return key;
        }
        public void Disable(){   
                Destroy(gameObject);
        } 

    }
}