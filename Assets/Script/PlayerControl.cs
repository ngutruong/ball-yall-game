﻿using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerControl : MonoBehaviour {

    private const float Y_ANGLE_MIN = -0.1f;
    private const float Y_ANGLE_MAX = 50.0f;

    [SerializeField]
    private float speed;

    [SerializeField]
    GameObject followMe;

    [SerializeField]
    GameObject camera;

    private Rigidbody rigidbody;
    private Vector3 offset;

    private float mouseX = 0.0f;
    private float mouseY = 0.0f;

    void Start()
    {
        offset = gameObject.transform.position - followMe.transform.position;
        rigidbody = GetComponent<Rigidbody>();
    }
	
	void FixedUpdate() {
        float moveHorizontal = Input.GetAxis("Horizontal");
        float moveVertical = Input.GetAxis("Vertical");

        Vector3 movement = new Vector3(moveHorizontal, 0.0f, moveVertical);
        rigidbody.AddForce(movement * speed);
        followMe.transform.position = gameObject.transform.position;

        mouseX += Input.GetAxis("Mouse X");
        mouseY += Input.GetAxis("Mouse Y");

        mouseY = Mathf.Clamp(mouseY, Y_ANGLE_MIN, Y_ANGLE_MAX);

        
    }
    void LateUpdate()
    {
        Vector3 dir = new Vector3(0, 0, -3.5f);
        Quaternion rotation = Quaternion.Euler(mouseY, mouseX, 0);
        camera.transform.position = gameObject.transform.position + rotation * dir;

        camera.transform.LookAt(gameObject.transform.position);
    }
}
