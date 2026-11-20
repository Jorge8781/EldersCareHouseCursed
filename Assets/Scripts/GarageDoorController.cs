using UnityEngine;

public class GarageDoorController : MonoBehaviour
{
    [Header("Garage Door")]
    public float openHeight = 2.5f;
    public float openSpeed = 2f;

    private Vector3 closedPosition;
    private Vector3 openPosition;

    private bool isOpen;

    void Start()
    {
        closedPosition = transform.localPosition;

        openPosition = closedPosition +
                       Vector3.up * openHeight;
    }

    void Update()
    {
        Vector3 targetPosition =
            isOpen ? openPosition : closedPosition;

        transform.localPosition = Vector3.Lerp(
            transform.localPosition,
            targetPosition,
            Time.deltaTime * openSpeed
        );
    }

    public void ToggleDoor()
    {
        isOpen = !isOpen;
    }
}

