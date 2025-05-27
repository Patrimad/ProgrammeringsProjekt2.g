using System;
using UnityEngine;

public class Bilboard : MonoBehaviour
{
    public Transform cam;

    private void Start()
    {
        cam = Camera.main.transform;
    }

    void LateUpdate()
    {
        transform.LookAt(transform.position + cam.forward);
    }
}
