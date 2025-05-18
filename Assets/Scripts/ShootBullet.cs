using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class ShootBullet : MonoBehaviour
{
    public float bulletSpeed = 100.0f;
    private float bulletRange = 100.0f;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        transform.Translate(Vector3.forward * bulletSpeed * Time.deltaTime);
        CheckBulletPosition();
    }

    void CheckBulletPosition()
    {
        Vector3 distance = transform.position;

        if(Vector3.Distance(distance,Vector3.zero) >= bulletRange)
        {
            Destroy(gameObject);
        }
    }
}
