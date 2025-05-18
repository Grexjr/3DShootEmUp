using System.Collections;
using System.Collections.Generic;
using JetBrains.Annotations;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.EventSystems;

public class FollowPlayer : MonoBehaviour
{
    public float speed = 500.0f;
    public float ramSpeed = 2000.0f;
    public float ramDistance = 10;
    public bool canChaseDown = false;
    private GameObject player;
    private Rigidbody objectRb;
    // Start is called before the first frame update
    void Start()
    {
        objectRb = GetComponent<Rigidbody>();
        player = GameObject.Find("Player");
    }

    // Update is called once per frame
    void Update()
    {
        if(!player.GetComponent<PlayerController>().gameOver)
        {
            objectRb.AddForce(MoveDirection() * speed);
            // CheckChaseDown();
        }
    }

    Vector3 MoveDirection()
    {
        Vector3 movement = (player.transform.position - transform.position).normalized;
        movement.y = 0;
        return movement;
    }

    /* void CheckChaseDown()
    {
        if(Vector3.Distance(player.transform.position,transform.position) < 5)
        {
            canChaseDown = true;
            Vector3 ramMovement = (player.transform.position - transform.position).normalized;
            objectRb.AddForce(ramMovement * ramSpeed);
        }
    } */
    // First will add enemy ramming into the player when they are close, then return to some default randomized y value
    // Will also change the time between powerup spawns to be random between 15-20 seconds
    // Will also give the player a dash to move them further in their current direction (including up!)
    // One day far in future will add environment/platforms that can spawn at random positions each wave to shake things up
}
