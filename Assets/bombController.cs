using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class bombController : MonoBehaviour
{
    Rigidbody rb;
    [SerializeField] float launchForce;
    [SerializeField] float initialTorque;
    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        LaunchBomb();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void LaunchBomb()
    {
        rb.AddForce(transform.up * launchForce);
        rb.AddTorque(new Vector3(initialTorque,initialTorque,initialTorque));
    }
}
