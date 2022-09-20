using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class fruitSpawnerController : MonoBehaviour
{
    [SerializeField] GameObject fruitPrefab;
    [SerializeField] float spawnDelay;
    [SerializeField] public float spawnDistance;
    [SerializeField] float firstSpawnDelay;
    private Camera cam;
    private GameObject fruit;
    float nextSpawn;
    Vector3 nextSpawnPosition = new Vector3();
    
    // Start is called before the first frame update
    void Start()
    {
        cam = Camera.main;
        nextSpawn = Time.time + firstSpawnDelay;
    }

    // Update is called once per frame
    void Update()
    {
        if (Time.time >= nextSpawn)
        {
            nextSpawnPosition = cam.ScreenToWorldPoint(new Vector3(Random.Range(0f, cam.pixelWidth), 0f, spawnDistance));
            nextSpawn = Time.time + spawnDelay;
            // Instantiate(fruitPrefab, transform.position, Quaternion.identity);
            fruit = Instantiate(fruitPrefab, nextSpawnPosition, Quaternion.identity);
            fruit.GetComponent<fruitController>().LaunchFruit();
        }
    }

    private void OnDrawGizmos() 
    {
        cam = Camera.main;
        Gizmos.color = Color.yellow;

        Gizmos.DrawLine(cam.ScreenToWorldPoint(new Vector3(0f, 0f, spawnDistance)), cam.ScreenToWorldPoint(new Vector3(cam.pixelWidth, 0f, spawnDistance)));
    }
}
