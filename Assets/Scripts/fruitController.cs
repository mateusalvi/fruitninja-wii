using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class fruitController : MonoBehaviour
{
    [SerializeField] GameObject fruitSpawner;
    [SerializeField] int numberOfFruitModels = 9;
    [SerializeField] float initialTorque;
    [SerializeField] GameObject cutFruitPrefab;
    [SerializeField] int fruitScore;
    [SerializeField] float launchForce;
    private Rigidbody rb;
    private GameObject cutFruit;
    private Transform leftPiece;
    private Transform rightPiece;
    private Camera cam;
    private GameObject UI;
    public AudioClip spawnSound;
    int randomFruitModel;

    void Start()
    {
        cam = Camera.main;
        randomFruitModel = Random.Range(0,numberOfFruitModels);
        transform.GetChild(randomFruitModel).gameObject.SetActive(true);
        rb = GetComponent<Rigidbody>();
        Destroy(gameObject, 3);
        UI = GameObject.Find("UI");
    }

    void Update()
    {
        checkIfOutOfScreen();
    }

    private void OnTriggerEnter(Collider other) 
    {
        if (other.gameObject.layer == 3)
        {
            CutFruit();
        }
    }

    private void checkIfOutOfScreen()
    {
        if (transform.position.y < (1.2f * cam.ScreenToWorldPoint(new Vector3(Random.Range(0f, cam.pixelWidth), 0f, fruitSpawner.GetComponent<fruitSpawnerController>().spawnDistance)).y))
        {
            UI.GetComponent<uiController>().TakeDamage(1);
            Destroy(gameObject);
        }
    }

    private void CutFruit()
    {
        rb = gameObject.GetComponent<Rigidbody>();

        cutFruit = Instantiate(cutFruitPrefab, transform.position, Quaternion.identity);
        cutFruit.transform.rotation = gameObject.transform.rotation;

        Destroy(gameObject);

        leftPiece = cutFruit.gameObject.transform.GetChild(0);
        rightPiece = cutFruit.gameObject.transform.GetChild(1);

        leftPiece.transform.GetChild(randomFruitModel).gameObject.SetActive(true);
        rightPiece.transform.GetChild(randomFruitModel).gameObject.SetActive(true);

        Vector3 normalBetweenSlices = (rightPiece.transform.position - leftPiece.transform.position);
        normalBetweenSlices.Normalize();
        leftPiece.GetComponent<Rigidbody>().AddForce(-normalBetweenSlices*50);
        rightPiece.GetComponent<Rigidbody>().AddForce(normalBetweenSlices*50);

        UI.GetComponent<uiController>().ChangeScore(fruitScore);
    }

    public void LaunchFruit()
    {
        GetComponent<AudioSource>().PlayOneShot(spawnSound, 1.0f);
        rb = GetComponent<Rigidbody>();
        rb.AddForce(Vector3.up * launchForce);
        rb.AddTorque(new Vector3(Random.value * initialTorque, Random.value * initialTorque, Random.value * initialTorque));
    }
}
