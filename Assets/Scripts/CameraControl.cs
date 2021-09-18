using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraControl : MonoBehaviour
{
    [SerializeField] Transform player;

    public float sensitivity = 1;
    float mouseX;
    float mouseY;

    private void Start()
    {
        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Locked;
    }

    void ControlCamera()
    {
        mouseX += Input.GetAxis("Mouse X") * sensitivity;
        mouseY += Input.GetAxis("Mouse Y") * sensitivity;
        mouseY = Mathf.Clamp(mouseY, -35, 60);

        player.rotation = Quaternion.Euler(0, mouseX, 0);
        transform.rotation = Quaternion.Euler(-mouseY, mouseX, 0);
    }

    // Update is called once per frame
    void Update()
    {
        ControlCamera();
    }
}
