using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class swordController : MonoBehaviour
{
    [SerializeField] float distanceFromCamera;
    private Camera cam;
    Vector3 mousePos = new Vector3();

    void Start()
    {
        cam = Camera.main;
    }

    void Update()
    {
        mousePos = Input.mousePosition;
        transform.position = cam.ScreenToWorldPoint(new Vector3 (mousePos.x, mousePos.y, distanceFromCamera));
        
    }
}