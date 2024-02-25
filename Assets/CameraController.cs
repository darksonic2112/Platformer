using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    public GameObject mario;
    private Vector3 offset;
    
    void Start()
    {
        offset = transform.position - mario.transform.position;
    }
    
    void Update()
    {
        float currentY = transform.position.y;
        float currentZ = transform.position.z;
        
        transform.position = new Vector3(mario.transform.position.x + offset.x, currentY, currentZ);
    }
}
