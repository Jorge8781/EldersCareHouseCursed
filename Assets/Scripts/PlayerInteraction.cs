using UnityEngine;
using TMPro;

public class PlayerInteraction : MonoBehaviour
{
    [Header("References")]
    public Camera playerCamera;
    public TMP_Text interactionPrompt;

    [Header("Settings")]
    public float interactionRange = 3f;
    public LayerMask pickupLayer;

    [Header("Object Pickup")]
    public float holdDistance = 1.8f;
    public float moveSpeed = 10f;

    private Rigidbody heldObject;

    void Update()
    {
        if (heldObject != null)
        {
            interactionPrompt.text = "Pulsa E para dejar";

            HoldObject();

            if (Input.GetKeyDown(KeyCode.E))
                DropObject();

            return;
        }

        DetectInteraction();

        if (Input.GetKeyDown(KeyCode.E))
            Interact();
    }

    void DetectInteraction()
    {
        interactionPrompt.text = "";

        Ray ray = new Ray(
            playerCamera.transform.position,
            playerCamera.transform.forward
        );

        if (Physics.Raycast(ray, out RaycastHit hit, interactionRange))
        {
            if (hit.collider.GetComponentInParent<GarageDoorController>() != null)
            {
                interactionPrompt.text = "Pulsa E para abrir/cerrar garaje";
            }
            else if (hit.collider.GetComponentInParent<DoorController>() != null)
            {
                interactionPrompt.text = "Pulsa E para abrir/cerrar";
            }
            else if (((1 << hit.collider.gameObject.layer) & pickupLayer.value) != 0
                     && hit.collider.GetComponentInParent<Rigidbody>() != null)
            {
                interactionPrompt.text = "Pulsa E para coger";
            }
        }
    }

    void Interact()
    {
        Ray ray = new Ray(
            playerCamera.transform.position,
            playerCamera.transform.forward
        );

        if (Physics.Raycast(ray, out RaycastHit hit, interactionRange))
        {
            GarageDoorController garage =
                hit.collider.GetComponentInParent<GarageDoorController>();

            if (garage != null)
            {
                garage.ToggleDoor();
                return;
            }

            DoorController door =
                hit.collider.GetComponentInParent<DoorController>();

            if (door != null)
            {
                door.ToggleDoor();
                return;
            }

            if (((1 << hit.collider.gameObject.layer) & pickupLayer.value) != 0)
            {
                Rigidbody rb = hit.collider.GetComponentInParent<Rigidbody>();

                if (rb != null)
                    PickupObject(rb);
            }
        }
    }

    void PickupObject(Rigidbody rb)
    {
        heldObject = rb;
        heldObject.useGravity = false;
        heldObject.linearDamping = 10f;
    }

    void HoldObject()
    {
        Vector3 targetPosition =
            playerCamera.transform.position +
            playerCamera.transform.forward * holdDistance;

        Vector3 direction = targetPosition - heldObject.position;

        heldObject.linearVelocity = direction * moveSpeed;
    }

    void DropObject()
    {
        heldObject.useGravity = true;
        heldObject.linearDamping = 0f;
        heldObject = null;
    }
}

