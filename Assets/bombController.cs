using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class bombController : MonoBehaviour
{
    Rigidbody rb;
    [SerializeField] float launchForce;
    [SerializeField] float initialTorque;
    [SerializeField] GameObject explosionPrefab;
    [SerializeField] GameObject UI;
    [SerializeField] GameObject bombSpawner;
    private Camera cam;
    // Start is called before the first frame update
    void Start()
    {
        cam = Camera.main;
        UI = GameObject.Find("UI");
        rb = GetComponent<Rigidbody>();
        Destroy(gameObject, 3);
    }

    // Update is called once per frame
    void Update()
    {
        if (transform.position.y < (1.2f * cam.ScreenToWorldPoint(new Vector3(Random.Range(0f, cam.pixelWidth), 0f, bombSpawner.GetComponent<bombSpawnerController>().spawnDistance)).y))
        {
            GetComponent<AudioSource>().mute = true;
            Destroy(gameObject);
        }
    }

    private void OnTriggerEnter(Collider other) 
    {
        if (other.gameObject.layer == 3)
        {
            ExplodeBomb();
        }
    }

    public void LaunchBomb()
    {
        rb = GetComponent<Rigidbody>();
        rb.AddForce(transform.up * launchForce);
        rb.AddTorque(new Vector3(Random.value * initialTorque, Random.value * initialTorque, Random.value * initialTorque));
    }

    private void ExplodeBomb()
    {
        UI.GetComponent<uiController>().TakeDamage(1000);
        Instantiate(explosionPrefab, transform.position, Quaternion.identity);
        Destroy(gameObject);
    }
}
