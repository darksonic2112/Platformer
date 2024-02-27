// Coyote Timer and Jump Buffer: https://www.youtube.com/watch?v=RFix_Kg2Di0 
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Threading;
using UnityEngine.UIElements;

public class CharacterController : MonoBehaviour
{
    public float acceleration = 10f;
    public float maxSpeed = 10f;
    public float jumpImpulse = 50f;
    public float jumpBoost = 3f;
    public GameManager timer;
    public bool isGrounded;
    public bool headRay;
    public GameObject mario;
    private bool hasDied = false;
    private AudioSource audioSource2;
    private AudioSource audioSource;
    public AudioClip musicClip;
    public AudioClip endMusic;
    private float waitTime = 5f;
    public LevelParser level;

    private Rigidbody rbody;
    private Collider col;
    private float coyoteTime = 0.2f;
    private float coyoteTimeCounter;
    
    private float jumpBufferTime = 1f;
    private float jumpBufferCounter;
    
    public float maxFallSpeed = 15f;

    void Start()
    {
        rbody = GetComponent<Rigidbody>();
        col = GetComponent<Collider>();

        audioSource = gameObject.AddComponent<AudioSource>();
        audioSource.clip = musicClip;
        audioSource.volume = 0.1f;
        audioSource.Play();

        audioSource2 = gameObject.AddComponent<AudioSource>();
        audioSource2.clip = endMusic;
        audioSource2.volume = 0.1f;
    }

    void Update()
    {
        float horizontalMovement = Input.GetAxis("Horizontal");
        Vector3 movement = Vector3.right * horizontalMovement * acceleration;

        if (Input.GetKeyDown(KeyCode.LeftShift) && Mathf.Abs(rbody.velocity.x) < maxSpeed * 2f)
        {
            maxSpeed *= 2;
            rbody.AddForce(movement, ForceMode.Acceleration);
        }
        else if (Input.GetKeyUp(KeyCode.LeftShift) && Mathf.Abs(rbody.velocity.x) < maxSpeed * 2)
        {
            maxSpeed = 10f;
            rbody.AddForce(movement, ForceMode.Acceleration);
        }

        if (isGrounded)
        {
            coyoteTimeCounter = coyoteTime;
        }
        else
        {
            coyoteTimeCounter -= Time.deltaTime;
        }

        if (Input.GetKeyDown(KeyCode.Space))
        {
            jumpBufferCounter = jumpBufferTime;
        }
        else
        {
            jumpBufferCounter -= Time.deltaTime;
        }
        
        if (Mathf.Abs(rbody.velocity.x) < maxSpeed)
        {
            rbody.AddForce(movement, ForceMode.Acceleration);
        }

        if (coyoteTimeCounter > 0 && jumpBufferCounter > 0f)
        {
            rbody.AddForce(Vector3.up * jumpImpulse, ForceMode.Impulse);
            jumpBufferCounter = 0f;
        }

        if (!(coyoteTimeCounter > 0) && jumpBufferCounter > 0f && rbody.velocity.y > 0)
        {
            rbody.AddForce(Vector3.up * jumpBoost * Time.deltaTime, ForceMode.Impulse);
            jumpBufferCounter = 0f;
        }

        if (rbody.velocity.y > 0 && !Input.GetKey(KeyCode.Space))
        {
            rbody.velocity = new Vector3(rbody.velocity.x, Mathf.Clamp(rbody.velocity.y, 0, jumpImpulse),
                rbody.velocity.z);
        }

        if (Input.GetKeyUp(KeyCode.Space))
        {
            coyoteTimeCounter = 0f;
        }
        
        if (!isGrounded && rbody.velocity.y < 0)
        {
            rbody.velocity = new Vector3(rbody.velocity.x, Mathf.Clamp(rbody.velocity.y, -maxFallSpeed, Mathf.Infinity), rbody.velocity.z);
        }

        float yaw = (rbody.velocity.x > 0) ? 90 : -90;
        transform.rotation = Quaternion.Euler(0f, yaw, 0f);
        UpdateAnimationParameters(horizontalMovement);

        if (Input.GetKeyDown(KeyCode.Space))
        {
            float radius = 0.3f;
            float distance = 1.0f;
            float yOffset = 1f;
            
            RaycastHit hit;
            Vector3 origin = transform.position + Vector3.up * (col.bounds.extents.y + yOffset);
            Vector3 direction = Vector3.up;
            if (Physics.SphereCast(origin, radius, direction, out hit, distance))
            {
                if (hit.collider == null)
                {
                    Debug.Log("Object is null");
                }

                if (hit.collider.CompareTag("Brick"))
                {
                    BreakBrick(hit.collider.gameObject);
                }
                else if (hit.collider.CompareTag("Coin"))
                {
                    CollectCoin(hit.collider.gameObject);
                }
                else if (hit.collider.CompareTag("Coins"))
                {
                    CollectCoin(hit.collider.gameObject);
                }
                else
                {
                    Debug.Log("Object not found");
                }
            }
        }
    }


private void OnCollisionEnter(Collision other)
    {
        if (!hasDied && other.gameObject.CompareTag("Water"))
        {
            mario.transform.position = new Vector3(21, 2, 0);
            audioSource.Stop();
            audioSource.Play();
            timer.ResetTimer();
            level.LoadLevel();
        }

        if (other.gameObject.CompareTag("Goal"))
        {
            audioSource.Stop();
            audioSource2.Play();
            Debug.Log("You have completed the Level!");
            timer.ResetTimer();
        }
        
    }

    void UpdateAnimationParameters(float horizontalMovement)
    {
        float speed = Mathf.Abs(horizontalMovement * acceleration);
        GetComponent<Animator>().SetFloat("Speed", Math.Abs(rbody.velocity.x));
        GetComponent<Animator>().SetBool("InAir", !isGrounded);
    }

    void FixedUpdate()
    {
        UpdateGroundedStatus();
    }

    void UpdateGroundedStatus()
    {
        float halfHeight = col.bounds.extents.y - 0.1f;
        Vector3 startPoint = transform.position;
        Vector3 endPoint = startPoint + Vector3.down * halfHeight;
        
        float halfHeightHead = col.bounds.extents.y + 0.9f;
        Vector3 startPointHead = transform.position;
        Vector3 endPointHead = startPoint + Vector3.up * halfHeightHead;

        isGrounded = Physics.Raycast(startPoint, Vector3.down, halfHeight);
        Color lineColor = (isGrounded) ? Color.red : Color.blue;
        Debug.DrawLine(startPoint, endPoint, lineColor, 0f, false);
        
        headRay = Physics.Raycast(startPointHead, Vector3.down, halfHeightHead);
        Color lineColorHead = (isGrounded) ? Color.green : Color.red;
        Debug.DrawLine(startPointHead, endPointHead, lineColorHead, 0f, false);
    }
    
    private void BreakBrick(GameObject brick)
    {
        timer.UpdateScore();
        Destroy(brick);
    }

    private void CollectCoin(GameObject coin)
    {
        timer.UpdateScore();
        timer.UpdateCoins();
        Destroy(coin);
    }
}