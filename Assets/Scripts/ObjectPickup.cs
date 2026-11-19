
using UnityEngine;

public class ObjectPickup : MonoBehaviour
{
    [Header("Camera")]
    public Transform playerCamera;

    [Header("Pickup Settings")]
    public float pickupRange = 2.5f;
    public float holdDistance = 1.8f;
    public float minHoldDistance = 1f;
    public float maxHoldDistance = 3f;
    public float scrollSensitivity = 1f;

    [Header("Object Detection")]
    public LayerMask pickupLayer;

    private Rigidbody heldObject;

    private bool originalUseGravity;
    private bool originalIsKinematic;
    private float originalLinearDamping;
    private float originalAngularDamping;

    void Start()
    {
        if (playerCamera == null && Camera.main != null)
        {
            playerCamera = Camera.main.transform;
        }

        if (playerCamera == null)
        {
            Debug.LogError("ObjectPickup: No se ha asignado la cámara del jugador.");
        }
    }

    void Update()
    {
        if (playerCamera == null)
            return;

        if (Input.GetKeyDown(KeyCode.E))
        {
            if (heldObject == null)
            {
                TryPickupObject();
            }
            else
            {
                DropObject();
            }
        }

        if (heldObject != null)
        {
            AdjustHoldDistance();
        }
    }

    void FixedUpdate()
    {
        if (heldObject != null)
        {
            HoldObject();
        }
    }

    void TryPickupObject()
    {
        Ray ray = new Ray(playerCamera.position, playerCamera.forward);
        RaycastHit hit;

        if (Physics.Raycast(ray, out hit, pickupRange, pickupLayer))
        {
            Rigidbody rb = hit.collider.GetComponentInParent<Rigidbody>();

            if (rb != null && !rb.isKinematic)
            {
                heldObject = rb;

                originalUseGravity = rb.useGravity;
                originalIsKinematic = rb.isKinematic;
                originalLinearDamping = rb.linearDamping;
                originalAngularDamping = rb.angularDamping;

                rb.linearVelocity = Vector3.zero;
                rb.angularVelocity = Vector3.zero;
                rb.useGravity = false;
                rb.isKinematic = true;
            }
        }
    }

    void AdjustHoldDistance()
    {
        float scroll = Input.GetAxis("Mouse ScrollWheel");

        if (scroll != 0)
        {
            holdDistance += scroll * scrollSensitivity;

            holdDistance = Mathf.Clamp(
                holdDistance,
                minHoldDistance,
                maxHoldDistance
            );
        }
    }

    void HoldObject()
    {
        Vector3 targetPosition =
            playerCamera.position + playerCamera.forward * holdDistance;

        heldObject.MovePosition(targetPosition);
    }

    void DropObject()
    {
        if (heldObject == null)
            return;

        heldObject.isKinematic = originalIsKinematic;
        heldObject.useGravity = originalUseGravity;
        heldObject.linearDamping = originalLinearDamping;
        heldObject.angularDamping = originalAngularDamping;

        heldObject = null;
    }
}

