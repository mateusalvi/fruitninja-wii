using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class sparkController : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        Quaternion up = Quaternion.Euler(0, 1, 0);

        // Dampen towards the target rotation
        transform.rotation = Quaternion.Euler(0, 1, 0);
    }
}
