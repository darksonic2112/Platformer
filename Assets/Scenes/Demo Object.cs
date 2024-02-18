using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class DemoObject : MonoBehaviour
{
    public GameObject package;
    public GameObject parasuite;
    public float deploymentHeight = 7.5f;
    public float parasuiteDrag = 5f;
    public float landingHeight = 1f;

    private float originalDrag;
    
    
    // Start is called before the first frame update
    void Start()
    {
        originalDrag = package.GetComponent<Rigidbody>().drag;
        parasuite.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        if (Physics.Raycast(package.transform.position, Vector3.down, out var hitInfo, deploymentHeight))
        {
            package.GetComponent<Rigidbody>().drag = parasuiteDrag;
            parasuite.SetActive(true);
            Debug.DrawRay(package.transform.position, Vector3.down * deploymentHeight, Color.red);

            if (hitInfo.distance < landingHeight)
            {
                Debug.Log("landed");
                parasuite.SetActive(false);
            }
        }
        else
        {
            package.GetComponent<Rigidbody>().drag = originalDrag;
            parasuite.SetActive(false);
            Debug.DrawRay(package.transform.position, Vector3.down * deploymentHeight, Color.cyan);
        }

    }
}
