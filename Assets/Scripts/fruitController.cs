using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class fruitController : MonoBehaviour
{
    Rigidbody rb;
    [SerializeField] int numberOfChilds = 9;
    int randomFruit;
    [SerializeField] float launchForce;
    [SerializeField] float initialTorque;
    private Camera cam;
    // Start is called before the first frame update
    void Start()
    {
        cam = Camera.main;
        randomFruit = Random.Range(0,numberOfChilds);
        transform.GetChild(randomFruit).gameObject.SetActive(true);
        rb = GetComponent<Rigidbody>();
        LaunchFruit();
        rb.AddTorque(new Vector3(initialTorque,initialTorque,initialTorque));
        Destroy(gameObject, 5);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnCollisionEnter(Collision other) 
    {
        // TODO: IF OTHER = ESPADA
        Destroy(gameObject);
    }

    private void LaunchFruit()
    {
        rb.AddForce(transform.up * launchForce);
    }
}
