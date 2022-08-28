using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class swordController : MonoBehaviour
{
    //[SerializeField] float distanceFromCamera;
    private Camera cam;
    Vector3 mousePos = new Vector3();
    private Rigidbody rb;
    private float rotx = 0;
    private float  roty  = 0;
    private float  rotz  = 0;
    private float  rotw  = 0;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
        cam = Camera.main;
    }

    void Update()
    {
        //#mousePos = Input.mousePosition;
        //transform.position = cam.ScreenToWorldPoint(new Vector3 (mousePos.x, mousePos.y, distanceFromCamera));
        rotx = Input.acceleration.x;
        roty = Input.acceleration.y;
        rotz = Input.acceleration.z;
        transform.rotation = new Quaternion(rotx, roty, rotz, rotw);

        //Vector3 tilt = Input.gyro;

        //rb.AddTorque(tilt);
    }

}