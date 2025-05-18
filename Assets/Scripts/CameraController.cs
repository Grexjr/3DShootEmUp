using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    public GameObject player;
    public float rotateSpeed;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(!player.GetComponent<PlayerController>().gameOver)
        {
            transform.position = player.transform.position;
            CheckCameraInput();
        }
    }

    void CheckCameraInput()
    {
        if(Input.GetAxis("Mouse X") > 0)
        {
            // Rotates the camera right if mouse is moved to the right
            transform.Rotate(Vector3.up, rotateSpeed * Time.deltaTime);
            player.transform.Rotate(Vector3.up, rotateSpeed * Time.deltaTime);
        }
        if(Input.GetAxis("Mouse X") < 0)
        {
            // Rotates the camera left if mouse is moved to the left
            transform.Rotate(Vector3.up, -rotateSpeed * Time.deltaTime);
            player.transform.Rotate(Vector3.up, -rotateSpeed * Time.deltaTime);
        }

        /*  Will work on this later; up down camera movement with limiters
            if(Input.GetAxis("Mouse Y") > 0)
        {
            // Rotates the camera up if the mouse is moved up
            transform.Rotate(Vector3.left, rotateSpeed * Time.deltaTime);
            player.transform.Rotate(Vector3.left, rotateSpeed * Time.deltaTime);
        }
        if(Input.GetAxis("Mouse Y") < 0)
        {
            // Rotates the camera down if the mouse is moved down
            transform.Rotate(Vector3.right, rotateSpeed * Time.deltaTime);
            player.transform.Rotate(Vector3.right, rotateSpeed * Time.deltaTime);
        } */
    }
}
