using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PrefabRotating : MonoBehaviour
{
    public int speed = 10;
    // Start is called before the first frame update
    private void Update()
    {
        transform.Rotate(new Vector3(0, 1 * Time.deltaTime * speed, 0));
    }

}
