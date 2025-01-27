﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    public Transform target;
    public float smoothing;

    Vector3 offset;

    void Start()
    {
        //find offset position between camera and target
        offset = transform.position - target.position;
    }

    void FixedUpdate()
    {
        //calculate the position of camera
        Vector3 camPosition = target.position + offset;
        //move the camera to position
        transform.position = Vector3.Lerp(transform.position, camPosition, smoothing);
    }
}
