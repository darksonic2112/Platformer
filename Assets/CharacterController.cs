using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Threading;

public class CharacterController : MonoBehaviour
{
    public float acceleration = 10f;
    public float maxSpeed = 10f;
    public float jumpImpulse = 50f;
    public float jumpBoost = 3f;
    public GameManager timer;
    public bool isGrounded;
    public GameObject mario;
    private bool hasDied = false;

    private Rigidbody rbody;
    private Collider col;

    void Start()
    {
        rbody = GetComponent<Rigidbody>();
        col = GetComponent<Collider>();
    }

    void Update()
    {
        float horizontalMovement = Input.GetAxis("Horizontal");
        Vector3 movement = Vector3.right * horizontalMovement * acceleration;
        
        if (Mathf.Abs(rbody.velocity.x) < maxSpeed)
            rbody.AddForce(movement, ForceMode.Acceleration);
        
        if (isGrounded && Input.GetKeyDown(KeyCode.Space))
        {
            rbody.AddForce(Vector3.up * jumpImpulse, ForceMode.Impulse);
        }
        
        if (!isGrounded && Input.GetKey(KeyCode.Space) && rbody.velocity.y > 0)
        {
            rbody.AddForce(Vector3.up * jumpBoost * Time.deltaTime, ForceMode.Impulse);
            
        }
        
        if (rbody.velocity.y > 0 && !Input.GetKey(KeyCode.Space))
        {
            rbody.velocity = new Vector3(rbody.velocity.x, Mathf.Clamp(rbody.velocity.y, 0, jumpImpulse), rbody.velocity.z);
        }
        float yaw = (rbody.velocity.x > 0) ? 90 : -90;
        transform.rotation = Quaternion.Euler(0f, yaw, 0f);
        UpdateAnimationParameters(horizontalMovement);
        
    }

    private void OnCollisionEnter(Collision other)
    {
        if (!hasDied && other.gameObject.CompareTag("Water"))
        {
            mario.transform.position = new Vector3(21, 2, 0);
            Thread.Sleep(1000);
            timer.ResetTimer();
        }
    }

    void UpdateAnimationParameters(float horizontalMovement)
    {
        float speed = Mathf.Abs(horizontalMovement * acceleration);
        GetComponent<Animator>().SetFloat("Speed", speed);
        GetComponent<Animator>().SetBool("InAir", !isGrounded);
    }

    void FixedUpdate()
    {
        UpdateGroundedStatus();
    }

    void UpdateGroundedStatus()
    {
        float halfHeight = col.bounds.extents.y + 0.1f;
        Vector3 startPoint = transform.position;
        Vector3 endPoint = startPoint + Vector3.down * halfHeight;

        isGrounded = Physics.Raycast(startPoint, Vector3.down, halfHeight);
        Color lineColor = (isGrounded) ? Color.red : Color.blue;
        Debug.DrawLine(startPoint, endPoint, lineColor, 0f, false);
    }
}
