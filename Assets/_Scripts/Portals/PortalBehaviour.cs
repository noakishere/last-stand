using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[DefaultExecutionOrder(100)]
public class PortalBehaviour : MonoBehaviour
{
    [SerializeField] private Transform player;
    [SerializeField] private Transform targetTransform;

    [Tooltip("Seconds before the player can teleport again")]
    [SerializeField] private float teleportCooldown = 0.5f;

    public Transform receiver;
    private bool playerIsOverlapping = false;
    private float lastTeleportTime = -Mathf.Infinity;
    [SerializeField] private CharacterController cc;

    private void Start()
    {
        //transform.position = cc.transform.position;
    }

    private void LateUpdate()
    {
        if (playerIsOverlapping && Time.time >= lastTeleportTime + teleportCooldown)
        {
            Vector3 portalToPlayer = player.position - transform.position;
            float dotProduct = Vector3.Dot(transform.up, portalToPlayer);

            // If this is true: The player has moved across the portal
            if (dotProduct < 0f)
            {
                cc.enabled = false;
                // Teleport him!
                float rotationDiff = Quaternion.Angle(transform.rotation, receiver.rotation);
                //rotationDiff += 180;
                player.Rotate(Vector3.up, rotationDiff);

                Vector3 positionOffset = Quaternion.Euler(0f, rotationDiff, 0f) * portalToPlayer;
                player.position = receiver.position + positionOffset;

                cc.enabled = true;
                lastTeleportTime = Time.time;
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
