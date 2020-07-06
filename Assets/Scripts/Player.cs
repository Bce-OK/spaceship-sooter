using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class Player : MonoBehaviour
{
    [SerializeField] private GameObject bullet;
    [SerializeField] private Transform shotPoint;
    [SerializeField] private float fireSpeed;
    
    [SerializeField] private int lifes;

    private float prev_x;
    private float lastTime = 0;
    // Start is called before the first frame update
    void Start()
    {
        if (fireSpeed <= 0.0001f)
            fireSpeed = 0.1f;
        if (shotPoint == null)
            shotPoint = transform;
        if (bullet == null)
            bullet = GameObject.Find("bullet");
        if (Input.mousePresent)
            prev_x = Input.mousePosition.x;
    }

    private void FixedUpdate()
    {
        if (Input.touchSupported)
        {
            if (Input.touches.Length > 0
                && Math.Abs(Input.touches[0].deltaPosition.x) > 0.001)
            {
                Vector3 pos = transform.position;
                pos.x += Input.touches[0].deltaPosition.x * Time.deltaTime;
                transform.position = pos;
            }
        }
        else
        {
            if (Input.GetMouseButtonDown(0))
            {
                Vector3 pos = transform.position;
                pos.x += (Input.mousePosition.x - prev_x) * Time.deltaTime;
                transform.position = pos;
                prev_x = Input.mousePosition.x;
            }
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (fireSpeed < Time.time - lastTime)
        {
            Instantiate(bullet, shotPoint.position, shotPoint.rotation);
            lastTime = Time.time;
        }
    }
}
