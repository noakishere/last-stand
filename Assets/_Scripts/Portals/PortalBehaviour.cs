using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PortalBehaviour : MonoBehaviour
{
    [SerializeField] private Transform player;
    [SerializeField] private Transform targetTransform;

    public Transform receiver;
    private bool playerIsOverlapping = false;

    private void LateUpdate()
    {
        if (playerIsOverlapping)
        {
            Vector3 portalToPlayer = player.position - transform.position;
            float dotProduct = Vector3.Dot(transform.up, portalToPlayer);

            // If this is true: The player has moved across the portal
            if (dotProduct < 0f)
            {
                // Teleport him!
                float rotationDiff = -Quaternion.Angle(transform.rotation, receiver.rotation);
                //rotationDiff += 180;
                player.Rotate(Vector3.up, rotationDiff);

                Vector3 positionOffset = Quaternion.Euler(0f, rotationDiff, 0f) * portalToPlayer;
                player.position = receiver.position + positionOffset;

                playerIsOverlapping = false;
            }
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.tag == "Player")
        {
            playerIsOverlapping = true;
            Debug.Log($"Enter {gameObject}");
            //other.transform.position = targetTransform.position;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.tag == "Player")
        {
            playerIsOverlapping = false;
            Debug.Log($"Exit from {gameObject}");
            //other.transform.position = targetTransform.position;
        }
    }
}
