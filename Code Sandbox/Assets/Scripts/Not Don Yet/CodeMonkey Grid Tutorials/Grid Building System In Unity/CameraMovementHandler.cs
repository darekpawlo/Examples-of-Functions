using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraMovementHandler : MonoBehaviour
{
    private Camera camera;

    private void Start()
    {
        camera = Camera.main;
    }

    // Update is called once per frame
    void Update()
    {
        HandleMovement();
    }

    private void HandleMovement()
    {
        float x = Input.GetAxisRaw("Horizontal");
        float y = Input.GetAxisRaw("Vertical");

        Vector3 moveDir = new Vector3(x, y).normalized;
        float moveSpeed = 30;

        transform.position += moveDir * moveSpeed * Time.deltaTime;
    }
}
