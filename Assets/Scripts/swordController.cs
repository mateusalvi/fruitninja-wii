using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class swordController : MonoBehaviour
{
    [SerializeField] float distanceFromCamera;
    private Camera cam;
    Vector3 mousePos = new Vector3();
    public GameObject bladeTrailPrefab;
    GameObject currentBladeTrail;
    Vector3 previousPosition;
    public float minCuttingVelocity = .001f;
    SphereCollider sphereCollider;
	private float velocity;

    void Start()
    {
        currentBladeTrail = null;
        sphereCollider = GetComponent<SphereCollider>();
        cam = Camera.main;
        mousePos = Input.mousePosition;
        previousPosition = mousePos;
    }

    private void FixedUpdate() {
        UpdateCut();
    }

    void Update()
    {

    }

    void UpdateCut ()
	{
        mousePos = Input.mousePosition;

        Vector3 newPos = cam.ScreenToWorldPoint(new Vector3 (mousePos.x, mousePos.y, distanceFromCamera));
        transform.position = newPos;

        velocity = (mousePos - previousPosition).magnitude * Time.deltaTime;

		if (velocity > minCuttingVelocity)
		{
            StartCutting();
		} else
		{
            StopCutting();
		}

		previousPosition = mousePos;
	}

    void StartCutting ()
	{
        sphereCollider.enabled = true;
        if (currentBladeTrail == null)
        {
            currentBladeTrail = Instantiate(bladeTrailPrefab, transform);
        }
        else
        {
            currentBladeTrail.transform.position = transform.position;
        }
	}

	void StopCutting ()
	{
		sphereCollider.enabled = false;
		if(currentBladeTrail != null)
        {
            currentBladeTrail.transform.SetParent(null);
		    Destroy(currentBladeTrail, 2f);
            currentBladeTrail = null;
        }
	}
}