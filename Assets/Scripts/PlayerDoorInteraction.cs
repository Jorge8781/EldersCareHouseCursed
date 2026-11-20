using UnityEngine;

public class PlayerDoorInteraction : MonoBehaviour
{
    public Camera playerCamera;
    public float interactionRange = 3f;

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.E))
        {
            Ray ray = new Ray(
                playerCamera.transform.position,
                playerCamera.transform.forward
            );

            RaycastHit hit;

            if (Physics.Raycast(ray, out hit, interactionRange))
            {
                GarageDoorController garageDoor =
                    hit.collider.GetComponentInParent<GarageDoorController>();

                if (garageDoor != null)
                {
                    garageDoor.ToggleDoor();
                    return;
                }

                DoorController door =
                    hit.collider.GetComponentInParent<DoorController>();

                if (door != null)
                {
                    door.ToggleDoor();
                }
            }
        }
    }
}


