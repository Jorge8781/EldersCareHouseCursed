using UnityEngine;

public class FirstPersonCamera : MonoBehaviour
{
    [Header("Camera")]
    public Camera playerCamera;

    [Header("Mouse Sensitivity")]
    public float sensitivity = 150f;

    [Header("Vertical Rotation")]
    public float minVerticalAngle = -75f;
    public float maxVerticalAngle = 75f;

    private float verticalRotation = 0f;

    void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }

    void Update()
    {
        float mouseX = Input.GetAxis("Mouse X") * sensitivity * Time.deltaTime;
        float mouseY = Input.GetAxis("Mouse Y") * sensitivity * Time.deltaTime;

       
        transform.Rotate(Vector3.up * mouseX);

     
        verticalRotation -= mouseY;

        verticalRotation = Mathf.Clamp(
            verticalRotation,
            minVerticalAngle,
            maxVerticalAngle
        );

        playerCamera.transform.localRotation =
            Quaternion.Euler(verticalRotation, 0f, 0f);
    }
}

