using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    // Want full 3d control of player like the car game
    // As well as ability to jump and shoot from pos of player and rotation of player

    public float speedLimit = 25.0f;
    public float rotateSpeed = 720.0f;
    public float speed = 50.0f;
    public float jumpForce = 600.0f;
    public float powerupStrength = 2.0f;
    private float gravityModifier = 3.0f;
    public int jumpCount = 2;
    private bool isOnGround = true;
    public bool hasPowerup = false;
    public bool gameOver = false;
    private Rigidbody playerRb;
    public GameObject bulletPrefab;
    public GameObject powerupIndicatorPrefab;
    private GameObject powerupIndicator;
    private GameObject spawnManager;

    // Start is called before the first frame update
    void Start()
    {
        playerRb = GetComponent<Rigidbody>();
        Physics.gravity *= gravityModifier;
        Cursor.lockState = CursorLockMode.Locked;
        spawnManager = GameObject.Find("Spawn Manager");
    }

    // Update is called once per frame
    void Update()
    {
        if(!gameOver && !spawnManager.GetComponent<SpawnManager>().inBetweenWaves)
        {
            CheckMovement();
            CheckShooting();
        }
        if(hasPowerup)
        {
            powerupIndicator.transform.position = transform.position;
        }
    }

    // Detects collisions with other objects
    void OnCollisionEnter(Collision collision)
    {
        if(collision.gameObject.CompareTag("Ground"))
        {
            isOnGround = true;
            jumpCount = 2;
        }

        if(collision.gameObject.CompareTag("Enemy"))
        {
            gameOver = true;
            Debug.Log("Game Over!");
        }
    }

    // Detects triggers with other objects
    void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.CompareTag("Powerup"))
        {
            if(!hasPowerup)
            {
                hasPowerup = true;
                Destroy(other.gameObject);
                powerupIndicator = Instantiate(powerupIndicatorPrefab,transform.position,powerupIndicatorPrefab.transform.rotation);
                StartCoroutine(PowerupCooldown());
            } else{
                hasPowerup = true;
                StartCoroutine(PowerupCooldown());
            }
        }
    }

    // Coroutine for powerup countdown
    IEnumerator PowerupCooldown()
    {
        yield return new WaitForSeconds(7);
        hasPowerup = false;
        Destroy(powerupIndicator);
    }

    // Controls/checks if the player can add more force to their movement, moves the player if so, also jumps
    void CheckMovement()
    {
        if(Input.GetKey(KeyCode.W) && playerRb.velocity.magnitude < speedLimit)
        {
            playerRb.AddRelativeForce(Vector3.forward * speed, ForceMode.Impulse);
        }

        if(Input.GetKey(KeyCode.A) && playerRb.velocity.magnitude < speedLimit)
        {
            playerRb.AddRelativeForce(Vector3.left * speed, ForceMode.Impulse);
        }

        if(Input.GetKey(KeyCode.S) && playerRb.velocity.magnitude < speedLimit)
        {
            playerRb.AddRelativeForce(Vector3.back * speed, ForceMode.Impulse);
        }

        if(Input.GetKey(KeyCode.D) && playerRb.velocity.magnitude < speedLimit)
        {
            playerRb.AddRelativeForce(Vector3.right * speed, ForceMode.Impulse);
        }

        if(Input.GetKeyDown(KeyCode.Space) && isOnGround && playerRb.velocity.magnitude < speedLimit)
        {
            if(!hasPowerup)
            {
                playerRb.AddForce(Vector3.up * jumpForce, ForceMode.Impulse);
                isOnGround = false;
            }
            if(hasPowerup)
            {
                playerRb.AddForce(Vector3.up * jumpForce * powerupStrength, ForceMode.Impulse);
                isOnGround = false;
            }
        }
         //Need a delay to check for second jump, if I add second jump; probably not with the jump powerup
    }

    void CheckShooting()
    {
        if(Input.GetMouseButtonDown(0))
        {
            Instantiate(bulletPrefab,transform.GetChild(0).position,transform.GetChild(0).rotation);
        }
    }
}
