using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ToCameraRotator : MonoBehaviour
{
    private Transform _mainCameraTransform;

    private void Start()
    {
        _mainCameraTransform = Camera.main.transform;
    }

    private void Update()
    {
        transform.LookAt(_mainCameraTransform);
    }
}
