using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraBehavior : MonoBehaviour
{
    [SerializeField] Transform player;
    [SerializeField] float mouseSensitivity = 100f;
    [SerializeField] float distanceFromPlayer = 5f;
    [SerializeField] float cameraHeight = 2f;

    [SerializeField] float minVerticalRotation = 0f;
    [SerializeField] float maxVerticalRotation = 60f;

    private float xRotation = 0f;
    private float yRotation = 0f;

    void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
    }

    void LateUpdate()
    {
        float mouseX = Input.GetAxis("Mouse X") * mouseSensitivity * Time.deltaTime;
        float mouseY = Input.GetAxis("Mouse Y") * mouseSensitivity * Time.deltaTime;

        yRotation += mouseX;
        xRotation -= mouseY;
        xRotation = Mathf.Clamp(xRotation, minVerticalRotation, maxVerticalRotation); //clamp vert. rotation to avoid clipping.

        // Rotate the player based on horizontal mouse movement
        player.rotation = Quaternion.Euler(0f, yRotation, 0f);

        // Calculate the new camera position
        Vector3 cameraOffset = new Vector3(0, cameraHeight, -distanceFromPlayer);
        Quaternion rotation = Quaternion.Euler(xRotation, yRotation, 0f);
        transform.position = player.position + rotation * cameraOffset;

        // Ensure the camera is always looking at the player
        transform.LookAt(player.position + Vector3.up * cameraHeight);
    }
}
